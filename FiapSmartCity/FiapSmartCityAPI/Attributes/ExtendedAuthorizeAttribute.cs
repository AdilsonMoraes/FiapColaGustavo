using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FiapSmartCityServices.Authentication;
using System.Data;

namespace FiapSmartCityAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ExtendedAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string[] Roles { get; }

        public ExtendedAuthorizeAttribute(string[] roles)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticationService = context.HttpContext.RequestServices.GetService<IAuthenticationService>();
            if (authenticationService != null)
            {
                var isAdmin = authenticationService.IsAdmin().Result;
                if (!isAdmin)
                {
                    var userComplete = authenticationService.GetUserWithRole().Result;
                    if (userComplete.Roles != null && userComplete?.Roles?.Count() > 0)
                    {
                        var unauthorizedObjectResultEmpty = !userComplete?.Roles?.Where(w => !Roles.Contains(w)).Any() ?? false;
                        if (!unauthorizedObjectResultEmpty)
                            context.Result = new UnauthorizedObjectResult(string.Empty);

                    }
                    else
                    {
                        context.Result = new UnauthorizedObjectResult(string.Empty);
                    }
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
            }
        }
    }
}
