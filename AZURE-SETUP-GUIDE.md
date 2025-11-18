# ?? Configuração de Conexão Azure - ProsperaDB

## ?? PROBLEMA IDENTIFICADO

**Existem 2 Service Principals com nomes similares no Azure AD:**
- `Prosperaweb` (P maiúsculo) - Object ID: 15d10f64-4cab-40db-9ab8-1470eed43528
- `prosperaweb` (p minúsculo) - Object ID: f2c39bf3-d685-4f95-bf14-d6d4fafc94dc ? **Este é o correto**

Isso causou o erro: `Principal 'prosperaweb' has a duplicate display name`

---

## ?? Configuração Atual

### Servidor SQL
- **Nome do Servidor**: `prosperadb.database.windows.net`
- **Resource Group**: `ProsperaConfigurationResourceGroup`
- **Banco de Dados**: `ProsperaDB`
- **Status**: ? Online

### App Service
- **Nome**: `prosperaweb`
- **Resource Group**: `rg-prospera`
- **URL**: https://prosperaweb.azurewebsites.net
- **Status**: ? Reiniciado e rodando

### Managed Identity (Correto)
- **Principal ID**: `f2c39bf3-d685-4f95-bf14-d6d4fafc94dc`
- **App ID**: `5ca9a43f-b9ef-4db4-9598-c1a480698c40`
- **Nome**: `prosperaweb` (minúsculo)

---

## ? SOLUÇÃO - Execute os Scripts

### Opção 1: Script Passo a Passo (RECOMENDADO) ?

**Arquivo**: `configure-db-access-STEP-BY-STEP.sql`

1. Abra o arquivo no Azure Data Studio
2. **IMPORTANTE**: Execute **um bloco por vez**
3. Aguarde cada bloco completar antes de executar o próximo
4. Siga as mensagens de status

### Opção 2: Script Simples

**Arquivo**: `configure-db-access-simple.sql`

Execute todo o script de uma vez.

### Opção 3: Script Original Atualizado

**Arquivo**: `configure-db-access.sql`

Execute o script completo.

---

## ?? O que cada script faz:

1. **Remove usuários duplicados** do banco de dados
2. **Aguarda propagação** no Azure AD (importante!)
3. **Cria novo usuário** usando o Managed Identity correto
4. **Concede permissões** (read, write, ddladmin)
5. **Verifica** a configuração final

---

## ?? Teste Após Executar

Aguarde **2-3 minutos** e teste:

?? https://prosperaweb.azurewebsites.net/Extrato/ConsultaExtrato

---

## ?? Se Ainda Falhar

### Remover Service Principal duplicado do Azure

Execute este comando no terminal:

```bash
# Remover o Service Principal "Prosperaweb" (P maiúsculo)
az ad sp delete --id 15d10f64-4cab-40db-9ab8-1470eed43528
```

Depois execute o script SQL novamente.

---

## ?? Verificação no Banco de Dados

Execute esta query para verificar o status:

```sql
-- Ver todos os usuários relacionados ao Prospera
SELECT 
    name, 
    type_desc, 
    authentication_type_desc,
    create_date 
FROM sys.database_principals 
WHERE name LIKE '%prosper%'
ORDER BY name;

-- Ver permissões do usuário
SELECT 
    dp.name AS Usuario,
    r.name AS Permissao
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'prosperaweb'
ORDER BY r.name;
```

---

## ?? Arquivos Disponíveis

| Arquivo | Descrição | Quando Usar |
|---------|-----------|-------------|
| `configure-db-access-STEP-BY-STEP.sql` | ? Script passo a passo | **Recomendado** - Máximo controle |
| `configure-db-access-simple.sql` | Script simples | Rápido e direto |
| `configure-db-access.sql` | Script original atualizado | Com verificações |
| `configure-db-access-alternative.sql` | Usando Object ID | Se outros falharem |
| `configure-db-access-FINAL.sql` | Usando App ID | Alternativa avançada |

---

## ?? Resumo da Solução

1. ? **Connection String** já está correta para `prosperadb.database.windows.net`
2. ? **Managed Identity** já está habilitado no App Service
3. ? **Pendente**: Remover usuários duplicados e criar o correto no banco
4. ? **Pendente**: Testar a aplicação

---

**Próximo Passo**: Execute o arquivo `configure-db-access-STEP-BY-STEP.sql` **bloco por bloco** no Azure Data Studio.
