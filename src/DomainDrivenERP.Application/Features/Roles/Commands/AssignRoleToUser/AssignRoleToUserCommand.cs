using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Commands.AssignRoleToUser;
public class AssignRoleToUserCommand : ICommand<bool>
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
}
