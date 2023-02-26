using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Eproject3.Models;
using Eproject3.Repositories.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Eproject3.middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
       

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthentication _authRepository)
        {
            var token = context.Request.Cookies["jwtToken"];
            if (token != null)
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();

                
                var user = _authRepository.GetUserFromToken(token);
                if (user == null)
                {
                    context.Response.Cookies.Delete("jwtToken");
                    context.Response.Redirect("/Account/Login");
                    return;
                }
                context.Items["User"] = user;
                context.Request.Headers.Add("Authorization", $"Bearer {token}");

            }

            await _next(context);
        }

        private static AuthorizeAttribute GetAuthorizeAttribute(HttpContext context)
        {
            var action = context.GetEndpoint()?.Metadata?.GetMetadata<ControllerActionDescriptor>();
            if (action != null)
            {
                return action.MethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            }
            return null;
        }

    }

}
