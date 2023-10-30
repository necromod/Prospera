using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class CadastroController : Controller
    {
        private readonly SessaoInterface _sessao;
        private readonly ProsperaContext _context;


        public CadastroController(ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
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
        public IActionResult Cadastrar(Usuario usuario)
        {

            //Iserção automática dos campos 
            usuario.DatCadastroUsuario = DateTime.Now;
            usuario.DatUltimoAcesUsuario = DateTime.Now;
            usuario.CargoUsuario = "Comum";
            usuario.StatusUsuario = "Ativo";

            //Criação do usuário dentro do banco
            _context.Usuario.Add(usuario);
            _context.SaveChanges();

            // Cria um objeto LoginModel com o email e senha do novo usuário
            LoginModel loginModel = new LoginModel
            {
                Email = usuario.EmailUsuario,
                Senha = usuario.SenhaUsuario
            };

            // Chama o método Entrar diretamente no LoginController
            var loginController = new LoginController(_context, _sessao);
            return loginController.Entrar(loginModel);
        }


    }
}
