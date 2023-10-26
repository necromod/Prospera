using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prospera.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Prospera.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
            return View(usuario);
        }
    }
}
