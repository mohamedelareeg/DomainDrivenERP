using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Identity;

public interface IIdentityService
{
    Task<Result<string?>> GetUserNameAsync(string userId);

    Task<Result<bool>> IsInRoleAsync(string userId, string role);

    Task<Result<bool>> AuthorizeAsync(string userId, string policyName);
    Task<Result<bool>> HasClaim(string userId, string claimType, string claimValue);

    Task<Result<bool>> ValidateToken(string token);

}
