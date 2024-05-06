using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Commands.AssignClaimToUser;
internal class AssignClaimToUserCommandHandler : ICommandHandler<AssignClaimToUserCommand, bool>
{
    private readonly IRoleService _roleService;

    public AssignClaimToUserCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<Result<bool>> Handle(AssignClaimToUserCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.AddClaimToUserAsync(request.UserId, request.ClaimType, request.ClaimValue, cancellationToken);
    }
}
