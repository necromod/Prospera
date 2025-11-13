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

        public Usuario? BuscarSessaoUsuario()
        {
            // Prefer claims (autenticação via cookie)
            var claimId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(claimId) && int.TryParse(claimId, out var id))
            {
                if (_userProvider != null)
                {
                    var u = _userProvider.GetUserById(id);
                    if (u != null) return u;
                }
            }

            // Fallback: busca na sessão (HttpSession)
            string? sessaoUsuario = _httpContext.HttpContext?.Session.GetString("SessaoUsuarioLogado");

            if (!string.IsNullOrEmpty(sessaoUsuario))
            {
                Usuario? usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
                return usuario;
            }

            // Se não há usuário autenticado, retorna null
            return null;
        }

        public void CriarSessaoUsuario(Usuario usuariomodel)
        {
            if (_httpContext.HttpContext == null) return;
            string valor = JsonConvert.SerializeObject(usuariomodel);
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext?.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
