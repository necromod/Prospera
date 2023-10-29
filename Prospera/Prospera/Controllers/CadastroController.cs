using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Prospera.Controllers
{
    public class CadastroController : Controller
    {
        //View /Home/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

    }
}
