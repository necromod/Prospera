-- ========================================
-- Script FINAL - Usando AppId correto
-- ========================================
-- Service Principal correto: prosperaweb
-- Object ID: f2c39bf3-d685-4f95-bf14-d6d4fafc94dc
-- App ID: 5ca9a43f-b9ef-4db4-9598-c1a480698c40
-- ========================================

-- ETAPA 1: Remover TODOS os usuários relacionados ao Prospera
PRINT 'Limpando configurações antigas...';

-- Tentar remover o usuário com "P" maiúsculo
BEGIN TRY
    ALTER ROLE db_datareader DROP MEMBER [Prosperaweb];
    ALTER ROLE db_datawriter DROP MEMBER [Prosperaweb];
    ALTER ROLE db_ddladmin DROP MEMBER [Prosperaweb];
    DROP USER [Prosperaweb];
    PRINT 'Usuário "Prosperaweb" removido.';
END TRY
BEGIN CATCH
    PRINT 'Usuário "Prosperaweb" não encontrado.';
END CATCH

-- Tentar remover o usuário com "p" minúsculo
BEGIN TRY
    ALTER ROLE db_datareader DROP MEMBER [prosperaweb];
    ALTER ROLE db_datawriter DROP MEMBER [prosperaweb];
    ALTER ROLE db_ddladmin DROP MEMBER [prosperaweb];
    DROP USER [prosperaweb];
    PRINT 'Usuário "prosperaweb" removido.';
END TRY
BEGIN CATCH
    PRINT 'Usuário "prosperaweb" não encontrado.';
END CATCH
GO

-- ETAPA 2: Aguardar propagação
WAITFOR DELAY '00:00:03';
PRINT 'Aguardando propagação no Azure AD...';
GO

-- ETAPA 3: Criar usuário usando App ID específico
-- Isso garante que estamos criando o usuário correto
PRINT 'Criando novo usuário com App ID específico...';
CREATE USER [prosperaweb-managed] 
    WITH SID = 0x5ca9a43fb9ef4db49598c1a480698c40, TYPE = E;
PRINT 'Usuário "prosperaweb-managed" criado!';
GO

-- ETAPA 4: Conceder permissões ao novo usuário
PRINT 'Concedendo permissões...';
ALTER ROLE db_datareader ADD MEMBER [prosperaweb-managed];
ALTER ROLE db_datawriter ADD MEMBER [prosperaweb-managed];
ALTER ROLE db_ddladmin ADD MEMBER [prosperaweb-managed];
PRINT 'Permissões concedidas!';
GO

-- ETAPA 5: Verificação completa
PRINT '';
PRINT '========================================';
PRINT 'USUÁRIOS E PERMISSÕES ATUAIS';
PRINT '========================================';

SELECT 
    dp.name AS Usuario,
    dp.type_desc AS Tipo,
    dp.authentication_type_desc AS Autenticacao,
    r.name AS Permissao
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name LIKE '%prosper%'
ORDER BY dp.name, r.name;

PRINT '';
PRINT '? ========================================';
PRINT '? CONFIGURAÇÃO COMPLETA!';
PRINT '? ========================================';
PRINT 'IMPORTANTE: Atualize a Connection String para usar o novo nome de usuário.';
