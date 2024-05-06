using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Commands.EditRole;
public class EditRoleCommand : ICommand<bool>
{
    public string RoleName { get; set; }
    public string NewRoleName { get; set; }
}
