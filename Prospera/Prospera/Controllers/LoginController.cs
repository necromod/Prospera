using Microsoft.AspNetCore.Mvc;
using Prospera.Data;
using Prospera.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prospera.Helpers;
using Newtonsoft.Json;

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
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Home");
        }       


        //Método para fazer Login de usuário
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // validação se o email existe no banco
                    var usuario = _context.Usuario.SingleOrDefault(u => u.EmailUsuario == loginModel.Email);

                    if (usuario != null)
                    {
                        // O email existe, verifique a senha
                        if (usuario.SenhaUsuario == loginModel.Senha)
                        {
                            //Criação de Cookies de login
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("MenuUsuario", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Senha incorreta. Tenta novamente";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Email não cadastrado";
                    }
                }
            }
            catch (Exception erro)
            {
                // Se houver algum erro inesperado
                ModelState.AddModelError(string.Empty, "Ocorreu um erro durante o login");
            }
            return View("Login");
        }
        
    }
}
