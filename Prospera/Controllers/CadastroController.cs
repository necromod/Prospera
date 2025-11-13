using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Prospera.Controllers
{
    public class CadastroController : Controller
    {
        private readonly SessaoInterface _sessao;
        private readonly ProsperaContext _context;
        private readonly ILogger<CadastroController> _logger;

        public CadastroController(ProsperaContext context, SessaoInterface sessao, ILogger<CadastroController> logger)
        {
            _context = context;
            _sessao = sessao;
            _logger = logger;
        }

        //View /Home/Cadastro
        public ActionResult Cadastro()
        {
            //Se usuário já estiver logado, redirecionar para MenuUsuario
            if (_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("MenuUsuario", "Home");
            }
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Usuario usuario)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrEmpty(usuario.EmailUsuario) || 
                    string.IsNullOrEmpty(usuario.SenhaUsuario) ||
                    string.IsNullOrEmpty(usuario.NomeUsuario))
                {
                    TempData["MensagemErro"] = "Por favor, preencha todos os campos obrigatórios.";
                    return View("Cadastro");
                }

                // Inserção automática dos campos 
                usuario.DatCadastroUsuario = DateTime.Now;
                usuario.DatUltimoAcesUsuario = DateTime.Now;
                usuario.CargoUsuario = "Comum";
                usuario.StatusUsuario = "Ativo";

                // Criação do usuário dentro do banco
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Novo usuário cadastrado: {Email}", usuario.EmailUsuario);

                // ✅ AUTENTICAÇÃO COMPLETA APÓS CADASTRO
                // 1. Criar Claims (mesmo fluxo do LoginController)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario),
                    new Claim(ClaimTypes.Email, usuario.EmailUsuario ?? "")
                };

                // 2. Configurar autenticação (manter logado por 15 dias após cadastro)
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15)
                };

                // 3. Criar cookie de autenticação
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // 4. Criar sessão (redundante, mas mantém compatibilidade)
                HttpContext.Session.SetString("SessaoExpiracao", DateTime.Now.AddDays(15).ToString());
                _sessao.CriarSessaoUsuario(usuario);

                _logger.LogInformation("Usuário autenticado após cadastro: {Email}", usuario.EmailUsuario);

                // 5. Redirecionar para MenuUsuario
                return RedirectToAction("MenuUsuario", "Home");
            }
            catch (Exception erro)
            {
                _logger.LogError(erro, "Erro ao cadastrar usuário: {Email}", usuario?.EmailUsuario);
                TempData["MensagemErro"] = "Ocorreu um erro ao criar sua conta. Tente novamente.";
                return View("Cadastro");
            }
        }

        [HttpPost]
        public JsonResult VerificarCPFDuplicado(string cpf)
        {
            var usuarioExistente = _context.Usuario.Any(u => u.CPFUsuario == cpf);
            return Json(!usuarioExistente);
        }

        [HttpPost]
        public JsonResult VerificarEmailDuplicado(string email)
        {
            var usuarioExistente = _context.Usuario.Any(u => u.EmailUsuario == email);
            return Json(!usuarioExistente);
        }
    }
}
