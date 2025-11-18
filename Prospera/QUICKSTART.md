# ?? Quick Start - Prospera

Guia rápido para desenvolvedores iniciarem com o projeto.

---

## ? Configuração Rápida (5 minutos)

### 1. Clone e Restaure

```bash
git clone https://github.com/necromod/Prospera.git
cd Prospera/Prospera
dotnet restore
```

### 2. Configure o Banco (escolha uma opção)

#### Opção A: SQL Server Local
Edite `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "ProsperaContext": "Server=localhost;Database=ProsperaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

#### Opção B: Azure SQL
```bash
dotnet user-secrets set "ConnectionStrings:ProsperaContext" "SUA_CONNECTION_STRING_AQUI"
```

### 3. Crie o Banco

```bash
dotnet ef database update
```

### 4. Execute

```bash
dotnet run
```

Acesse: `https://localhost:5041`

---

## ?? Estrutura Rápida

```
Controllers/  ? Lógica de negócio
Models/       ? Entidades do banco
Views/        ? Interface (Razor)
wwwroot/      ? CSS, JS, imagens
Data/         ? DbContext
```

---

## ?? Fluxos Principais

### Cadastro + Login
1. `Home/Index` ? Página inicial
2. `Cadastro/Cadastro` ? Criar conta
3. `Login/Login` ? Fazer login
4. `Home/MenuUsuario` ? Dashboard

### CRUD Financeiro
1. Dashboard ? Clique no menu lateral
2. Modal abre ? Preencha formulário
3. Cadastrar ? Salva e fecha
4. Dashboard atualizado

---

## ?? Comandos Úteis

```bash
# Build
dotnet build

# Run
dotnet run

# Watch (hot reload)
dotnet watch run

# Migrations
dotnet ef migrations add NomeDaMigracao
dotnet ef database update

# Limpar
dotnet clean

# Publicar
dotnet publish -c Release
```

---

## ?? Personalização Rápida

### Cores
Edite `wwwroot/css/MenuUsuario.css`:
```css
:root {
    --primary-color: #7836FA;  /* Mude aqui */
}
```

### Logo
Substitua `wwwroot/img/Favicon.png`

### Textos
Edite os arquivos `.cshtml` em `Views/`

---

## ?? Problemas Comuns

### Erro de Connection String
```bash
dotnet user-secrets set "ConnectionStrings:ProsperaContext" "SUA_STRING"
```

### Porta em uso
Mude em `Properties/launchSettings.json`:
```json
"applicationUrl": "https://localhost:NOVA_PORTA"
```

### Banco não existe
```bash
dotnet ef database update --verbose
```

---

## ?? Recursos

- **README.md** - Documentação completa
- **CHANGELOG.md** - Histórico de mudanças
- [Documentação .NET](https://docs.microsoft.com/dotnet/)
- [Entity Framework](https://docs.microsoft.com/ef/)

---

## ?? Dicas

1. Use `dotnet watch run` para hot reload
2. Explore o código com F12 (Go to Definition)
3. Teste em diferentes tamanhos de tela (F12 ? Device Toolbar)
4. Veja logs no terminal durante execução
5. Use Git para versionar suas mudanças

---

## ?? Contribuir

1. Fork o projeto
2. Crie uma branch: `git checkout -b minha-feature`
3. Commit: `git commit -m 'feat: minha feature'`
4. Push: `git push origin minha-feature`
5. Abra um Pull Request

---

**Dúvidas?** Abra uma [issue no GitHub](https://github.com/necromod/Prospera/issues)
