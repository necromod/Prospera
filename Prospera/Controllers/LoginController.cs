using Microsoft.AspNetCore.Mvc;
using Prospera.Data;
using Prospera.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Prospera.Helpers;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Prospera.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;


        public LoginController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        public IActionResult Login()
        {
            //Se usuário já estiver logado, redirecionar para MenuUsuario
            if(_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("MenuUsuario", "Home");
            }
            return View();
        }

        //Método para limpar sessão de usuário
        public async Task<IActionResult> Sair()
        {
            _sessao.RemoverSessaoUsuario();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


        //Método para fazer Login de usuário
        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Validação se o email existe no banco
                    var usuario = _context.Usuario?.SingleOrDefault(u => u.EmailUsuario == loginModel.Email);

                    if (usuario == null)
                    {
                        TempData["MensagemErro"] = "Email não cadastrado. Tente novamente ou cadastre-se";
                        return View("Login", loginModel);
                    }

                    // Validação de senha
                    if (loginModel.Senha != usuario.SenhaUsuario)
                    {
                        TempData["MensagemErro"] = "Senha incorreta. Tente novamente";
                        return View("Login", loginModel);
                    }

                    // Login bem-sucedido
                    if (HttpContext != null)
                    {
                        // Verifique se a caixa "Manter logado" foi marcada
                        bool manterLogado = HttpContext.Request.Form["manterLogado"] == "on";

                        // Configurar o tempo de expiração da sessão com base na escolha do usuário
                        var tempoExpiracaoSessao = manterLogado ? TimeSpan.FromDays(15) : TimeSpan.FromMinutes(20);

                        // Cria claims e realiza o SignIn com cookie
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                            new Claim(ClaimTypes.Name, usuario.NomeUsuario),
                            new Claim(ClaimTypes.Email, usuario.EmailUsuario ?? "")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = manterLogado,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(tempoExpiracaoSessao)
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        //Configurar a sessão com o tempo de expiração especificado
                        HttpContext.Session.SetString("SessaoExpiracao", DateTime.Now.Add(tempoExpiracaoSessao).ToString());

                        //Criação de Sessão de usuário
                        _sessao.CriarSessaoUsuario(usuario);

                        return RedirectToAction("MenuUsuario", "Home");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Erro ao processar login. Tente novamente";
                        return View("Login", loginModel);
                    }
                }
                else
                {
                    // ModelState inválido
                    TempData["MensagemErro"] = "Por favor, preencha todos os campos corretamente";
                    return View("Login", loginModel);
                }
            }
            catch (Exception ex)
            {
                // Se houver algum erro inesperado
                TempData["MensagemErro"] = $"Ocorreu um erro durante o login: {ex.Message}";
                return View("Login", loginModel);
            }
        }
    }
}
