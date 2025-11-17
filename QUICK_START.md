# ? Quick Start - Deploy Rápido no Azure

## ?? Deploy em 3 Passos (10 minutos)

### Passo 1: Criar Recursos no Azure (5 minutos)

```bash
# 1.1 - Resource Group
az group create --name rg-prospera --location brazilsouth

# 1.2 - SQL Server (SUBSTITUA <ADMIN_USER> e <ADMIN_PASSWORD> por valores seguros!)
az sql server create \
  --name sql-prospera-server \
  --resource-group rg-prospera \
  --location brazilsouth \
  --admin-user <ADMIN_USER> \
  --admin-password '<ADMIN_PASSWORD>'

# 1.3 - SQL Database
az sql db create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name Prospera-DB \
  --service-objective S0

# 1.4 - Firewall (Permitir Azure Services)
az sql server firewall-rule create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

# 1.5 - App Service Plan
az appservice plan create \
  --name plan-prospera \
  --resource-group rg-prospera \
  --location brazilsouth \
  --sku B1 \
  --is-linux false

# 1.6 - Web App
az webapp create \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --plan plan-prospera \
  --runtime "DOTNET|8.0"

# 1.7 - Application Insights
az monitor app-insights component create \
  --app ai-prospera \
  --location brazilsouth \
  --resource-group rg-prospera \
  --application-type web
```

### Passo 2: Configurar Connection Strings (2 minutos)

```bash
# 2.1 - Configurar Connection String no App Service
# IMPORTANTE: Substitua <ADMIN_USER> e <ADMIN_PASSWORD> pelos valores que você definiu no Passo 1.2!
az webapp config connection-string set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --connection-string-type SQLAzure \
  --settings ProsperaContext="Server=tcp:sql-prospera-server.database.windows.net,1433;Initial Catalog=Prospera-DB;User ID=<ADMIN_USER>;Password=<ADMIN_PASSWORD>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# 2.2 - Obter Connection String do App Insights
AI_CONN=$(az monitor app-insights component show \
  --app ai-prospera \
  --resource-group rg-prospera \
  --query connectionString -o tsv)

# 2.3 - Configurar App Insights no Web App
az webapp config appsettings set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --settings ApplicationInsights__ConnectionString="$AI_CONN"

# 2.4 - Habilitar HTTPS Only
az webapp update \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --https-only true

# 2.5 - Configurar Always On
az webapp config set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --always-on true
```

### Passo 3: Configurar GitHub e Deploy (3 minutos)

```bash
# 3.1 - Obter Publish Profile
az webapp deployment list-publishing-profiles \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --xml > publish-profile.xml

# 3.2 - Copie o conteúdo do arquivo publish-profile.xml
cat publish-profile.xml

# 3.3 - Adicione no GitHub:
# - Vá em: https://github.com/necromod/Prospera/settings/secrets/actions
# - Clique em "New repository secret"
# - Name: AZURE_WEBAPP_PUBLISH_PROFILE
# - Value: Cole o conteúdo do arquivo XML
# - Clique em "Add secret"

# 3.4 - IMPORTANTE: Delete o arquivo local após adicionar no GitHub!
rm publish-profile.xml

# 3.5 - Commit e Push (vai disparar o deploy automático)
git add .
git commit -m "Deploy para Azure - Prospera v1.0"
git push origin main
```

## ? Verificação

Após ~5 minutos do push, verifique:

```bash
# Health Check
curl https://prosperaweb.azurewebsites.net/health

# Ver Logs
az webapp log tail --name Prosperaweb --resource-group rg-prospera

# Status do App
az webapp show \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --query "state" -o tsv
```

## ?? Acessar a Aplicação

**URL**: https://prosperaweb.azurewebsites.net

## ?? Verificar Monitoramento

**Application Insights**: 
1. Acesse: https://portal.azure.com
2. Busque: ai-prospera
3. Veja métricas, logs e performance

## ?? Se Algo der Errado

### Erro de Conexão com Banco?
```bash
# Adicionar seu IP no firewall
MEU_IP=$(curl -s ifconfig.me)
az sql server firewall-rule create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name AllowMyIP \
  --start-ip-address $MEU_IP \
  --end-ip-address $MEU_IP
```

### App não inicia?
```bash
# Ver logs detalhados
az webapp log config \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --application-logging filesystem \
  --detailed-error-messages true \
  --failed-request-tracing true \
  --web-server-logging filesystem

# Ver logs
az webapp log tail --name Prosperaweb --resource-group rg-prospera
```

### Migrations não aplicadas?
```bash
# Aplicar manualmente (localmente)
# IMPORTANTE: Use a connection string do Azure Portal!
dotnet ef database update \
  --project Prospera/Prospera.csproj \
  --connection "Server=tcp:sql-prospera-server.database.windows.net,1433;Initial Catalog=Prospera-DB;User ID=<ADMIN_USER>;Password=<ADMIN_PASSWORD>;Encrypt=True;"
```

## ?? Documentação Completa

- **AZURE_DEPLOYMENT_GUIDE.md** - Guia detalhado
- **DEPLOY_CHECKLIST.md** - Checklist completo
- **TROUBLESHOOTING.md** - Soluções para problemas
- **START_HERE.md** - Visão geral completa

## ?? Dicas de Segurança

1. **NUNCA** compartilhe senhas em código ou arquivos
2. **SEMPRE** use senhas fortes (mínimo 12 caracteres, letras maiúsculas, minúsculas, números e símbolos)
3. **DELETE** o arquivo `publish-profile.xml` após adicionar no GitHub
4. **NÃO COMMITE** arquivos com credenciais
5. **USE** Azure Key Vault para produção
6. **ROTACIONE** senhas regularmente

## ?? Checklist de Segurança

- [ ] Senha forte definida para SQL Server
- [ ] Publish profile adicionado apenas no GitHub Secrets
- [ ] Arquivo local publish-profile.xml deletado
- [ ] Connection strings configuradas no Azure Portal (não no código)
- [ ] HTTPS Only habilitado
- [ ] Firewall do SQL configurado
- [ ] Application Insights sem informações sensíveis

## ?? Pronto!

Se tudo estiver OK, você verá:
- ? Site acessível em https://prosperaweb.azurewebsites.net
- ? Health check retornando "Healthy"
- ? Logs sem erros
- ? Application Insights com dados

**?? Aplicação no ar com sucesso!**

---

**Tempo total**: ~10 minutos
**Custo**: ~R$ 115/mês
**Status**: Production Ready
**Segurança**: ? Credenciais protegidas
