# Guia de Troubleshooting - Prospera no Azure

## ?? Problemas Comuns e Soluções

### 1. Erro 500.30 - ASP.NET Core app failed to start

**Sintomas:**
- Aplicação não inicia
- Erro 500.30 no navegador
- Logs mostram "Failed to start application"

**Soluções:**

1. **Verificar logs:**
```bash
az webapp log tail --name Prosperaweb --resource-group rg-prospera
```

2. **Verificar Connection String:**
- Vá em Azure Portal > App Service > Configuration > Connection strings
- Verifique se `ProsperaContext` está configurada corretamente
- Type deve ser: SQLAzure

3. **Verificar Application Settings:**
```bash
az webapp config appsettings list --name Prosperaweb --resource-group rg-prospera
```

4. **Habilitar logs detalhados:**
- Azure Portal > App Service > App Service logs
- Habilitar todas as opções
- Reiniciar o app

### 2. Erro de Conexão com Banco de Dados

**Sintomas:**
- "Cannot open database"
- "Login failed for user"
- Timeout ao conectar

**Soluções:**

1. **Verificar Firewall do SQL:**
```bash
# Permitir Azure Services
az sql server firewall-rule create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

# Permitir seu IP (para debug)
az sql server firewall-rule create \
  --resource-group rg-prospera \
  --server sql-prospera-server \
  --name AllowMyIP \
  --start-ip-address SEU_IP \
  --end-ip-address SEU_IP
```

2. **Testar conexão localmente:**
```bash
# Obter connection string
az sql db show-connection-string \
  --client ado.net \
  --name Prospera-DB \
  --server sql-prospera-server
```

3. **Verificar Managed Identity (se usar):**
```bash
# Ver se está habilitada
az webapp identity show --name Prosperaweb --resource-group rg-prospera

# Verificar permissões no SQL (executar no Query Editor do Azure Portal):
SELECT * FROM sys.database_principals WHERE name = 'Prosperaweb';
```

### 3. Migrations não são aplicadas

**Sintomas:**
- Tabelas não existem no banco
- "Invalid object name" errors

**Soluções:**

1. **Aplicar migrations manualmente:**
```bash
# Localmente com connection string do Azure
dotnet ef database update --project Prospera/Prospera.csproj --connection "SUA_CONNECTION_STRING"
```

2. **Verificar logs de startup:**
- Procure por "Applying pending migrations"
- Se não aparecer, a migration automática não rodou

3. **Verificar permissões:**
- O usuário/Managed Identity precisa ter permissão `db_ddladmin`

### 4. Sessão não persiste / Usuário desloga automaticamente

**Sintomas:**
- Usuário é deslogado aleatoriamente
- Session variables são perdidas

**Soluções:**

1. **Verificar HTTPS:**
- Azure Portal > App Service > TLS/SSL settings > HTTPS Only: On

2. **Verificar ARR Affinity:**
- Azure Portal > App Service > Configuration > General settings
- ARR affinity: On (se usar apenas 1 instância)

3. **Usar Azure Cache for Redis (produção):**
```bash
# Criar Redis Cache
az redis create \
  --name cache-prospera \
  --resource-group rg-prospera \
  --location brazilsouth \
  --sku Basic \
  --vm-size c0

# Obter connection string
az redis list-keys --name cache-prospera --resource-group rg-prospera

# Adicionar pacote no projeto
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis

# Atualizar Program.cs:
# builder.Services.AddStackExchangeRedisCache(options =>
# {
#     options.Configuration = builder.Configuration.GetConnectionString("Redis");
# });
```

### 5. Static Files (CSS/JS) não carregam

**Sintomas:**
- Página sem estilos
- JavaScript não funciona
- 404 para arquivos .css e .js

**Soluções:**

1. **Verificar web.config:**
- O arquivo `web.config` deve estar no root do projeto

2. **Verificar wwwroot:**
- Arquivos estáticos devem estar em `wwwroot/`

3. **Limpar cache do navegador:**
- Ctrl + Shift + R

4. **Verificar logs:**
```bash
az webapp log tail --name Prosperaweb --resource-group rg-prospera | grep "404"
```

### 6. Performance Lenta

**Sintomas:**
- Páginas demoram para carregar
- Timeouts

**Soluções:**

1. **Verificar DTU do SQL Database:**
```bash
# Ver uso atual
az sql db show \
  --name Prospera-DB \
  --server sql-prospera-server \
  --resource-group rg-prospera \
  --query "currentServiceObjectiveName"

# Aumentar tier se necessário
az sql db update \
  --name Prospera-DB \
  --server sql-prospera-server \
  --resource-group rg-prospera \
  --service-objective S1
```

2. **Verificar App Service Plan:**
```bash
# Ver tier atual
az appservice plan show \
  --name plan-prospera \
  --resource-group rg-prospera \
  --query "sku"

# Aumentar se necessário
az appservice plan update \
  --name plan-prospera \
  --resource-group rg-prospera \
  --sku S1
```

3. **Habilitar Application Insights:**
- Ver queries lentas
- Ver exceções
- Ver uso de memória/CPU

4. **Otimizar queries:**
- Adicionar índices no banco
- Usar AsNoTracking() em queries read-only
- Implementar caching

### 7. Deploy falha no GitHub Actions

**Sintomas:**
- Workflow fica vermelho
- "Failed to deploy" no GitHub Actions

**Soluções:**

1. **Verificar Publish Profile:**
```bash
# Gerar novo publish profile
az webapp deployment list-publishing-profiles \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --xml > publish-profile.xml

# Atualizar secret no GitHub
# Settings > Secrets > AZURE_WEBAPP_PUBLISH_PROFILE
```

2. **Verificar logs do workflow:**
- GitHub > Actions > Ver workflow falhado
- Expandir cada step

3. **Testar build localmente:**
```bash
cd Prospera
dotnet build --configuration Release
dotnet publish --configuration Release --output ./publish
```

### 8. Application Insights não mostra dados

**Sintomas:**
- Sem telemetria no Azure Portal
- Application Map vazio

**Soluções:**

1. **Verificar Connection String:**
```bash
# Ver connection string do App Insights
az monitor app-insights component show \
  --app ai-prospera \
  --resource-group rg-prospera \
  --query connectionString

# Adicionar no App Service
az webapp config appsettings set \
  --name Prosperaweb \
  --resource-group rg-prospera \
  --settings ApplicationInsights__ConnectionString="VALOR"
```

2. **Aguardar alguns minutos:**
- Dados podem demorar 3-5 minutos para aparecer

3. **Testar manualmente:**
- Acessar o site
- Gerar alguns erros
- Fazer várias requisições

### 9. Erro CORS em APIs

**Sintomas:**
- "CORS policy: No 'Access-Control-Allow-Origin'"
- Requests de JavaScript falham

**Soluções:**

1. **Adicionar CORS no Program.cs:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://seudominio.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Depois do var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
```

2. **Configurar no Azure:**
- Azure Portal > App Service > CORS
- Adicionar origens permitidas

### 10. Erro "The page isn't redirecting properly"

**Sintomas:**
- Loop infinito de redirects
- Navegador mostra "ERR_TOO_MANY_REDIRECTS"

**Soluções:**

1. **Verificar HTTPS configuration:**
```csharp
// Comentar temporariamente no Program.cs para debug:
// app.UseHttpsRedirection();
```

2. **Verificar proxy settings:**
- Azure está atrás de um proxy
- Pode precisar de ForwardedHeaders middleware

```csharp
// Adicionar no Program.cs antes de UseHttpsRedirection:
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
```

## ??? Comandos Úteis de Debug

### Ver logs em tempo real
```bash
az webapp log tail --name Prosperaweb --resource-group rg-prospera
```

### Download de todos os logs
```bash
az webapp log download --name Prosperaweb --resource-group rg-prospera --log-file logs.zip
```

### SSH no container (se Linux)
```bash
az webapp ssh --name Prosperaweb --resource-group rg-prospera
```

### Restart do app
```bash
az webapp restart --name Prosperaweb --resource-group rg-prospera
```

### Ver configurações atuais
```bash
az webapp config show --name Prosperaweb --resource-group rg-prospera
```

### Ver status do app
```bash
az webapp show --name Prosperaweb --resource-group rg-prospera --query state
```

### Ver métricas
```bash
az monitor metrics list \
  --resource "/subscriptions/{subscription-id}/resourceGroups/rg-prospera/providers/Microsoft.Web/sites/Prosperaweb" \
  --metric "CpuPercentage" \
  --start-time 2024-01-01T00:00:00Z \
  --end-time 2024-01-01T23:59:59Z
```

## ?? Suporte

Se os problemas persistirem:

1. **Verificar Status do Azure:**
   - https://status.azure.com/

2. **Abrir ticket de suporte:**
   - Azure Portal > Help + support > New support request

3. **Consultar documentação:**
   - https://docs.microsoft.com/azure/app-service/
   - https://docs.microsoft.com/azure/sql-database/

4. **Fóruns da comunidade:**
   - Stack Overflow (tag: azure-web-app-service)
   - Microsoft Q&A
