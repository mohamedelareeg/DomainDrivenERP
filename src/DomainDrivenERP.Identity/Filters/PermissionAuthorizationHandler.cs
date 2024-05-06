using Microsoft.AspNetCore.Authorization;

namespace DomainDrivenERP.Identity.Filters;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAuthorizationHandler()
    {

    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null)
            return;

        bool canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");

        if (canAccess)
        {
            context.Succeed(requirement);
            return;
        }
    }
}
