using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Extensions;
using Shared.Jwt;
using Shared.Models;

namespace Shared.PermissionRules
{
    public class PermissionRuleAttribute : TypeFilterAttribute
    {
        public PermissionRuleAttribute(params int[] permissions) : base(typeof(GlobalActionFilter))
        {
            Arguments = new object[] { permissions };
        }

        private class GlobalActionFilter : IAsyncActionFilter
        {
            private readonly int[] permissions;
            private Jwt.Jwt jwt;
            public GlobalActionFilter(int[] permissions, Jwt.Jwt jwt)
            {
                this.permissions = permissions;
                this.jwt = jwt;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                try
                {
                    var authHeader = context.HttpContext.Request.Headers.Authorization;

                    if (string.IsNullOrEmpty(authHeader))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    var token = this.jwt.ReadJWTToken(authHeader);

                    int role = token.GetRoleId();

                    if (!this.permissions.Contains(role))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    await next();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
