using Microsoft.AspNetCore.Authorization;

namespace DomainDrivenERP.Identity.Filters;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
