# ?? DOCUMENTAÇÃO: PROCESSO DE LOGIN E SESSÃO - PROSPERA

## ?? ÍNDICE
1. [Visão Geral](#visão-geral)
2. [Fluxo Completo de Autenticação](#fluxo-completo-de-autenticação)
3. [Mecanismos de Autenticação](#mecanismos-de-autenticação)
4. [Como a Sessão é Mantida](#como-a-sessão-é-mantida)
5. [Componentes Envolvidos](#componentes-envolvidos)
6. [Análise Detalhada do Código](#análise-detalhada-do-código)
7. [Diagrama de Sequência](#diagrama-de-sequência)
8. [Problemas e Melhorias](#problemas-e-melhorias)

---

## ?? VISÃO GERAL

O sistema Prospera utiliza **DUAS formas paralelas** de autenticação:

1. **?? Cookie Authentication (ASP.NET Core Identity)** ? Principal
2. **?? Session Storage (HttpSession)** ? Fallback/Redundância

### ?? Observação Importante
A implementação atual é **redundante** e mistura dois sistemas de autenticação, o que pode causar confusão. O ideal seria usar **apenas Cookie Authentication**.

---

## ?? FLUXO COMPLETO DE AUTENTICAÇÃO

### 1?? **Usuário Acessa a Página de Login**

```
GET /Login/Login
    ?
LoginController.Login()
    ?
Verifica: _sessao.BuscarSessaoUsuario() != null ?
    ?
    ?? SIM ? RedirectToAction("MenuUsuario", "Home")
    ?? NÃO ? return View() [Exibe tela de login]
```

**Arquivo:** `Controllers/LoginController.cs` (linhas 26-33)
```csharp
public IActionResult Login()
{
    //Se usuário já estiver logado, redirecionar para MenuUsuario
    if(_sessao.BuscarSessaoUsuario() != null)
    {
        return RedirectToAction("MenuUsuario", "Home");
    }
    return View();
}
```

---

### 2?? **Usuário Preenche Email/Senha e Clica "LOGAR"**

**View:** `Views/Login/Login.cshtml` (linha 58)
```html
<form asp-controller="Login" asp-action="Entrar" method="post">
    <input asp-for="Email" type="text" placeholder="Email" />
    <input asp-for="Senha" type="password" placeholder="Senha" />
    <input type="checkbox" name="manterLogado" id="ChekManterLogado"> Manter logado?
    <button type="submit">LOGAR</button>
</form>
```

**POST enviado para:** `/Login/Entrar`

**Dados enviados:**
- `Email`: string
- `Senha`: string
- `manterLogado`: "on" (se checkbox marcado) ou null

---

### 3?? **Processamento do Login**

**Arquivo:** `Controllers/LoginController.cs` (linhas 48-110)

#### **PASSO A: Validação do ModelState**
```csharp
if (ModelState.IsValid)
{
    // Continua...
}
```

#### **PASSO B: Busca do Usuário no Banco**
```csharp
var usuario = _context.Usuario?.SingleOrDefault(u => u.EmailUsuario == loginModel.Email);
```

**?? PROBLEMA DE SEGURANÇA:**
- Senha é armazenada em **TEXTO PLANO** no banco
- Comparação direta: `loginModel.Senha == usuario.SenhaUsuario`
- **Deveria usar:** BCrypt, PBKDF2 ou Argon2 para hash de senhas

#### **PASSO C: Verificação da Senha**
```csharp
if (usuario != null)
{
    if (loginModel.Senha == usuario.SenhaUsuario) 
    {
        // Login válido ? Continua para PASSO D
    }
    else 
    { 
        TempData["MensagemErro"] = $"Senha incorreta. Tenta novamente"; 
    }
}
else
{
    TempData["MensagemErro"] = $"Email não cadastrado...";
}
```

#### **PASSO D: Criação da Autenticação (SE VÁLIDO)**

##### D.1 - Verificar se "Manter Logado" foi marcado
```csharp
bool manterLogado = HttpContext.Request.Form["manterLogado"] == "on" ? true : false;
```

##### D.2 - Definir tempo de expiração
```csharp
var tempoExpiracaoSessao = manterLogado ? TimeSpan.FromDays(15) : TimeSpan.FromMinutes(5);
```

##### D.3 - Criar Claims (dados do usuário no cookie)
```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
    new Claim(ClaimTypes.Name, usuario.NomeUsuario),
    new Claim(ClaimTypes.Email, usuario.EmailUsuario ?? "")
};
```

**?? Claims criadas:**
| Claim Type | Valor | Uso |
|------------|-------|-----|
| `ClaimTypes.NameIdentifier` | ID do usuário (ex: "123") | Identificador único |
| `ClaimTypes.Name` | Nome completo | Exibição no sistema |
| `ClaimTypes.Email` | Email | Referência/auditoria |

##### D.4 - Criar ClaimsIdentity e AuthenticationProperties
```csharp
var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

var authProperties = new AuthenticationProperties
{
    IsPersistent = manterLogado,  // Cookie persiste entre sessões do navegador
    ExpiresUtc = DateTimeOffset.UtcNow.Add(tempoExpiracaoSessao)
};
```

##### D.5 - **?? CRIAR O COOKIE DE AUTENTICAÇÃO**
```csharp
await HttpContext.SignInAsync(
    CookieAuthenticationDefaults.AuthenticationScheme,
    new ClaimsPrincipal(claimsIdentity),
    authProperties
);
```

**O que acontece aqui:**
- ASP.NET Core cria um **cookie criptografado** no navegador
- Nome do cookie: `.AspNetCore.Cookies` (padrão)
- Contém as Claims do usuário
- HttpOnly = true (não acessível via JavaScript)
- Secure (se HTTPS)
- Expira conforme `ExpiresUtc`

##### D.6 - **?? CRIAR SESSÃO REDUNDANTE (HttpSession)**
```csharp
HttpContext.Session.SetString("SessaoExpiracao", DateTime.Now.Add(tempoExpiracaoSessao).ToString());

_sessao.CriarSessaoUsuario(usuario);  // Serializa Usuario em JSON e guarda na Session
```

**Arquivo:** `Helpers/Sessao.cs` (linhas 52-57)
```csharp
public void CriarSessaoUsuario(Usuario usuariomodel)
{
    if (_httpContext.HttpContext == null) return;
    string valor = JsonConvert.SerializeObject(usuariomodel);
    _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
}
```

**?? REDUNDÂNCIA:** Isso duplica os dados do usuário:
- **Cookie** ? Claims com ID, Nome, Email
- **Session** ? Objeto Usuario completo serializado em JSON

##### D.7 - Redirecionar para MenuUsuario
```csharp
return RedirectToAction("MenuUsuario", "Home");
```

---

## ??? MECANISMOS DE AUTENTICAÇÃO

### 1. **Cookie Authentication (Principal)**

**Configuração:** `Program.cs` (linhas 48-56)
```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/Login";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(15);
    });
```

**Como funciona:**
1. Ao fazer `SignInAsync()`, o ASP.NET Core cria um cookie criptografado
2. A cada requisição, o middleware de autenticação:
   - Lê o cookie
   - Descriptografa
   - Popula `HttpContext.User` com as Claims
3. Controllers com `[Authorize]` verificam se `HttpContext.User.Identity.IsAuthenticated == true`

**Vantagens:**
- ? Padrão do ASP.NET Core
- ? Seguro (criptografado)
- ? Suporta `[Authorize]`
- ? Integrado com Identity

---

### 2. **Session Storage (Redundante)**

**Configuração:** `Program.cs` (linhas 72-77)
```csharp
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromMinutes(5);
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});
```

**Como funciona:**
1. `CriarSessaoUsuario()` serializa o objeto `Usuario` em JSON
2. Armazena no HttpSession com chave `"SessaoUsuarioLogado"`
3. HttpSession usa um **cookie de sessão** (`.AspNetCore.Session`) contendo um ID
4. Os dados ficam armazenados **no servidor** (memória ou cache)

**Problemas:**
- ? Duplicação desnecessária
- ? Pode causar inconsistências (ex: cookie válido, mas session expirou)
- ? Session tem timeout de 5 minutos (diferente do cookie de 15 dias)

---

## ?? COMO A SESSÃO É MANTIDA

### **Verificação de Autenticação**

**Arquivo:** `Helpers/Sessao.cs` (método `BuscarSessaoUsuario()`)

```csharp
public Usuario? BuscarSessaoUsuario()
{
    // 1?? PRIORIDADE: Buscar nas Claims (Cookie Authentication)
    var claimId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!string.IsNullOrEmpty(claimId) && int.TryParse(claimId, out var id))
    {
        if (_userProvider != null)
        {
            var u = _userProvider.GetUserById(id);  // Busca no banco
            if (u != null) return u;
        }
    }

    // 2?? FALLBACK: Buscar na Session
    string? sessaoUsuario = _httpContext.HttpContext?.Session.GetString("SessaoUsuarioLogado");
    if (!string.IsNullOrEmpty(sessaoUsuario))
    {
        Usuario? usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
        return usuario;
    }

    // 3?? Nenhuma autenticação encontrada
    return null;
}
```

**Ordem de verificação:**
1. **Claims (Cookie)** ? Se encontrado, busca usuário no banco por ID
2. **Session (HttpSession)** ? Se encontrado, deserializa JSON
3. **Null** ? Usuário não autenticado

---

### **Middleware Pipeline**

A cada requisição, o ASP.NET Core executa (em ordem):

```
Request
  ?
1. UseStaticFiles()
  ?
2. UseRouting()
  ?
3. UseAuthentication()  ? Lê cookie e popula HttpContext.User
  ?
4. UseAuthorization()   ? Verifica [Authorize] attributes
  ?
5. UseSession()         ? Carrega Session do cache
  ?
6. Controller Action
  ?
Response
```

**Configuração:** `Program.cs` (linhas 117-121)
```csharp
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  // ? IMPORTANTE: antes de UseAuthorization
app.UseAuthorization();
app.UseSession();
```

---

## ?? COMPONENTES ENVOLVIDOS

### 1. **LoginController**
- **Responsabilidade:** Gerenciar autenticação
- **Actions:**
  - `Login()` [GET] ? Exibe tela de login
  - `Entrar()` [POST] ? Processa login
  - `Sair()` [GET] ? Faz logout

### 2. **SessaoInterface / Sessao**
- **Responsabilidade:** Abstração para gerenciar "sessão" do usuário
- **Métodos:**
  - `BuscarSessaoUsuario()` ? Retorna usuário autenticado (ou null)
  - `CriarSessaoUsuario()` ? Armazena usuário na Session
  - `RemoverSessaoUsuario()` ? Remove da Session

### 3. **UserProvider / IUserProvider**
- **Responsabilidade:** Buscar usuário no banco por ID
- **Implementação:** `Helpers/UserProvider.cs`

```csharp
public Usuario? GetUserById(int id)
{
    try
    {
        return _context.Usuario?.FirstOrDefault(u => u.IdUsuario == id);
    }
    catch
    {
        return null;
    }
}
```

### 4. **Program.cs**
- Configura Authentication
- Configura Session
- Configura Middleware Pipeline

### 5. **Models**
- `LoginModel` ? DTO para formulário de login
- `Usuario` ? Entidade do banco de dados

---

## ?? DIAGRAMA DE SEQUÊNCIA

```
????????     ??????????????     ????????????     ????????????     ???????????
? User ?     ?  Browser   ?     ?Controller?     ?  Sessao  ?     ?Database ?
????????     ??????????????     ????????????     ????????????     ???????????
   ?                ?                 ?                ?                ?
   ? Clica "LOGIN"  ?                 ?                ?                ?
   ????????????????>?                 ?                ?                ?
   ?                ? GET /Login/Login?                ?                ?
   ?                ?????????????????>?                ?                ?
   ?                ?                 ? BuscarSessao() ?                ?
   ?                ?                 ????????????????>?                ?
   ?                ?                 ?     null       ?                ?
   ?                ?                 ?<????????????????                ?
   ?                ?   View(Login)   ?                ?                ?
   ?                ?<?????????????????                ?                ?
   ?  Tela de Login ?                 ?                ?                ?
   ?<????????????????                 ?                ?                ?
   ?                ?                 ?                ?                ?
   ? Digita email/  ?                 ?                ?                ?
   ? senha + LOGAR  ?                 ?                ?                ?
   ????????????????>?                 ?                ?                ?
   ?                ?POST/Login/Entrar?                ?                ?
   ?                ?????????????????>?                ?                ?
   ?                ?                 ?  Busca usuario ?                ?
   ?                ?                 ????????????????????????????????>?
   ?                ?                 ?    Usuario     ?                ?
   ?                ?                 ?<????????????????????????????????
   ?                ?                 ? Valida senha   ?                ?
   ?                ?                 ? (texto plano)  ?                ?
   ?                ?                 ?                ?                ?
   ?                ?                 ? SignInAsync()  ?                ?
   ?                ?                 ? [Cria Cookie]  ?                ?
   ?                ?                 ?                ?                ?
   ?                ?                 ?CriarSessao(usr)?                ?
   ?                ?                 ????????????????>?                ?
   ?                ?                 ?  [Session SET] ?                ?
   ?                ?                 ?                ?                ?
   ?                ? Redirect+Cookie ?                ?                ?
   ?                ?<?????????????????                ?                ?
   ? Cookie armazen.?                 ?                ?                ?
   ?<????????????????                 ?                ?                ?
   ?                ?                 ?                ?                ?
   ?                ?GET/Home/MenuUsr ?                ?                ?
   ?                ?????????????????>? [Cookie lido   ?                ?
   ?                ?                 ?  automaticam.] ?                ?
   ?                ?                 ?                ?                ?
   ?                ?                 ? BuscarSessao() ?                ?
   ?                ?                 ????????????????>?                ?
   ?                ?                 ?  GetUserById() ?                ?
   ?                ?                 ?                ????????????????>?
   ?                ?                 ?     Usuario    ?                ?
   ?                ?                 ?<????????????????<????????????????
   ?                ?   View(Menu)    ?                ?                ?
   ?                ?<?????????????????                ?                ?
   ?  Menu Principal?                ?                ?                ?
   ?<????????????????                 ?                ?                ?
```

---

## ?? PROBLEMAS E MELHORIAS

### ?? Problemas Críticos de Segurança

#### 1. **Senhas em Texto Plano**
```csharp
if (loginModel.Senha == usuario.SenhaUsuario)  // ? TEXTO PLANO
```

**Solução:**
```csharp
// Na criação do usuário:
usuario.SenhaUsuario = BCrypt.Net.BCrypt.HashPassword(senhaTextoPlano);

// No login:
if (BCrypt.Net.BCrypt.Verify(loginModel.Senha, usuario.SenhaUsuario))
{
    // Login válido
}
```

#### 2. **Falta de Rate Limiting**
- Sem proteção contra ataques de força bruta
- **Solução:** Implementar `AspNetCoreRateLimit` ou usar middleware customizado

#### 3. **Sem HTTPS Enforced**
- Credenciais podem ser interceptadas
- **Solução:** Adicionar `app.UseHsts()` e `app.UseHttpsRedirection()`

---

### ?? Problemas de Arquitetura

#### 1. **Redundância Cookie + Session**
```csharp
// ? Duplicação desnecessária
await HttpContext.SignInAsync(...);  // Cookie
_sessao.CriarSessaoUsuario(usuario); // Session
```

**Por que é problema:**
- Cookie tem duração de 15 dias
- Session tem timeout de 5 minutos
- Se Session expirar, mas Cookie ainda válido ? inconsistência

**Solução:** Usar **APENAS Cookie Authentication**
```csharp
// REMOVER toda a parte de Session
// Usar apenas Claims do cookie
var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
```

#### 2. **BuscarSessaoUsuario() faz Query no Banco**
```csharp
public Usuario? BuscarSessaoUsuario()
{
    var claimId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!string.IsNullOrEmpty(claimId) && int.TryParse(claimId, out var id))
    {
        var u = _userProvider.GetUserById(id);  // ? QUERY A CADA CHAMADA
        if (u != null) return u;
    }
    // ...
}
```

**Problema:** Se `BuscarSessaoUsuario()` for chamado múltiplas vezes na mesma requisição, faz múltiplas queries.

**Solução:** Cache ou usar apenas Claims
```csharp
// Opção 1: Cache na requisição
private Usuario? _cachedUser;

public Usuario? BuscarSessaoUsuario()
{
    if (_cachedUser != null) return _cachedUser;
    
    var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (int.TryParse(claimId, out var id))
    {
        _cachedUser = _userProvider.GetUserById(id);
    }
    return _cachedUser;
}

// Opção 2: Usar apenas Claims (mais eficiente)
public string? GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
public string? GetUserName() => User.FindFirst(ClaimTypes.Name)?.Value;
public string? GetUserEmail() => User.FindFirst(ClaimTypes.Email)?.Value;
```

---

### ?? Melhorias Recomendadas

#### 1. **Adicionar Refresh Tokens**
Para sessões longas (15 dias), implementar refresh token para renovar autenticação.

#### 2. **Adicionar Claims Customizados**
```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
    new Claim(ClaimTypes.Name, usuario.NomeUsuario),
    new Claim(ClaimTypes.Email, usuario.EmailUsuario ?? ""),
    new Claim("Cargo", usuario.CargoUsuario),  // ? Claim customizado
    new Claim(ClaimTypes.Role, "Usuario")      // ? Role para autorização
};
```

#### 3. **Implementar Autorização Baseada em Roles**
```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminPanel() { ... }
```

#### 4. **Adicionar Logging de Autenticação**
```csharp
_logger.LogInformation("Login bem-sucedido: {Email} | IP: {IP}", 
    loginModel.Email, 
    HttpContext.Connection.RemoteIpAddress);
```

#### 5. **Implementar "Esqueceu a Senha"**
Atualmente o link existe mas não funciona:
```html
<a href="#" id="BtnEsqSenha">Esqueceu a senha?</a>
```

#### 6. **Adicionar Two-Factor Authentication (2FA)**

---

## ?? RESUMO

### ? O que está funcionando:
- ? Cookie Authentication configurado corretamente
- ? Claims sendo criadas com dados do usuário
- ? `[Authorize]` protegendo rotas
- ? Logout limpando cookie e session
- ? "Manter logado" funcional

### ?? O que precisa ser corrigido:
- ? **CRÍTICO:** Senhas em texto plano
- ? **CRÍTICO:** Sem HTTPS enforced
- ?? Redundância Cookie + Session
- ?? `BuscarSessaoUsuario()` faz queries desnecessárias
- ?? Session timeout diferente do Cookie

### ?? Recomendação:

**Simplificar arquitetura:**
1. Remover toda lógica de HttpSession
2. Usar apenas Cookie Authentication (Claims)
3. Implementar hash de senhas (BCrypt)
4. Adicionar HTTPS redirect
5. Implementar rate limiting

---

**Data da Documentação:** 2025
**Versão:** 1.0
**Status:** ? Documentação completa do fluxo atual
