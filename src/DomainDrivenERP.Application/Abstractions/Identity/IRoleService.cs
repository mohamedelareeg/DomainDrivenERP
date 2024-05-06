
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Identity;

public interface IRoleService
{
    Task<Result<bool>> CreateRoleAsync(string roleName, CancellationToken cancellationToken = default);
    Task<Result<bool>> EditRoleAsync(string roleName, string newRoleName, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteRoleAsync(string roleName, CancellationToken cancellationToken = default);
    Task<Result<bool>> AddClaimToRoleAsync(string roleName, string claimType, string claimValue, CancellationToken cancellationToken = default);
    Task<Result<bool>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken = default);
    Task<Result<bool>> AddClaimToUserAsync(string userId, string claimType, string claimValue, CancellationToken cancellationToken = default);
    Task<Result<CustomList<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<CustomList<string>>> GetUserClaimsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<CustomList<string>>> GetRoleClaimsAsync(string roleName, CancellationToken cancellationToken = default);
    Task<Result<CustomList<string>>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<Result<CustomList<string>>> GetAllClaimsAsync(CancellationToken cancellationToken = default);
}
