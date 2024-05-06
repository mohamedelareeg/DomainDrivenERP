using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Commands.DeleteRole;
internal class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _roleService.DeleteRoleAsync(request.RoleName, cancellationToken);
    }
}
