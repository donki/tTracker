using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace tTrackerWeb.Helpers
{

    public class TokenAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private TokenManager tokenManager;

        public TokenAuthorizationFilter(TokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Accede a la solicitud y verifica el token aquí
            var token = GetTokenFromRequest(context.HttpContext.Request);

            if (!tokenManager.ExistsToken(token) || tokenManager.IsTokenExpired(token))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                //context.Result = new OkResult();
            }
        }

        private string GetTokenFromRequest(HttpRequest request)
        {
            string authorizationHeader = request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            // Extraer el token después de "Bearer "
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

    }
}
