-- ========================================
-- SOLUÇÃO DEFINITIVA - Passo a Passo
-- ========================================

-- ?? IMPORTANTE: Execute cada bloco separadamente!
-- Copie e cole um bloco por vez no Azure Data Studio

-- ============================================================
-- BLOCO 1: Verificar situação atual
-- ============================================================
PRINT 'Verificando usuários existentes no banco...';
SELECT 
    name, 
    type_desc, 
    authentication_type_desc,
    create_date 
FROM sys.database_principals 
WHERE name LIKE '%prosper%'
ORDER BY name;
GO

-- ============================================================
-- BLOCO 2: Remover usuário "Prosperaweb" (P maiúsculo)
-- ============================================================
-- Execute este bloco se o usuário "Prosperaweb" existir

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'Prosperaweb')
BEGIN
    PRINT 'Removendo usuário "Prosperaweb" (P maiúsculo)...';
    
    -- Remover das roles
    BEGIN TRY ALTER ROLE db_datareader DROP MEMBER [Prosperaweb]; END TRY BEGIN CATCH END CATCH
    BEGIN TRY ALTER ROLE db_datawriter DROP MEMBER [Prosperaweb]; END TRY BEGIN CATCH END CATCH
    BEGIN TRY ALTER ROLE db_ddladmin DROP MEMBER [Prosperaweb]; END TRY BEGIN CATCH END CATCH
    
    -- Remover usuário
    DROP USER [Prosperaweb];
    PRINT '? Usuário "Prosperaweb" removido.';
END
ELSE
BEGIN
    PRINT '?? Usuário "Prosperaweb" não existe.';
END
GO

-- ============================================================
-- BLOCO 3: Remover usuário "prosperaweb" (p minúsculo)
-- ============================================================
-- Execute este bloco se o usuário "prosperaweb" existir

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'prosperaweb')
BEGIN
    PRINT 'Removendo usuário "prosperaweb" (p minúsculo)...';
    
    -- Remover das roles
    BEGIN TRY ALTER ROLE db_datareader DROP MEMBER [prosperaweb]; END TRY BEGIN CATCH END CATCH
    BEGIN TRY ALTER ROLE db_datawriter DROP MEMBER [prosperaweb]; END TRY BEGIN CATCH END CATCH
    BEGIN TRY ALTER ROLE db_ddladmin DROP MEMBER [prosperaweb]; END TRY BEGIN CATCH END CATCH
    
    -- Remover usuário
    DROP USER [prosperaweb];
    PRINT '? Usuário "prosperaweb" removido.';
END
ELSE
BEGIN
    PRINT '?? Usuário "prosperaweb" não existe.';
END
GO

-- ============================================================
-- BLOCO 4: Aguardar 10 segundos para propagação
-- ============================================================
PRINT 'Aguardando 10 segundos para propagação no Azure AD...';
WAITFOR DELAY '00:00:10';
PRINT '? Aguardando concluído.';
GO

-- ============================================================
-- BLOCO 5: Criar novo usuário com nome único
-- ============================================================
PRINT 'Criando novo usuário "prosperaweb" do Managed Identity...';
CREATE USER [prosperaweb] FROM EXTERNAL PROVIDER;
PRINT '? Usuário criado com sucesso!';
GO

-- ============================================================
-- BLOCO 6: Conceder permissões
-- ============================================================
PRINT 'Concedendo permissões...';
ALTER ROLE db_datareader ADD MEMBER [prosperaweb];
PRINT '? db_datareader concedido';
ALTER ROLE db_datawriter ADD MEMBER [prosperaweb];
PRINT '? db_datawriter concedido';
ALTER ROLE db_ddladmin ADD MEMBER [prosperaweb];
PRINT '? db_ddladmin concedido';
GO

-- ============================================================
-- BLOCO 7: Verificação final
-- ============================================================
PRINT '';
PRINT '========================================';
PRINT 'VERIFICAÇÃO FINAL';
PRINT '========================================';

SELECT 
    dp.name AS [Usuário],
    dp.type_desc AS [Tipo],
    dp.authentication_type_desc AS [Autenticação],
    r.name AS [Permissão]
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'prosperaweb'
ORDER BY r.name;

PRINT '';
PRINT '? ========================================';
PRINT '? CONFIGURAÇÃO CONCLUÍDA COM SUCESSO!';
PRINT '? ========================================';
