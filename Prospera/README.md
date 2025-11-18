# ?? Prospera - Sistema de Gestão Financeira

<div align="center">

**Sistema completo de controle financeiro pessoal desenvolvido em ASP.NET Core**

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Azure](https://img.shields.io/badge/Azure-SQL_Database-0078D4?logo=microsoft-azure)](https://azure.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

[Recursos](#-recursos) • [Instalação](#-instalação) • [Uso](#-como-usar) • [Tecnologias](#-tecnologias-utilizadas) • [Contribuir](#-contribuindo)

</div>

---

## ?? Sobre o Projeto

**Prospera** é um sistema web de gestão financeira pessoal desenvolvido originalmente em 2023 como **Projeto Integrador do SENAC**, e completamente modernizado em 2025 para atender aos padrões atuais de desenvolvimento web.

### ?? Objetivo

Proporcionar uma plataforma intuitiva e completa para gerenciamento de finanças pessoais, permitindo:
- Controle de receitas e despesas
- Gestão de contas bancárias
- Acompanhamento de transações
- Cadastro de credores e devedores
- Visualização de cotações em tempo real
- Dashboard com insights financeiros

### ?? Contexto Acadêmico

**Projeto Integrador SENAC - 2023**
- **Instituição**: SENAC (Serviço Nacional de Aprendizagem Comercial)
- **Curso**: Técnico em Desenvolvimento de Sistemas
- **Objetivo Acadêmico**: Integrar conhecimentos de banco de dados, programação backend, frontend e deploy em nuvem
- **Equipe Original**: Projeto desenvolvido como trabalho de conclusão

### ?? Modernização 2025

Em janeiro de 2025, o projeto passou por uma **completa reestruturação** para:
- ? Migrar para .NET 8 e C# 12
- ? Implementar autenticação moderna com Azure AD
- ? Redesenhar a interface com design system moderno
- ? Adicionar sistema de modais responsivos
- ? Integrar APIs externas (cotações, notícias)
- ? Configurar CI/CD com GitHub Actions
- ? Deploy automatizado no Azure

---

## ? Recursos

### ?? Dashboard Inteligente
- **Cards de Estatísticas**: Visão rápida de saldo, último recebimento, último pagamento e total de transações
- **Contas Bancárias**: Visualização de todas as contas com saldos atualizados
- **Cotações em Tempo Real**: Dólar, Euro e Yuan com atualização automática (API AwesomeAPI)
- **Notícias Financeiras**: Feed de notícias do IBGE com navegação entre artigos
- **Movimentações Recentes**: Lista das últimas transações com status e valores

### ?? Gestão Financeira Completa

#### ?? Receitas
- Cadastro de receitas com múltiplas formas de pagamento
- Controle de status (Recebido, Pendente, Atrasado)
- Associação com pagadores
- Histórico detalhado

#### ?? Despesas
- Registro de despesas com categorização
- Formas de pagamento: PIX, Transferência, Débito, Crédito, Boleto
- Controle de vencimentos
- Alertas de pagamentos atrasados

#### ?? Contas Bancárias
- Cadastro de múltiplas contas
- Controle de saldo em tempo real
- Informações de agência e número da conta
- Observações e notas

#### ?? Terceiros (Credores/Devedores)
- Cadastro completo de pessoas e empresas
- Dados de contato (telefone, email)
- Endereço completo
- Histórico de transações

#### ?? Transações
- Consulta de extrato completo
- Filtros por data e tipo
- Busca em tempo real
- Exportação de dados

### ?? Interface Moderna
- **Design System Consistente**: Cores, tipografia e componentes padronizados
- **Sistema de Modais**: Todos os formulários em modais elegantes (80% da tela)
- **Responsivo**: Funciona perfeitamente em desktop, tablet e mobile
- **Animações Suaves**: Transições e efeitos visuais profissionais
- **Sidebar Colapsável**: Menu lateral adaptável ao tamanho da tela

---

## ?? Tecnologias Utilizadas

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **SQL Server** - Banco de dados relacional
- **Azure SQL Database** - Banco de dados em nuvem

### Frontend
- **Razor Pages** - Engine de templates
- **HTML5** & **CSS3** - Estrutura e estilização
- **JavaScript ES6+** - Interatividade
- **Google Fonts (Inter)** - Tipografia moderna
- **SVG Icons (Feather Icons)** - Ícones escaláveis

### Autenticação & Segurança
- **ASP.NET Core Identity** - Gerenciamento de usuários
- **Cookie Authentication** - Sessões persistentes
- **Azure Active Directory** - Autenticação em produção
- **Data Protection** - Criptografia de dados sensíveis

### APIs Externas
- **AwesomeAPI** - Cotações de moedas em tempo real
- **IBGE Notícias** - Feed de notícias financeiras

### DevOps & Cloud
- **Azure App Service** - Hospedagem da aplicação
- **Azure SQL Database** - Banco de dados gerenciado
- **Azure App Configuration** - Gerenciamento de configurações
- **GitHub Actions** - CI/CD automatizado

### Ferramentas de Desenvolvimento
- **Visual Studio 2022** - IDE principal
- **Git** - Controle de versão
- **GitHub** - Repositório de código
- **Azure Portal** - Gerenciamento de recursos cloud

---

## ?? Instalação

### Pré-requisitos

```bash
# .NET SDK 8.0 ou superior
dotnet --version

# SQL Server Local ou acesso ao Azure SQL Database
# Git para clonar o repositório
git --version
```

### 1. Clonar o Repositório

```bash
git clone https://github.com/necromod/Prospera.git
cd Prospera
```

### 2. Configurar Connection String

#### Opção A: SQL Server Local

Edite `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ProsperaContext": "Server=localhost;Database=ProsperaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

#### Opção B: Azure SQL Database

Configure o User Secret:

```bash
cd Prospera
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ProsperaContext" "Server=tcp:SEU_SERVIDOR.database.windows.net;Authentication=Active Directory Default;Initial Catalog=ProsperaDB;Encrypt=True;"
```

### 3. Aplicar Migrations

```bash
dotnet ef database update
```

### 4. Executar a Aplicação

```bash
dotnet run
```

Acesse: `https://localhost:5041`

---

## ?? Como Usar

### 1. Primeiro Acesso

1. Acesse a página inicial
2. Clique em **"Cadastrar"**
3. Preencha seus dados: Nome, Email, CPF, Senha
4. Clique em **"Criar Conta"**
5. Você será automaticamente logado

### 2. Dashboard

Após o login, você verá:
- Cards de resumo financeiro
- Contas bancárias
- Cotações em tempo real
- Transações recentes
- Notícias financeiras

### 3. Cadastrar Receita/Despesa

1. Clique em **"Receitas"** ou **"Despesas"** no menu lateral
2. Preencha o formulário no modal
3. Clique em **"Cadastrar"**
4. O modal fecha e você retorna ao Dashboard

### 4. Consultar Transações

1. Clique em **"Transações"**
2. Use os filtros por data e tipo
3. Busque por palavra-chave
4. Exporte os dados se necessário

---

## ?? Arquitetura do Projeto

```
Prospera/
??? Controllers/           # Controladores MVC
??? Models/               # Modelos de dados
??? Data/                 # Contexto do banco de dados
??? Helpers/              # Classes auxiliares
??? Views/                # Views Razor
?   ??? Home/            # Dashboard
?   ??? Contas/          # Receitas e Despesas
?   ??? ContaBancarias/  # Contas bancárias
?   ??? Terceiros/       # Credores/Devedores
??? wwwroot/              # Arquivos estáticos
?   ??? css/             # Estilos
?   ??? js/              # Scripts
?   ??? img/             # Imagens
??? Migrations/           # Migrações do EF Core
```

---

## ?? Design System

### Paleta de Cores

```css
Primary:    #7836FA  /* Roxo principal */
Secondary:  #8C52FF  /* Roxo secundário */
Success:    #10B981  /* Verde */
Warning:    #F59E0B  /* Laranja */
Danger:     #EF4444  /* Vermelho */
Info:       #3B82F6  /* Azul */
```

### Componentes

- **Modais**: 90% largura/altura, backdrop blur, animações suaves
- **Botões**: 4 variantes (primary, success, warning, danger)
- **Inputs**: Bordas arredondadas, foco com sombra
- **Cards**: Sombra elevada, hover com transformação
- **Tabelas**: Header colorido, scroll customizado

---

## ?? Banco de Dados

### Principais Tabelas

- **Usuario**: Dados do usuário (nome, email, senha hash)
- **Contas**: Receitas e despesas
- **ContaBancaria**: Informações bancárias
- **Terceiros**: Credores e devedores
- **Extrato**: Histórico de transações

---

## ?? Segurança

- ? Autenticação Cookie-based com expiração configurável
- ? Senhas hasheadas (nunca em texto plano)
- ? HTTPS obrigatório em produção
- ? Anti-CSRF tokens em formulários
- ? SQL Injection protection via Entity Framework
- ? XSS protection via Razor encoding
- ? Azure AD integration

---

## ?? Deploy no Azure

### Deploy Automático (CI/CD)

O projeto possui workflow do GitHub Actions:
- **Push para `main`** ? Deploy automático
- **Build** ? Testa compilação
- **Migrations** ? Aplica no banco
- **Deploy** ? Publica no App Service

### Configurar Secrets no GitHub

1. Acesse: **Settings ? Secrets ? Actions**
2. Adicione:
   - `AZURE_WEBAPP_PUBLISH_PROFILE`
   - `AZURE_SQL_CONNECTIONSTRING` (opcional)

---

## ?? Roadmap

### ? Concluído (v2.0 - Janeiro 2025)

- [x] Migração para .NET 8
- [x] Redesign completo da interface
- [x] Sistema de modais
- [x] Autenticação moderna
- [x] Deploy no Azure
- [x] CI/CD com GitHub Actions

### ?? Planejado (v2.1+)

- [ ] Gráficos e dashboards avançados
- [ ] Exportação de relatórios (Excel/PDF)
- [ ] App mobile
- [ ] Multi-idioma
- [ ] Modo escuro

---

## ?? Contribuindo

Contribuições são bem-vindas!

1. Fork o projeto
2. Crie uma branch (`git checkout -b feature/nova-funcionalidade`)
3. Commit suas mudanças (`git commit -m 'feat: adiciona X'`)
4. Push para a branch (`git push origin feature/nova-funcionalidade`)
5. Abra um Pull Request

### Padrões de Commit

Usamos [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: nova funcionalidade
fix: correção de bug
docs: documentação
style: formatação
refactor: refatoração
test: testes
chore: manutenção
```

---

## ?? Licença

Este projeto está sob a licença **MIT**. Veja [LICENSE](LICENSE) para mais detalhes.

---

## ?? Equipe

### Desenvolvimento Original (2023)
**Projeto Integrador SENAC**
- Desenvolvido como trabalho de conclusão de curso

### Modernização (2025)
**necromod**
- Reestruturação completa
- Migração para .NET 8
- Redesign da interface
- Deploy no Azure

---

## ?? Contato

- **GitHub**: [@necromod](https://github.com/necromod)
- **Issues**: [GitHub Issues](https://github.com/necromod/Prospera/issues)
- **Website**: [Prospera no Azure](https://prosperaapp.azurewebsites.net)

---

## ?? Agradecimentos

- **SENAC** - Formação técnica e suporte ao projeto original
- **Microsoft** - Documentação do .NET e Azure
- **Comunidade Open Source** - Bibliotecas e ferramentas
- **AwesomeAPI** - API de cotações
- **IBGE** - API de notícias

---

<div align="center">

**Prospera** - Gerencie suas finanças com inteligência ??

Desenvolvido com ?? em 2023 e modernizado em 2025

[? Voltar ao topo](#-prospera---sistema-de-gestão-financeira)

</div>
