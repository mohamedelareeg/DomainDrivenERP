using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Commands.AddClaimToRole;
public class AddClaimToRoleCommand : ICommand<bool>
{
    public string RoleName { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}
