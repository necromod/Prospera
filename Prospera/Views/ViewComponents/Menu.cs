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
            
            // Defina a variável de exibição com base na presença de um usuário na sessão
            bool exibirSelecao1 = !string.IsNullOrEmpty(sessaoUsuario);

            ViewData["ExibirSelecao1"] = exibirSelecao1;

            if (!exibirSelecao1)
            {
                return View();
            }

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
            return View(usuario);
        }
    }
}
