using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Commands.CreateRole;
public class CreateRoleCommand : ICommand<bool>
{
    public string RoleName { get; set; }
}
