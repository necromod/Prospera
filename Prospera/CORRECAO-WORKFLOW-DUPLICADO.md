# ?? CORREÇÃO APLICADA - Workflow Duplicado Removido

## ?? Problema Identificado

Existiam **dois workflows** conflitantes:

### 1. ? **Prosperaweb.yml** (ANTIGO - REMOVIDO)
- Usava: **Azure Login com Service Principal**
- Requeria: `CLIENT_ID`, `TENANT_ID`, `SUBSCRIPTION_ID`
- Rodava em: **Windows**
- Status: **Causava erro de autenticação**

### 2. ? **azure-webapp-deploy.yml** (ATUAL - MANTIDO)
- Usa: **Publish Profile** (método mais simples)
- Requer: `AZURE_WEBAPP_PUBLISH_PROFILE`
- Roda em: **Ubuntu** (Linux)
- Status: **Configurado corretamente**

## ? Solução Aplicada

### Arquivos Removidos:
- ? `.github/workflows/Prosperaweb.yml` (workflow antigo)
- ? `.github/workflows/test-secret.yml` (workflow de teste)

### Arquivo Mantido:
- ? `.github/workflows/azure-webapp-deploy.yml` (workflow correto)

## ?? Workflow Atual (azure-webapp-deploy.yml)

### Características:
- **Nome**: Build and deploy ASP.NET Core app to Azure Web App
- **Trigger**: Push na branch `main` ou execução manual
- **Jobs**: 
  1. **build** - Compila o projeto Prospera
  2. **deploy** - Faz deploy no Azure usando Publish Profile
- **Autenticação**: `AZURE_WEBAPP_PUBLISH_PROFILE` (secret)
- **App Name**: `Prosperaweb`
- **Plataforma**: Ubuntu (Linux)
- **.NET Version**: 8.0.x

## ?? Próximos Passos

### 1. Verificar o Secret no GitHub

Certifique-se de que o secret está configurado:
- URL: https://github.com/necromod/Prospera/settings/secrets/actions
- Nome: `AZURE_WEBAPP_PUBLISH_PROFILE`
- Status: Deve aparecer na lista (valor oculto é normal)

### 2. Commit e Push

```sh
cd C:\Users\edsilva\source\repos\necromod\Prospera

# Ver mudanças
git status

# Adicionar arquivos
git add .github/workflows/

# Commit
git commit -m "Remove workflows duplicados e conflitantes - mantém apenas azure-webapp-deploy.yml com Publish Profile"

# Push
git push origin main
```

### 3. Monitorar o Deploy

Após o push, verifique em:
- https://github.com/necromod/Prospera/actions

Você deve ver apenas **um** workflow executando:
- ? "Build and deploy ASP.NET Core app to Azure Web App"

## ?? Verificação Rápida

### Listar workflows ativos:
```sh
Get-ChildItem ".github\workflows" | Select-Object Name
```

**Resultado esperado:**
```
Name
----
azure-webapp-deploy.yml
```

### Verificar conteúdo do workflow:
```sh
Get-Content ".github\workflows\azure-webapp-deploy.yml" | Select-String "publish-profile"
```

**Deve mostrar:**
```yaml
publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
```

## ? Checklist Final

- [x] Workflow antigo `Prosperaweb.yml` removido
- [x] Workflow de teste `test-secret.yml` removido
- [x] Workflow `azure-webapp-deploy.yml` configurado corretamente
- [ ] Secret `AZURE_WEBAPP_PUBLISH_PROFILE` configurado no GitHub
- [ ] Commit e push das alterações
- [ ] Monitorar workflow no GitHub Actions

## ?? Resultado Esperado

Após o push:
1. ? Build será executado com sucesso
2. ? Deploy será feito usando Publish Profile
3. ? App estará disponível em: https://prosperaweb.azurewebsites.net

## ?? Referências

- [Azure Web Apps Deploy Action](https://github.com/Azure/webapps-deploy)
- [GitHub Actions Secrets](https://docs.github.com/actions/security-guides/encrypted-secrets)
- [Deploy to Azure App Service](https://docs.microsoft.com/azure/app-service/deploy-github-actions)

---

**Data da Correção**: ${new Date().toISOString().split('T')[0]}  
**Status**: ? Pronto para commit e push
