using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DomainDrivenERP.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IConfiguration _configuration;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _configuration = configuration;
    }

    public async Task<Result<bool>> AuthorizeAsync(string userId, string policyName)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        System.Security.Claims.ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result<string?>> GetUserNameAsync(string userId)
    {
        ApplicationUser user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<Result<bool>> HasClaim(string userId, string claimType, string claimValue)
    {
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        System.Security.Claims.ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        bool hasClaim = principal.Claims.Any(c => c.Type == claimType && c.Value == claimValue);

        return hasClaim;
    }

    public async Task<Result<bool>> IsInRoleAsync(string userId, string role)
    {
        ApplicationUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<Result<bool>> ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            // Configure TokenValidationParameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return Result.Failure<bool>(new Error("TokenValidationFailed", "Invalid token"));
            }

            string? userId = principal.FindFirst(ClaimTypes.Name)?.Value;
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result.Failure<bool>(new Error("UserNotFound", "User not found"));
            }

            return Result.Success(true);
        }
        catch (Exception)
        {
            return Result.Failure<bool>(new Error("UnexpectedError", "An unexpected error occurred while validating the token."));
        }
    }


}
