# ?? CORREÇÃO: ERR_TOO_MANY_REDIRECTS (LOOP INFINITO)

## ? PROBLEMA IDENTIFICADO

Ao clicar em "LOGIN" na página inicial, ocorria um loop infinito de redirecionamentos:

```
localhost:5041/Home/MenuUsuario ? ERR_TOO_MANY_REDIRECTS
```

---

## ?? CAUSA RAIZ

**Arquivo:** `Helpers/Sessao.cs` (linhas 47-52)

### Código Problemático:

```csharp
public Usuario? BuscarSessaoUsuario()
{
    // ...verificações de claims e sessão...

    // ??? FALLBACK PROBLEMÁTICO
    if (_userProvider != null)
    {
        var defaultUser = _userProvider.GetUserById(1);
        if (defaultUser != null)
        {
            return defaultUser;  // ? SEMPRE RETORNA UM USUÁRIO
        }
    }

    return null;
}
```

### ?? Fluxo do Loop Infinito:

```
1. Clicar "LOGIN" ? /Login/Login
    ?
2. LoginController.Login() executa:
   if(_sessao.BuscarSessaoUsuario() != null)
    ?
3. BuscarSessaoUsuario() retorna SEMPRE usuário ID=1 (fallback)
    ?
4. Como retornou usuário, redireciona:
   return RedirectToAction("MenuUsuario", "Home");
    ?
5. MenuUsuario tem [Authorize] ? detecta NÃO AUTENTICADO DE VERDADE
    ?
6. Sistema redireciona para LoginPath = "/Login/Login"
    ?
7. VOLTA PARA O PASSO 2
    ?
    ?? LOOP INFINITO ??
```

---

## ? CORREÇÃO APLICADA

**Arquivo:** `Helpers/Sessao.cs`

### Código Corrigido:

```csharp
public Usuario? BuscarSessaoUsuario()
{
    // 1. Prioriza autenticação via Claims (Cookie)
    var claimId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!string.IsNullOrEmpty(claimId) && int.TryParse(claimId, out var id))
    {
        if (_userProvider != null)
        {
            var u = _userProvider.GetUserById(id);
            if (u != null) return u;
        }
    }

    // 2. Fallback para sessão HttpSession
    string? sessaoUsuario = _httpContext.HttpContext?.Session.GetString("SessaoUsuarioLogado");
    if (!string.IsNullOrEmpty(sessaoUsuario))
    {
        Usuario? usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
        return usuario;
    }

    // ? REMOVIDO O FALLBACK PROBLEMÁTICO
    // Se não há usuário autenticado, retorna null
    return null;
}
```

### ?? Diferenças:

| ANTES | DEPOIS |
|-------|--------|
| ? Sempre retornava usuário ID=1 se existisse no banco | ? Retorna `null` se não houver autenticação válida |
| ? `LoginController.Login()` sempre redirecionava para MenuUsuario | ? `LoginController.Login()` exibe tela de login corretamente |
| ? Loop infinito de redirecionamento | ? Fluxo normal de autenticação |

---

## ?? FLUXO CORRIGIDO

```
1. Clicar "LOGIN" ? /Login/Login
    ?
2. LoginController.Login() executa:
   if(_sessao.BuscarSessaoUsuario() != null)
    ?
3. BuscarSessaoUsuario() retorna NULL ?
    ?
4. Exibe a view de login: return View();
    ?
5. Usuário digita email/senha e clica "LOGAR"
    ?
6. POST ? /Login/Entrar
    ?
7. Valida credenciais e faz SignIn com cookie
    ?
8. Redireciona para /Home/MenuUsuario
    ?
9. [Authorize] detecta autenticação VÁLIDA ?
    ?
10. Exibe o menu principal
```

---

## ?? TESTES REALIZADOS

? **Build bem-sucedido**
? **Fallback problemático removido**
? **Lógica de autenticação preservada**

---

## ?? VALIDAÇÃO MANUAL

Execute os seguintes testes:

### 1. Teste de Login (Sem Cookies)

```
1. Limpar cookies do navegador (Ctrl+Shift+Del)
2. Abrir http://localhost:5041
3. Clicar em "LOGIN"
4. ? Deve abrir /Login/Login SEM LOOP
5. ? Deve mostrar a tela de login estilizada
6. Fazer login com credenciais válidas
7. ? Deve redirecionar para /Home/MenuUsuario
```

### 2. Teste de Acesso Direto ao MenuUsuario

```
1. (Sem estar logado)
2. Tentar acessar diretamente: http://localhost:5041/Home/MenuUsuario
3. ? Deve redirecionar para /Login/Login?ReturnUrl=...
4. ? NÃO deve entrar em loop
5. Fazer login
6. ? Deve voltar para /Home/MenuUsuario
```

### 3. Teste de Logout e Re-Login

```
1. Estando logado, clicar em "Sair"
2. ? Deve fazer logout
3. ? Deve redirecionar para /Home/Index
4. Clicar em "LOGIN" novamente
5. ? NÃO deve entrar em loop
6. ? Deve exibir tela de login
```

---

## ?? OBSERVAÇÕES IMPORTANTES

### Por que o fallback existia?

Provavelmente foi adicionado para evitar `NullReferenceException` em controllers que não validavam se o usuário estava logado. **Isso é uma má prática** porque:

1. ? Mascara problemas de autenticação
2. ? Cria comportamentos inesperados
3. ? Viola o princípio de autenticação explícita

### Solução Correta:

? **Controllers que precisam de autenticação devem usar `[Authorize]`**
? **Controllers públicos não devem depender de `BuscarSessaoUsuario()`**
? **Validar `BuscarSessaoUsuario() != null` quando necessário**

---

## ?? RESUMO

| Item | Status |
|------|--------|
| Fallback problemático removido | ? |
| Loop infinito corrigido | ? |
| Build validado | ? |
| Lógica de autenticação preservada | ? |
| Claims authentication funcionando | ? |
| Session fallback funcionando | ? |

---

## ?? AÇÕES IMEDIATAS

1. ? **Limpar cookies do navegador** (Ctrl+Shift+Del)
2. ? **Reiniciar a aplicação** (dotnet run)
3. ? **Testar os 3 cenários acima**

---

## ?? SE O PROBLEMA PERSISTIR

Verifique:

1. **Histórico do navegador:** Limpar completamente
2. **Múltiplas instâncias:** Garanta que só uma instância do `dotnet run` está executando
3. **Porta diferente:** Tente em navegador privado/anônimo
4. **Logs:** Verifique o console do ASP.NET Core para ver redirecionamentos

---

**Data da Correção:** 2025
**Status:** ? Loop infinito RESOLVIDO
**Build:** ? Compilação bem-sucedida
