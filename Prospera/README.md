# ?? Prospera - Sistema de Gestão Financeira

<div align="center">

**Sistema completo de controle financeiro pessoal desenvolvido em ASP.NET Core**

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Azure](https://img.shields.io/badge/Azure-SQL_Database-0078D4?logo=microsoft-azure)](https://azure.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

?? **[Acesse o Sistema](https://prosperaweb.azurewebsites.net)**

</div>

---

## ?? Sobre o Projeto

**Prospera** é um sistema web de gestão financeira pessoal desenvolvido originalmente em 2023 como **Projeto Integrador do SENAC**, e completamente modernizado em janeiro de 2025.

### ?? Objetivo

Proporcionar uma plataforma intuitiva e completa para gerenciamento de finanças pessoais:
- ? Controle de receitas e despesas
- ? Gestão de contas bancárias
- ? Acompanhamento de transações
- ? Cadastro de credores e devedores
- ? Visualização de cotações em tempo real
- ? Dashboard com insights financeiros

### ?? Modernização 2025

O projeto passou por uma **completa reestruturação**:
- ? Migração para **.NET 8** e **C# 12**
- ?? Autenticação moderna com **Azure AD**
- ?? Redesign completo da interface
- ?? Sistema de modais responsivos
- ?? Integração com APIs externas
- ?? CI/CD com **GitHub Actions**
- ?? Deploy no **Azure App Service**
- ??? **Azure SQL Database** unificado

---

## ? Recursos

### ?? Dashboard Inteligente
- Cards de estatísticas financeiras
- Contas bancárias com saldos
- Cotações em tempo real (Dólar, Euro, Yuan)
- Feed de notícias do IBGE
- Movimentações recentes

### ?? Gestão Financeira
- **Receitas & Despesas**: Cadastro completo com status e formas de pagamento
- **Contas Bancárias**: Gestão de múltiplas contas
- **Terceiros**: Cadastro de credores e devedores
- **Transações**: Extrato completo com filtros

### ?? Interface Moderna
- Design system consistente
- Modais elegantes (90% da tela)
- Totalmente responsivo
- Animações suaves

---

## ??? Tecnologias

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM
- **C# 12.0** - Linguagem

### Frontend
- **Razor Pages** - Templates
- **HTML5 & CSS3** - Interface
- **JavaScript ES6+** - Interatividade

### Banco de Dados
- **Azure SQL Database**
  - Servidor: `prosperadb.database.windows.net`
  - Database: `ProsperaDB`
  - Tier: Gratuito (Gen5, 2 vCores)

### Cloud
- **Azure App Service** - `prosperaweb`
- **GitHub Actions** - CI/CD

---

## ?? Instalação

### Pré-requisitos
```bash
dotnet --version  # >= 8.0
git --version
```

### 1. Clonar
```bash
git clone https://github.com/necromod/Prospera.git
cd Prospera/Prospera
```

### 2. Configurar Banco
```bash
# Usar banco Azure (recomendado)
dotnet user-secrets set "ConnectionStrings:ProsperaContext" "Server=tcp:prosperadb.database.windows.net,1433;Initial Catalog=ProsperaDB;User ID=prosperadmin;Password=SENHA;Encrypt=True;"
```

### 3. Executar
```bash
dotnet run
```

Acesse: `https://localhost:5041`

---

## ?? Como Usar

1. **Cadastre-se**: Crie sua conta com nome, email, CPF e senha
2. **Dashboard**: Visualize seu resumo financeiro
3. **Cadastre**: Adicione receitas, despesas e contas bancárias
4. **Monitore**: Acompanhe cotações e notícias em tempo real
5. **Consulte**: Veja o extrato completo de transações

---

## ?? Estrutura

```
Prospera/
??? Controllers/       # Controladores MVC
??? Models/           # Modelos de dados
??? Data/             # Contexto EF Core
??? Views/            # Views Razor
??? wwwroot/          # Arquivos estáticos
?   ??? css/         # Estilos
?   ??? js/          # Scripts
?   ??? img/         # Imagens
??? Program.cs        # Configuração
```

---

## ??? Banco de Dados

### Ambiente ÚNICO

Tanto o **desenvolvimento local** quanto a **produção Azure** usam o **MESMO BANCO**:

```
??? Visual Studio (Local)
        ??
  Azure SQL Database
   prosperadb.database.windows.net
        ??
?? Azure Web App (Produção)
```

**Vantagens:**
- ? Dados sincronizados automaticamente
- ? Login funciona em ambos os ambientes
- ? Desenvolvimento com dados reais
- ? Deploy simplificado

### Tabelas Principais

- **Usuario**: Dados do usuário
- **Contas**: Receitas e despesas
- **ContaBancaria**: Informações bancárias
- **Terceiros**: Credores/devedores
- **Extrato**: Histórico de transações

---

## ?? Segurança

- ? HTTPS obrigatório
- ? Senhas hasheadas (ASP.NET Identity)
- ? Cookies seguros (HttpOnly, Secure, SameSite)
- ? Proteção contra SQL Injection e XSS
- ? Anti-CSRF tokens
- ? Connection strings criptografadas

---

## ?? Deploy

### Produção
- **URL**: https://prosperaweb.azurewebsites.net
- **App Service**: `prosperaweb` (rg-prospera)
- **Database**: `ProsperaDB` (Australia East)

### CI/CD
Push para `main` ? Build ? Test ? Deploy automático

---

## ?? Roadmap

### ? v2.0 (Janeiro 2025)
- [x] Migração .NET 8
- [x] Redesign interface
- [x] Deploy Azure
- [x] CI/CD
- [x] Banco unificado

### ?? v2.1+ (Futuro)
- [ ] Gráficos interativos
- [ ] Exportação PDF/Excel
- [ ] App mobile
- [ ] Modo escuro
- [ ] Multi-idioma

---

## ?? Contribuindo

1. Fork o projeto
2. Crie uma branch (`git checkout -b feature/nova`)
3. Commit (`git commit -m 'feat: adiciona X'`)
4. Push (`git push origin feature/nova`)
5. Pull Request

**Padrão de commits**: [Conventional Commits](https://www.conventionalcommits.org/)

---

## ?? Licença

MIT License - veja [LICENSE](LICENSE)

---

## ?? Equipe

**Desenvolvimento Original (2023)**: Projeto Integrador SENAC  
**Modernização (2025)**: [@necromod](https://github.com/necromod)

---

## ?? Contato

- ?? [prosperaweb.azurewebsites.net](https://prosperaweb.azurewebsites.net)
- ?? [@necromod](https://github.com/necromod)
- ?? [Issues](https://github.com/necromod/Prospera/issues)

---

## ?? Agradecimentos

- SENAC - Formação e suporte
- Microsoft - .NET e Azure
- AwesomeAPI - Cotações
- IBGE - Notícias
- Comunidade Open Source

---

<div align="center">

**Prospera** - Gerencie suas finanças com inteligência ??

Desenvolvido com ?? em 2023 | Modernizado em 2025

[![Azure](https://img.shields.io/badge/Azure-Online-0078D4?logo=microsoft-azure)](https://prosperaweb.azurewebsites.net)

</div>
