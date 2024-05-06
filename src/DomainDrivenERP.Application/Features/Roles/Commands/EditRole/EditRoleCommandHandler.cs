using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Commands.EditRole;
internal class EditRoleCommandHandler : ICommandHandler<EditRoleCommand, bool>
{
    private readonly IRoleService _roleService;

    public EditRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<Result<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.EditRoleAsync(request.RoleName, request.NewRoleName, cancellationToken);
    }
}
