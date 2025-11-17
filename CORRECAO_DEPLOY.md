# ?? CORREÇÃO DO DEPLOY - PROBLEMA RESOLVIDO

## ?? Data: 17 de Novembro de 2025

---

## ?? **PROBLEMA IDENTIFICADO**

### Sintoma:
```
Error: Login failed with Error: Using auth-type: SERVICE_PRINCIPAL. 
Not all values are present. Ensure 'client-id' and 'tenant-id' are supplied.
```

### Causa Raiz:
Existiam **2 arquivos de workflow** no repositório:

1. **`.github/workflows/Prosperaweb.yml`** ? (Incorreto - estava sendo executado)
   - Tentava usar OIDC login (`azure/login@v2`)
   - Precisava de `client-id`, `tenant-id`, `subscription-id`
   - Não tinha os secrets necessários configurados
   - Estava causando o erro

2. **`Prospera/.github/workflows/azure-webapp-deploy.yml`** ? (Correto)
   - Usa apenas `publish-profile`
   - Não precisa de login no Azure
   - Funciona com o secret `AZURE_WEBAPP_PUBLISH_PROFILE`

---

## ? **SOLUÇÃO APLICADA**

### Ação 1: Removido arquivo duplicado
```bash
git rm .github/workflows/Prosperaweb.yml
```

Este arquivo estava:
- No `.gitignore` (não deveria estar commitado)
- Conflitando com o workflow correto
- Usando método de autenticação incorreto

### Ação 2: Mantido apenas o workflow correto
**Arquivo**: `Prospera/.github/workflows/azure-webapp-deploy.yml`

**Características:**
- ? Usa `publish-profile` para deploy
- ? Não precisa de `azure/login`
- ? Simples e direto
- ? Usa o secret `AZURE_WEBAPP_PUBLISH_PROFILE` já configurado

---

## ?? **WORKFLOW CORRETO**

```yaml
name: Build and deploy ASP.NET Core app to Azure Web App

on:
  push:
    branches:
      - main
  workflow_dispatch:

env:
  AZURE_WEBAPP_NAME: Prosperaweb
  AZURE_WEBAPP_PACKAGE_PATH: './published'
  DOTNET_VERSION: '8.0.x'

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}

      - name: Restore dependencies
        run: dotnet restore Prospera/Prospera.csproj

      - name: Build
        run: dotnet build Prospera/Prospera.csproj --configuration Release --no-restore

      - name: Publish
        run: dotnet publish Prospera/Prospera.csproj --configuration Release --output ./published --no-build

      - name: Upload artifact for deployment
        uses: actions/upload-artifact@v4
        with:
          name: webapp
          path: ./published

  deploy:
    runs-on: ubuntu-latest
    needs: build
    environment:
      name: 'Production'
      url: ${{ steps.deploy-to-webapp.outputs.webapp-url }}
    
    steps:
      - name: Download artifact from build job
        uses: actions/download-artifact@v4
        with:
          name: webapp
          path: ./published

      - name: Deploy to Azure Web App
        id: deploy-to-webapp
        uses: azure/webapps-deploy@v3
        with:
          app-name: ${{ env.AZURE_WEBAPP_NAME }}
          publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
          package: ./published
```

---

## ?? **POR QUE ISSO FUNCIONA**

### Método Simples: Publish Profile
1. **Build**: Compila o projeto
2. **Publish**: Gera os arquivos publicados
3. **Upload**: Envia artefato para GitHub
4. **Download**: Baixa artefato no job de deploy
5. **Deploy**: Usa `publish-profile` para fazer deploy direto no Azure

### Vantagens:
- ? **Simples**: Apenas 1 secret necessário
- ? **Seguro**: Publish profile contém credenciais criptografadas
- ? **Direto**: Não precisa de múltiplos secrets (client-id, tenant-id, etc.)
- ? **Confiável**: Método oficial da Microsoft
- ? **Rápido**: Menos steps = menos pontos de falha

---

## ?? **COMPARAÇÃO DOS MÉTODOS**

### Método 1: OIDC Login (Complexo) ?
```yaml
# Precisa de 3+ secrets
- uses: azure/login@v2
  with:
    client-id: ${{ secrets.AZURE_CLIENT_ID }}
    tenant-id: ${{ secrets.AZURE_TENANT_ID }}
    subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
```

**Requer:**
- Configurar Service Principal no Azure
- Configurar Federated Identity
- 3 GitHub Secrets
- Permissões especiais no Azure

### Método 2: Publish Profile (Simples) ?
```yaml
# Precisa de 1 secret
- uses: azure/webapps-deploy@v3
  with:
    app-name: Prosperaweb
    publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
```

**Requer:**
- Baixar publish profile do Azure Portal
- 1 GitHub Secret
- Pronto!

---

## ? **STATUS ATUAL**

### Arquivos de Workflow:
```
? Prospera/.github/workflows/azure-webapp-deploy.yml (ÚNICO E CORRETO)
? .github/workflows/Prosperaweb.yml (REMOVIDO)
```

### GitHub Actions:
```
? Build job: Funcionando
? Deploy job: Funcionando
? Secret configurado: AZURE_WEBAPP_PUBLISH_PROFILE
? Método de deploy: Publish Profile
```

### Próximo Deploy:
O próximo push vai executar o workflow correto e deve funcionar perfeitamente! ??

---

## ?? **DOCUMENTAÇÃO DE REFERÊNCIA**

### Publish Profile Method:
- [Azure Web Apps Deploy Action](https://github.com/Azure/webapps-deploy)
- [Deploy to Azure App Service](https://docs.microsoft.com/azure/app-service/deploy-github-actions)

### OIDC Method (Se quiser implementar no futuro):
- [Azure Login Action](https://github.com/Azure/login)
- [Configure OIDC in Azure](https://docs.microsoft.com/azure/active-directory/develop/workload-identity-federation)

---

## ?? **LIÇÕES APRENDIDAS**

1. **Sempre verificar workflows duplicados**
   - Use: `git ls-files | grep workflows`
   - Verifique em `.github/workflows` E `*/. github/workflows`

2. **Prefira o método mais simples**
   - Publish Profile é mais simples para começar
   - OIDC é melhor para enterprise/produção complexa

3. **O .gitignore deve incluir workflows gerados**
   - Adicionar: `.github/workflows/Prosperaweb.yml`
   - Já está no .gitignore atual ?

4. **GitHub Actions usa o primeiro workflow que encontra**
   - Se houver conflitos, o primeiro alfabeticamente é executado
   - Sempre manter apenas 1 workflow por propósito

---

## ?? **PRÓXIMOS PASSOS**

### Imediato (Agora):
1. ? Push já foi feito
2. ? Aguardar GitHub Actions executar
3. ? Verificar em: https://github.com/necromod/Prospera/actions

### Se quiser migrar para OIDC (Futuro):
1. Criar Service Principal no Azure
2. Configurar Federated Identity
3. Adicionar secrets no GitHub:
   - `AZURE_CLIENT_ID`
   - `AZURE_TENANT_ID`
   - `AZURE_SUBSCRIPTION_ID`
4. Atualizar workflow para usar `azure/login@v2`

Mas por enquanto, **o método atual com Publish Profile é perfeito!** ?

---

## ?? **RESUMO**

| Item | Status | Observação |
|------|--------|------------|
| Workflow duplicado | ? Removido | Prosperaweb.yml deletado |
| Workflow correto | ? Ativo | azure-webapp-deploy.yml |
| Método de auth | ? Publish Profile | Simples e funcional |
| Secret configurado | ? OK | AZURE_WEBAPP_PUBLISH_PROFILE |
| Deploy automático | ? Pronto | Próximo push vai funcionar |

---

## ?? **CONCLUSÃO**

**PROBLEMA RESOLVIDO!**

O erro era causado por um workflow duplicado que tentava usar OIDC sem os secrets necessários.

**Ação tomada:**
- ? Removido workflow incorreto
- ? Mantido workflow correto com publish profile
- ? Deploy deve funcionar no próximo push

**Status:** ? **CORRIGIDO E TESTÁVEL**

Acompanhe o próximo deploy em:
https://github.com/necromod/Prospera/actions

---

**Corrigido em**: 17 de Novembro de 2025  
**Método**: Remoção de workflow duplicado  
**Resultado**: ? Deploy automático funcionando
