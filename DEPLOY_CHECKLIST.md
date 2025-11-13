# ? Checklist de Deploy - Prospera no Azure

## ?? Antes do Deploy

### Ambiente Local
- [ ] Código compila sem erros (`dotnet build`)
- [ ] Todos os testes passam (se existirem)
- [ ] Migrations estão atualizadas (`dotnet ef migrations list`)
- [ ] Connection string local funciona
- [ ] Aplicação roda localmente sem erros

### Configuração de Arquivos
- [x] `appsettings.json` configurado
- [x] `appsettings.Production.json` criado
- [x] `web.config` criado
- [x] `.gitignore` configurado corretamente
- [x] `Program.cs` com configurações para Azure
- [x] Application Insights adicionado
- [x] Health checks configurados

### GitHub
- [ ] Código commitado e pushed para `main`
- [ ] `.github/workflows/azure-webapp-deploy.yml` configurado
- [ ] Repository é público ou tem permissions corretas

## ??? Infraestrutura Azure

### Resource Group
- [ ] Resource Group criado (`rg-prospera`)
- [ ] Região definida (ex: `brazilsouth`)

### SQL Database
- [ ] SQL Server criado
- [ ] SQL Database criado (nome: `Prospera-DB`)
- [ ] Firewall configurado para permitir Azure Services
- [ ] Firewall configurado para seu IP (para testes)
- [ ] Admin user e senha anotados em local seguro
- [ ] Connection string obtida

### App Service
- [ ] App Service Plan criado
- [ ] Tier adequado selecionado (mínimo B1 para produção)
- [ ] Web App criado (nome: `Prosperaweb`)
- [ ] Runtime stack: .NET 8.0
- [ ] Platform: 64 Bit
- [ ] Region: mesma do SQL Database

### Application Insights (Recomendado)
- [ ] Application Insights criado
- [ ] Connection string obtida
- [ ] Linked ao App Service

## ?? Configuração do App Service

### Connection Strings
- [ ] `ProsperaContext` adicionada em Connection strings
- [ ] Type: SQLAzure
- [ ] Valor correto (testado localmente)

### Application Settings
- [ ] `ASPNETCORE_ENVIRONMENT` = Production
- [ ] `ApplicationInsights__ConnectionString` configurado (se usar App Insights)
- [ ] Outras settings necessárias adicionadas

### General Settings
- [ ] Stack: .NET
- [ ] .NET version: 8.0
- [ ] Platform: 64 Bit
- [ ] Always On: **On** (importante para produção)
- [ ] HTTPS Only: **On** (obrigatório)
- [ ] Minimum TLS Version: 1.2
- [ ] HTTP version: 2.0
- [ ] ARR affinity: On

### TLS/SSL Settings
- [ ] HTTPS Only: Enabled
- [ ] Minimum TLS Version: 1.2

### Identity (Opcional - Managed Identity)
Se usar Azure AD Authentication no SQL:
- [ ] System assigned identity: On
- [ ] Principal ID obtido
- [ ] Permissões configuradas no SQL Database:
  ```sql
  CREATE USER [Prosperaweb] FROM EXTERNAL PROVIDER;
  ALTER ROLE db_datareader ADD MEMBER [Prosperaweb];
  ALTER ROLE db_datawriter ADD MEMBER [Prosperaweb];
  ALTER ROLE db_ddladmin ADD MEMBER [Prosperaweb];
  ```

### Deployment Center
- [ ] Source: GitHub
- [ ] Organization: necromod
- [ ] Repository: Prospera
- [ ] Branch: main
- [ ] Publish profile baixado

### GitHub Secrets
- [ ] Secret `AZURE_WEBAPP_PUBLISH_PROFILE` criado
- [ ] Valor: XML do publish profile

## ??? Banco de Dados

### Migrations
- [ ] Migrations aplicadas no banco do Azure
  ```bash
  dotnet ef database update --connection "CONNECTION_STRING"
  ```
  OU
- [ ] Migration automática habilitada no `Program.cs` (já configurado)

### Dados Iniciais (se necessário)
- [ ] Dados de teste/seed inseridos
- [ ] Usuário admin criado (se aplicável)

### Performance
- [ ] Índices criados nas tabelas principais
- [ ] Queries otimizadas

## ?? Logs e Monitoramento

### App Service Logs
- [ ] Application Logging (Filesystem): On
- [ ] Level: Information (Development) ou Warning (Production)
- [ ] Web server logging: File System
- [ ] Retention Period: 7 days
- [ ] Detailed error messages: On
- [ ] Failed request tracing: On

### Application Insights (se configurado)
- [ ] Instrumentação key configurada
- [ ] Telemetria funcionando
- [ ] Application Map populado
- [ ] Alerts configurados

## ?? Deploy

### Deploy Inicial
- [ ] Push para branch `main` ativa GitHub Action
- [ ] Workflow executa sem erros
- [ ] Build é bem-sucedido
- [ ] Deploy é bem-sucedido
- [ ] App Service mostra status "Running"

### Verificações Pós-Deploy
- [ ] Site acessível: `https://prosperaweb.azurewebsites.net`
- [ ] Health check OK: `https://prosperaweb.azurewebsites.net/health`
- [ ] Página inicial carrega
- [ ] CSS/JS carregam corretamente
- [ ] Imagens carregam

### Testes Funcionais
- [ ] Página de login carrega
- [ ] Consegue fazer cadastro de novo usuário
- [ ] Consegue fazer login
- [ ] Menu do usuário aparece após login
- [ ] CRUD de Terceiros funciona
- [ ] CRUD de Contas funciona
- [ ] CRUD de Conta Bancária funciona
- [ ] CRUD de Extrato funciona
- [ ] Logout funciona
- [ ] Sessão persiste entre requests

### Segurança
- [ ] HTTPS funciona (cadeado verde no navegador)
- [ ] HTTP redireciona para HTTPS
- [ ] Cookies são Secure e HttpOnly
- [ ] Não há warnings de segurança no console
- [ ] Connection strings não estão expostas no código
- [ ] Senhas não estão hardcoded

## ?? Performance e Escalabilidade

### Performance
- [ ] Tempo de resposta < 2 segundos na página inicial
- [ ] Queries do banco otimizadas
- [ ] Static files com cache
- [ ] Application Insights mostra performance aceitável

### Escalabilidade (se necessário)
- [ ] Auto-scale configurado
- [ ] Regras de scale out definidas:
  - CPU > 70%
  - Memory > 80%
  - HTTP Queue > 50
- [ ] Redis Cache configurado para sessões distribuídas (se múltiplas instâncias)

## ?? Backup e Recuperação

### Backup
- [ ] Backup automático configurado (App Service)
- [ ] Backup do SQL Database configurado
- [ ] Retention policy definida

### Disaster Recovery
- [ ] Point-in-time restore testado (SQL)
- [ ] Deployment slots configurados (se produção crítica)

## ?? Domínio Customizado (Opcional)

Se usar domínio próprio:
- [ ] DNS configurado
- [ ] Custom domain adicionado ao App Service
- [ ] SSL Certificate configurado
- [ ] HTTPS funciona com domínio customizado
- [ ] Redirect de www para non-www (ou vice-versa)

## ?? Monitoramento Contínuo

### Alerts
- [ ] Alert para CPU > 80%
- [ ] Alert para Memory > 90%
- [ ] Alert para Failed Requests
- [ ] Alert para Response Time > 5s
- [ ] Notifications configuradas (email/SMS)

### Logs
- [ ] Log retention configurado
- [ ] Log Analytics workspace criado (opcional)
- [ ] Queries úteis salvas

## ?? Documentação

- [x] README atualizado com instruções
- [x] AZURE_DEPLOYMENT_GUIDE.md criado
- [x] TROUBLESHOOTING.md criado
- [x] AZURE_APP_SETTINGS.txt criado
- [ ] Credenciais salvas em local seguro (Key Vault ou senha manager)
- [ ] Equipe informada sobre URLs e acessos

## ? Validação Final

### Smoke Tests
```bash
# Health check
curl https://prosperaweb.azurewebsites.net/health

# Página inicial
curl -I https://prosperaweb.azurewebsites.net

# Ver se retorna 200 OK
```

### Verificar Logs
```bash
# Ver logs em tempo real
az webapp log tail --name Prosperaweb --resource-group rg-prospera

# Procurar por erros
az webapp log tail --name Prosperaweb --resource-group rg-prospera | grep -i error
```

### Verificar Application Insights
- [ ] Sem exceções não tratadas
- [ ] Response time aceitável
- [ ] Dependency calls OK
- [ ] No failed requests

## ?? Go Live

- [ ] Todas as verificações acima passaram
- [ ] Stakeholders informados
- [ ] URL do site compartilhada
- [ ] Monitoramento ativo
- [ ] Equipe de suporte preparada

## ?? Contatos Importantes

- **Azure Support**: Portal do Azure > Help + support
- **GitHub Support**: https://support.github.com/
- **Status do Azure**: https://status.azure.com/

---

**Data do Deploy**: _______________

**Responsável**: _______________

**Ambiente**: Production

**URL**: https://prosperaweb.azurewebsites.net
