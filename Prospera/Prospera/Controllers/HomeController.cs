using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prospera.Models;
using System.Diagnostics;

namespace Prospera.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        /*public IActionResult MenuUsuario()
        {
            return View();
        }*/
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
