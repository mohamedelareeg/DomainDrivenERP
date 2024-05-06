using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Commands.AssignRoleToUser;
internal class AssignRoleToUserCommandHandler : ICommandHandler<AssignRoleToUserCommand, bool>
{
    private readonly IRoleService _roleService;

    public AssignRoleToUserCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<Result<bool>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.AddRoleToUserAsync(request.UserId, request.RoleName, cancellationToken);
    }
}
