using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Commands.CreateRole;
internal class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, bool>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return _roleService.CreateRoleAsync(request.RoleName, cancellationToken);
    }
}
