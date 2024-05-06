using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetAllRoles;
internal class GetAllRolesQueryHandler : IListQueryHandler<GetAllRolesQuery, string>
{
    private readonly IRoleService _roleService;

    public GetAllRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<Result<CustomList<string>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetAllRolesAsync(cancellationToken);
    }
}
