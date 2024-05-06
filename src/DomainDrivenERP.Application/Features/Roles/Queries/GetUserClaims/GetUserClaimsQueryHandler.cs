using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetUserClaims;
internal class GetUserClaimsQueryHandler : IListQueryHandler<GetUserClaimsQuery, string>
{
    private readonly IRoleService _roleService;

    public GetUserClaimsQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<Result<CustomList<string>>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetUserClaimsAsync(request.UserId, cancellationToken);
    }
}
