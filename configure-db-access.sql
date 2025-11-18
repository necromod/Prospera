-- ========================================
-- Script para configurar acesso do App Service ao banco de dados
-- ========================================
-- SERVIDOR: prosperadb.database.windows.net
-- BANCO: ProsperaDB
-- 
-- INSTRUÇÕES:
-- 1. Conecte-se ao banco ProsperaDB no servidor prosperadb usando sua conta Entra ID
-- 2. Execute este script completo
-- ========================================

-- PASSO 1: Verificar se o usuário já existe
IF EXISTS (SELECT name FROM sys.database_principals WHERE name = 'prosperaweb')
BEGIN
    PRINT 'Usuário "prosperaweb" já existe. Removendo permissões antigas...';
    
    -- Remover das roles
    IF EXISTS (SELECT 1 FROM sys.database_role_members drm 
               JOIN sys.database_principals dp ON drm.member_principal_id = dp.principal_id 
               WHERE dp.name = 'prosperaweb')
    BEGIN
        ALTER ROLE db_datareader DROP MEMBER [prosperaweb];
        ALTER ROLE db_datawriter DROP MEMBER [prosperaweb];
        ALTER ROLE db_ddladmin DROP MEMBER [prosperaweb];
    END
    
    -- Remover o usuário
    DROP USER [prosperaweb];
    PRINT 'Usuário removido com sucesso.';
END
ELSE
BEGIN
    PRINT 'Usuário "prosperaweb" não existe. Criando novo usuário...';
END
GO

-- PASSO 2: Criar usuário para o Managed Identity do App Service
CREATE USER [prosperaweb] FROM EXTERNAL PROVIDER;
PRINT 'Usuário criado com sucesso.';
GO

-- PASSO 3: Conceder permissões necessárias
ALTER ROLE db_datareader ADD MEMBER [prosperaweb];
PRINT 'Permissão db_datareader concedida.';

ALTER ROLE db_datawriter ADD MEMBER [prosperaweb];
PRINT 'Permissão db_datawriter concedida.';

ALTER ROLE db_ddladmin ADD MEMBER [prosperaweb];
PRINT 'Permissão db_ddladmin concedida.';
GO

-- PASSO 4: Verificar se o usuário foi criado com sucesso
PRINT '';
PRINT '========================================';
PRINT 'VERIFICAÇÃO DO USUÁRIO';
PRINT '========================================';

SELECT 
    name AS [Nome do Usuário], 
    type_desc AS [Tipo], 
    authentication_type_desc AS [Tipo de Autenticação],
    create_date AS [Data de Criação]
FROM sys.database_principals 
WHERE name = 'prosperaweb';
GO

-- PASSO 5: Verificar permissões concedidas
PRINT '';
PRINT '========================================';
PRINT 'PERMISSÕES CONCEDIDAS';
PRINT '========================================';

SELECT 
    dp.name AS [Usuário],
    r.name AS [Role/Permissão]
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'prosperaweb'
ORDER BY r.name;
GO

PRINT '';
PRINT '? ========================================';
PRINT '? CONFIGURAÇÃO CONCLUÍDA COM SUCESSO!';
PRINT '? ========================================';
PRINT 'O App Service "prosperaweb" agora tem acesso ao banco de dados ProsperaDB.';
PRINT 'Reinicie o App Service para aplicar as mudanças.';
