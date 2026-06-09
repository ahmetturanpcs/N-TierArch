using exampleProject.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace exampleProject.Web.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Kullanıcının kimlik kartındaki (Claims) tüm yetkileri (Permission) buluyoruz.
            // Identity mekanizmasında rollerin claim türü genellikle "Permission" veya "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/custom" olarak tutulabilir.
            // Biz standart 'Permission' tipini arayacağız.
            var hasPermission = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission);

            if (hasPermission)
            {
                context.Succeed(requirement); // Yetki eşleşti, kapıyı aç!
            }

            return Task.CompletedTask;
        }
    }
}