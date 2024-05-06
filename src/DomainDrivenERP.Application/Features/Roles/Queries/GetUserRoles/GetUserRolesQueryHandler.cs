using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetUserRoles;
internal class GetUserRolesQueryHandler : IListQueryHandler<GetUserRolesQuery, string>
{
    private readonly IRoleService _roleService;

    public GetUserRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public Task<Result<CustomList<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        return _roleService.GetUserRolesAsync(request.UserId, cancellationToken);
    }
}
