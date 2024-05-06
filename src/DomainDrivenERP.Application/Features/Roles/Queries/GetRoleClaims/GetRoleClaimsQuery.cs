using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetRoleClaims;
public class GetRoleClaimsQuery : IListQuery<string>
{
    public string RoleName { get; set; }
}
