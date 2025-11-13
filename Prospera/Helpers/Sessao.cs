using Prospera.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Prospera.Helpers
{
    public class Sessao : SessaoInterface
    {

        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserProvider? _userProvider;

        public Sessao(IHttpContextAccessor httpContext, IUserProvider? userProvider = null)
        {
            _httpContext = httpContext;
            _userProvider = userProvider;
        }

        public Usuario BuscarSessaoUsuario()
        {
            // Prefer claims
            var claimId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(claimId) && int.TryParse(claimId, out var id))
            {
                if (_userProvider != null)
                {
                    return _userProvider.GetUserById(id);
                }
            }

            string? sessaoUsuario = _httpContext.HttpContext?.Session.GetString("SessaoUsuarioLogado");

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
