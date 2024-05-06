using Microsoft.AspNetCore.Identity;

namespace DomainDrivenERP.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Code { get; set; }

}
