using Microsoft.AspNetCore.Identity;

namespace DomainDrivenERP.Persistence.Identity.Entities;
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
