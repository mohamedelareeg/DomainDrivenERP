using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Roles.Queries.GetUserRoles;
public class GetUserRolesQuery : IListQuery<string>
{
    public string UserId { get; set; }
}
