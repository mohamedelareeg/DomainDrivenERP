using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Commands.DeleteRole;
public class DeleteRoleCommand : ICommand<bool>
{
    public string RoleName { get; set; }
}
