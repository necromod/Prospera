using Prospera.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prospera.Helpers
{
    public class Sessao : SessaoInterface
    {

        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Usuario BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (!string.IsNullOrEmpty(sessaoUsuario))
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public void CriarSessaoUsuario(Usuario usuariomodel)
        {
            string valor = JsonConvert.SerializeObject(usuariomodel);
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
