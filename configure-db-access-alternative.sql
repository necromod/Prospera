-- ========================================
-- Script ALTERNATIVO - Usando Object ID diretamente
-- ========================================
-- Use este script se o anterior falhar por nome duplicado
-- ========================================

-- PASSO 1: Verificar usuários existentes
PRINT 'Verificando usuários existentes...';
SELECT name, type_desc, authentication_type_desc 
FROM sys.database_principals 
WHERE name LIKE '%prospera%' OR name LIKE '%web%';
GO

-- PASSO 2: Remover usuário antigo se existir (usando SID)
DECLARE @sql NVARCHAR(MAX);
DECLARE @username NVARCHAR(128);

DECLARE user_cursor CURSOR FOR
SELECT name 
FROM sys.database_principals 
WHERE name = 'prosperaweb' OR authentication_type_desc = 'EXTERNAL';

OPEN user_cursor;
FETCH NEXT FROM user_cursor INTO @username;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Remover das roles primeiro
    IF EXISTS (SELECT 1 FROM sys.database_role_members drm 
               JOIN sys.database_principals dp ON drm.member_principal_id = dp.principal_id 
               WHERE dp.name = @username)
    BEGIN
        SET @sql = 'ALTER ROLE db_datareader DROP MEMBER [' + @username + ']';
        EXEC sp_executesql @sql;
        
        SET @sql = 'ALTER ROLE db_datawriter DROP MEMBER [' + @username + ']';
        EXEC sp_executesql @sql;
        
        SET @sql = 'ALTER ROLE db_ddladmin DROP MEMBER [' + @username + ']';
        EXEC sp_executesql @sql;
    END
    
    -- Remover usuário
    SET @sql = 'DROP USER [' + @username + ']';
    EXEC sp_executesql @sql;
    PRINT 'Usuário removido: ' + @username;
    
    FETCH NEXT FROM user_cursor INTO @username;
END

CLOSE user_cursor;
DEALLOCATE user_cursor;
GO

-- PASSO 3: Criar usuário usando o Object ID do Managed Identity
-- Object ID: f2c39bf3-d685-4f95-bf14-d6d4fafc94dc
CREATE USER [prosperaweb] WITH SID = 0xf2c39bf3d6854f95bf14d6d4fafc94dc, TYPE = E;
PRINT 'Usuário criado com Object ID.';
GO

-- PASSO 4: Conceder permissões
ALTER ROLE db_datareader ADD MEMBER [prosperaweb];
ALTER ROLE db_datawriter ADD MEMBER [prosperaweb];
ALTER ROLE db_ddladmin ADD MEMBER [prosperaweb];
PRINT 'Permissões concedidas.';
GO

-- PASSO 5: Verificar configuração final
PRINT '';
PRINT '========================================';
PRINT 'CONFIGURAÇÃO FINAL';
PRINT '========================================';

SELECT 
    dp.name AS [Usuário],
    dp.type_desc AS [Tipo],
    dp.authentication_type_desc AS [Autenticação],
    r.name AS [Role]
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'prosperaweb'
ORDER BY r.name;
GO

PRINT '? Configuração concluída!';
