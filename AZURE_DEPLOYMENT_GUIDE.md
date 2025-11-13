# Prospera - Guia de Publicação no Azure

## ?? Pré-requisitos

- Conta Azure ativa
- Azure SQL Database criado
- Azure App Service (Windows ou Linux)
- GitHub repository configurado (já existe)

## ?? Configuração do Azure SQL Database

### 1. Criar Azure SQL Database

```bash
# Criar Resource Group
az group create --name rg-prospera --location brazilsouth

# Criar SQL Server
az sql server create \
  --name sql-prospera-server \
  --resource-group rg-prospera \
  --location brazilsouth \
  --admin-user prosperaadmin \
  --admin-password '<SUA_SENHA_FORTE>'

# Criar Database
az sql db create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name Prospera-DB \
  --service-objective S0
```

### 2. Configurar Firewall e Azure AD Authentication (Opcional)

```bash
# Permitir serviços do Azure
az sql server firewall-rule create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

# Configurar Azure AD Admin (para Managed Identity)
az sql server ad-admin create \
  --resource-group rg-prospera \
  --server-name sql-prospera-server \
  --display-name <SEU_EMAIL> \
  --object-id <SEU_OBJECT_ID>
```

### 3. String de Conexão

**Com SQL Authentication:**
```
Server=tcp:sql-prospera-server.database.windows.net,1433;Initial Catalog=Prospera-DB;Persist Security Info=False;User ID=prosperaadmin;Password=<SUA_SENHA>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

**Com Azure AD Managed Identity:**
```
Server=tcp:sql-prospera-server.database.windows.net,1433;Initial Catalog=Prospera-DB;Authentication="Active Directory Default";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

## ?? Configuração do Azure App Service

### 1. Criar App Service

```bash
# Criar App Service Plan
az appservice plan create \
  --name plan-prospera \
  --resource-group rg-prospera \
  --location brazilsouth \
  --sku B1 \
  --is-linux false

# Criar Web App
az webapp create \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --plan plan-prospera \
  --runtime "DOTNET|8.0"
```

### 2. Configurar Application Settings

```bash
# Configurar Connection String
az webapp config connection-string set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --connection-string-type SQLAzure \
  --settings ProsperaContext="<SUA_CONNECTION_STRING>"

# Configurar Application Insights (opcional mas recomendado)
az monitor app-insights component create \
  --app ai-prospera \
  --location brazilsouth \
  --resource-group rg-prospera \
  --application-type web

# Obter Connection String do App Insights
az monitor app-insights component show \
  --app ai-prospera \
  --resource-group rg-prospera \
  --query connectionString -o tsv

# Configurar App Insights no Web App
az webapp config appsettings set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --settings ApplicationInsights__ConnectionString="<APP_INSIGHTS_CONNECTION_STRING>"
```

### 3. Habilitar Managed Identity (se usar Azure AD Authentication)

```bash
# Habilitar System Managed Identity
az webapp identity assign \
  --name Prosperaweb \
  --resource-group rg-prospera

# Obter o Object ID da Managed Identity
az webapp identity show \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --query principalId -o tsv

# Dar permissões no SQL Database
# Execute no Azure Portal > SQL Database > Query Editor
# CREATE USER [Prosperaweb] FROM EXTERNAL PROVIDER;
# ALTER ROLE db_datareader ADD MEMBER [Prosperaweb];
# ALTER ROLE db_datawriter ADD MEMBER [Prosperaweb];
# ALTER ROLE db_ddladmin ADD MEMBER [Prosperaweb];
```

## ?? Deploy via GitHub Actions

### 1. Configurar Secrets no GitHub

Obter o Publish Profile:
```bash
az webapp deployment list-publishing-profiles \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --xml
```

No GitHub:
1. Vá em **Settings** ? **Secrets and variables** ? **Actions**
2. Adicione um novo secret:
   - Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
   - Value: Cole o XML do publish profile

### 2. O workflow já está configurado em `.github/workflows/azure-webapp-deploy.yml`

O deploy será automático a cada push na branch `main`.

## ?? Verificação Pós-Deploy

### 1. Verificar Health Check
```
https://prosperaweb.azurewebsites.net/health
```

### 2. Verificar Logs
```bash
# Ver logs em tempo real
az webapp log tail \
  --name Prosperaweb \
  --resource-group rg-prospera

# Habilitar logs detalhados
az webapp log config \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --application-logging filesystem \
  --detailed-error-messages true \
  --failed-request-tracing true \
  --web-server-logging filesystem
```

### 3. Acessar Application Insights

Vá ao Azure Portal ? Application Insights ? ai-prospera para ver:
- Performance metrics
- Failed requests
- Exceptions
- User analytics

## ?? Configurações de Segurança Recomendadas

### 1. Configurar HTTPS Only
```bash
az webapp update \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --https-only true
```

### 2. Configurar Minimum TLS Version
```bash
az webapp config set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --min-tls-version 1.2
```

### 3. Configurar Custom Domain (opcional)
```bash
# Mapear domínio customizado
az webapp config hostname add \
  --webapp-name Prosperaweb \
  --resource-group rg-prospera \
  --hostname www.seudominio.com.br

# Adicionar SSL Certificate
az webapp config ssl bind \
  --certificate-thumbprint <THUMBPRINT> \
  --ssl-type SNI \
  --name Prosperaweb \
  --resource-group rg-prospera
```

## ?? Monitoramento e Troubleshooting

### Comandos úteis:

```bash
# Verificar status do app
az webapp show \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --query state

# Reiniciar o app
az webapp restart \
  --name Prosperaweb \
  --resource-group rg-prospera

# Ver configurações atuais
az webapp config appsettings list \
  --name Prosperaweb \
  --resource-group rg-prospera

# Ver connection strings configuradas
az webapp config connection-string list \
  --name Prosperaweb \
  --resource-group rg-prospera
```

## ?? Problemas Comuns

### Erro de conexão com banco de dados
- Verifique se o firewall do SQL Server permite conexões do Azure
- Confirme que a connection string está correta
- Verifique se as migrations foram aplicadas

### Erro 500 no startup
- Verifique os logs com `az webapp log tail`
- Confirme que todas as configurações estão no Application Settings
- Verifique se o Application Insights está configurado corretamente

### Sessão não persiste
- Verifique se os cookies estão configurados corretamente
- Confirme que HTTPS está habilitado
- Para ambientes distribuídos, considere usar Azure Redis Cache para sessões

## ?? Recursos Adicionais

- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure SQL Database Documentation](https://docs.microsoft.com/azure/sql-database/)
- [Application Insights Documentation](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)

## ? Checklist Final

- [ ] Azure SQL Database criado e configurado
- [ ] App Service criado
- [ ] Connection String configurada
- [ ] Application Insights configurado
- [ ] GitHub Secret adicionado
- [ ] HTTPS Only habilitado
- [ ] Firewall do SQL configurado
- [ ] Migrations aplicadas
- [ ] Health check acessível
- [ ] Logs funcionando
