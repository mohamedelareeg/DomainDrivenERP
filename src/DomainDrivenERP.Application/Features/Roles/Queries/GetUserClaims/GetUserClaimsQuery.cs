using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetUserClaims;
public class GetUserClaimsQuery : IListQuery<string>
{
    public string UserId { get; set; }
}
