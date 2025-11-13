# Guia de Configuração do Azure Deploy

## ?? Problema Atual

O deploy está falhando porque:
1. **Autenticação básica está desabilitada** no Azure App Service
2. Não é possível baixar o **Publish Profile**

```
Erro: "A autenticação básica está desabilitada"
```

## ? Solução: Habilitar Autenticação Básica

### Passo 1: Habilitar Auth Básica no Azure Portal

1. Acesse o [Portal do Azure](https://portal.azure.com)
2. Navegue até **App Services** ? **Prosperaweb**
3. No menu lateral, clique em **Configuration** (Configuração)
4. Selecione a aba **General settings** (Configurações gerais)
5. Role até **Platform settings**
6. Localize **SCM Basic Auth Publishing Credentials**
7. **Altere para "On"** (Ligado) ??
8. Clique em **Save** (Salvar) no topo

**Imagem de referência:**
```
Configuration > General settings > SCM Basic Auth Publishing Credentials: [ON]
```

### Passo 2: Baixar o Publish Profile

1. Volte para a página **Overview** do App Service Prosperaweb
2. No menu superior, clique em **Get publish profile** ??
3. O arquivo `Prosperaweb.PublishSettings` será baixado
4. Abra o arquivo no **Notepad** e copie **TODO** o conteúdo

**Exemplo do conteúdo (não use este, é apenas exemplo):**
```xml
<publishData>
  <publishProfile profileName="Prosperaweb - Web Deploy" publishMethod="MSDeploy" 
    publishUrl="prosperaweb.scm.azurewebsites.net:443" 
    msdeploySite="Prosperaweb" 
    userName="$Prosperaweb" 
    userPWD="xxxxxxxxxxxxx" 
    destinationAppUrl="http://prosperaweb.azurewebsites.net" />
</publishData>
```

### Passo 3: Adicionar Secret no GitHub

1. Vá para o repositório: https://github.com/necromod/Prospera
2. Clique em **Settings** ? **Secrets and variables** ? **Actions**
3. Clique em **New repository secret**
4. Configure:
   - **Name**: `AZURE_WEBAPP_PUBLISH_PROFILE`
   - **Value**: Cole o conteúdo **completo** do arquivo `.PublishSettings`
5. Clique em **Add secret**

### Passo 4: Verificar o Nome do App

No arquivo `.github/workflows/azure-webapp-deploy.yml`, confirme:
```yaml
env:
  AZURE_WEBAPP_NAME: Prosperaweb  # ? Deve corresponder ao nome no Azure
```

## ?? Alternativa: Usar Service Principal (Mais Seguro)

Se preferir não habilitar autenticação básica:

### 1. Criar Service Principal via Azure CLI

```bash
az login

az ad sp create-for-rbac --name "Prospera-GitHub-Deploy" \
  --role contributor \
  --scopes /subscriptions/5c4924fa-2b44-4cdb-8d95-1d0bae34b761/resourceGroups/<seu-resource-group>/providers/Microsoft.Web/sites/Prosperaweb \
  --json-auth
```

### 2. Copiar o JSON Retornado

```json
{
  "clientId": "xxxxx-xxxxx-xxxxx-xxxxx",
  "clientSecret": "xxxxx~xxxxx",
  "subscriptionId": "5c4924fa-2b44-4cdb-8d95-1d0bae34b761",
  "tenantId": "61585c28-aee6-4741-9fd9-bd0b04b1e51d",
  "resourceManagerEndpointUrl": "https://management.azure.com/"
}
```

### 3. Adicionar Secret no GitHub

- **Nome**: `AZURE_CREDENTIALS`
- **Valor**: Cole o JSON completo

### 4. Atualizar Workflow (se usar Service Principal)

Adicione o step de login antes do deploy:
```yaml
- name: Login to Azure
  uses: azure/login@v2
  with:
    creds: ${{ secrets.AZURE_CREDENTIALS }}

- name: Deploy to Azure Web App
  uses: azure/webapps-deploy@v3
  with:
    app-name: ${{ env.AZURE_WEBAPP_NAME }}
    package: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
    # NÃO use publish-profile se usar AZURE_CREDENTIALS
```

## ?? Testar o Deploy

Após configurar o secret:

```bash
# Commit e push
git add .github/workflows/azure-webapp-deploy.yml
git commit -m "Configura deploy com Publish Profile"
git push origin main
```

Monitore em: https://github.com/necromod/Prospera/actions

## ?? Status das Variáveis Azure

Informações da sua conta:
- ? `TENANT_ID`: `61585c28-aee6-4741-9fd9-bd0b04b1e51d`
- ? `SUBSCRIPTION_ID`: `5c4924fa-2b44-4cdb-8d95-1d0bae34b761`
- ? `CLIENT_ID`: Será criado ao configurar Service Principal (opcional)

## ?? Troubleshooting

### Erro: "Failed to deploy web package"
? **Solução**: Verifique se o secret `AZURE_WEBAPP_PUBLISH_PROFILE` está configurado corretamente

### Erro: "Resource not found"
? **Solução**: Confirme que o App Service "Prosperaweb" existe e está ativo no Azure

### Erro: "Authentication failed"
? **Solução**: Baixe um novo Publish Profile após habilitar auth básica

### Erro: "Basic authentication is disabled"
? **Solução**: Siga o Passo 1 acima para habilitar

## ?? Comparação dos Métodos

| Método | Prós | Contras | Recomendação |
|--------|------|---------|--------------|
| **Publish Profile** | ? Simples<br>? Rápido<br>? Fácil configurar | ?? Requer auth básica<br>?? Menos seguro | ?? **RECOMENDADO** para começar |
| **Service Principal** | ? Mais seguro<br>? Granular<br>? Não precisa auth básica | ?? Complexo<br>?? Requer Azure CLI | ?? Para produção |

## ?? Recursos Adicionais

- [Deploy to Azure App Service](https://docs.microsoft.com/azure/app-service/deploy-github-actions)
- [Configure GitHub Actions Secrets](https://docs.github.com/actions/security-guides/encrypted-secrets)
- [Azure Web Apps Deploy Action](https://github.com/Azure/webapps-deploy)
- [Azure Login Action](https://github.com/Azure/login)

## ? Checklist Final

- [ ] Habilitar autenticação básica no Azure Portal
- [ ] Baixar arquivo `.PublishSettings`
- [ ] Criar secret `AZURE_WEBAPP_PUBLISH_PROFILE` no GitHub
- [ ] Confirmar nome do app: `Prosperaweb`
- [ ] Fazer commit e push
- [ ] Monitorar workflow no GitHub Actions
