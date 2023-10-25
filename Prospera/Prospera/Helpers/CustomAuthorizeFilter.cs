using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;
using Prospera.Models;
using Prospera.Controllers;

namespace Prospera.Helpers
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly AuthorizationPolicy _policy;

        public CustomAuthorizeFilter(AuthorizationPolicy policy)
        {
            _policy = policy;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var authorizeResult = authorizationService.AuthorizeAsync(context.HttpContext.User, null, _policy).GetAwaiter().GetResult();

            if (!authorizeResult.Succeeded)
            {
                // Usuário não autorizado, redireciona para a página de login com uma mensagem
                context.Result = new RedirectToActionResult("Login", "Usuario", new { message = "Você precisa estar logado para ter acesso a esta página." });
            }
        }
    }
}
