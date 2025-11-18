-- ========================================
-- Script SIMPLES - Limpeza e Configuração
-- ========================================

-- ETAPA 1: Limpar usuário antigo
PRINT 'Limpando configuração antiga...';

-- Remover das roles (se existir)
BEGIN TRY
    ALTER ROLE db_datareader DROP MEMBER [prosperaweb];
END TRY
BEGIN CATCH
    PRINT 'Usuário não estava em db_datareader';
END CATCH

BEGIN TRY
    ALTER ROLE db_datawriter DROP MEMBER [prosperaweb];
END TRY
BEGIN CATCH
    PRINT 'Usuário não estava em db_datawriter';
END CATCH

BEGIN TRY
    ALTER ROLE db_ddladmin DROP MEMBER [prosperaweb];
END TRY
BEGIN CATCH
    PRINT 'Usuário não estava em db_ddladmin';
END CATCH

-- Remover usuário (se existir)
BEGIN TRY
    DROP USER [prosperaweb];
    PRINT 'Usuário anterior removido.';
END TRY
BEGIN CATCH
    PRINT 'Nenhum usuário anterior encontrado.';
END CATCH
GO

-- ETAPA 2: Aguardar 5 segundos
WAITFOR DELAY '00:00:05';
GO

-- ETAPA 3: Criar novo usuário
PRINT 'Criando novo usuário...';
CREATE USER [prosperaweb] FROM EXTERNAL PROVIDER;
PRINT 'Usuário criado com sucesso!';
GO

-- ETAPA 4: Conceder permissões
PRINT 'Concedendo permissões...';
ALTER ROLE db_datareader ADD MEMBER [prosperaweb];
ALTER ROLE db_datawriter ADD MEMBER [prosperaweb];
ALTER ROLE db_ddladmin ADD MEMBER [prosperaweb];
PRINT 'Permissões concedidas!';
GO

-- ETAPA 5: Verificar
PRINT '';
PRINT '========================================';
PRINT 'VERIFICAÇÃO FINAL';
PRINT '========================================';

SELECT 
    dp.name AS Usuario,
    dp.type_desc AS Tipo,
    r.name AS Permissao
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'prosperaweb'
ORDER BY r.name;

PRINT '';
PRINT '? SUCESSO! Configuração completa.';
