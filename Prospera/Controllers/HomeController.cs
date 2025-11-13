using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prospera.Helpers;
using Prospera.Models;
using Prospera.Data;
using System.Diagnostics;

namespace Prospera.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProsperaContext _context;
        private readonly SessaoInterface _sessao;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProsperaContext context, SessaoInterface sessao)
        {
            _context = context;
            _sessao = sessao;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [Authorize]
        public IActionResult MenuUsuario()
        {
            _logger.LogInformation("User {UserId} accessed MenuUsuario", User.Identity?.Name);
            return View();
        }
        public IActionResult VariasExibicoes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CarregarMenu()
        {
            return ViewComponent("Menu");
        }

        public IActionResult CarregarViewComponent(string viewComponentName)
        {
            return ViewComponent(viewComponentName);
        }
        public IActionResult carregarViewContas(string viewComponentContas)
        {
            return ViewComponent(viewComponentContas);
        }


    }
}
