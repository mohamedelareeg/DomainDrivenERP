using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenERP.Identity.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUser _user;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUser user)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _user = user;
    }

    public async Task<Result<bool>> AddClaimToRoleAsync(string roleName, string claimType, string claimValue, CancellationToken cancellationToken = default)
    {
        IdentityRole? role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result.Failure<bool>(new Error("RoleNotFound", $"Role '{roleName}' not found."));
        }

        ApplicationUser? user = await _userManager.FindByIdAsync(_user.Id);
        if (user == null)
        {
            return Result.Failure<bool>(new Error("UserNotFound", $"User '{_user.Id}' not found."));
        }

        IdentityResult result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorAddingClaim", $"Error adding claim to role '{roleName}'."));
    }
    public async Task<Result<bool>> AddClaimToUserAsync(string userId, string claimType, string claimValue, CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<bool>(new Error("UserNotFound", $"User '{userId}' not found."));
        }

        IdentityResult result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorAddingClaim", $"Error adding claim to user '{user.UserName}'."));
    }


    public async Task<Result<bool>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<bool>(new Error("UserNotFound", $"User '{userId}' not found."));
        }

        IdentityResult result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorAssigningRole", $"Error assigning role '{roleName}' to user '{user.UserName}'."));
    }


    public async Task<Result<bool>> CreateRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        bool roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
        {
            return Result.Failure<bool>(new Error("RoleExists", $"Role '{roleName}' already exists."));
        }

        var role = new IdentityRole(roleName);
        IdentityResult result = await _roleManager.CreateAsync(role);

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorCreatingRole", $"Error creating role '{roleName}'."));
    }
    public async Task<Result<bool>> DeleteRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        IdentityRole? role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result.Failure<bool>(new Error("RoleNotFound", $"Role '{roleName}' not found."));
        }

        IdentityResult result = await _roleManager.DeleteAsync(role);

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorDeletingRole", $"Error deleting role '{roleName}'."));
    }

    public async Task<Result<bool>> EditRoleAsync(string roleName, string newRoleName, CancellationToken cancellationToken = default)
    {
        IdentityRole? role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result.Failure<bool>(new Error("RoleNotFound", $"Role '{roleName}' not found."));
        }

        role.Name = newRoleName;
        IdentityResult result = await _roleManager.UpdateAsync(role);

        return result.Succeeded
            ? Result.Success(true)
            : Result.Failure<bool>(new Error("ErrorUpdatingRole", $"Error updating role '{newRoleName}'."));
    }

    public async Task<Result<CustomList<string>>> GetAllClaimsAsync(CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(_user.Id);
        if (user == null)
        {
            return Result.Failure<CustomList<string>>(new Error("UserNotFound", $"User '{_user.Id}' not found."));
        }

        IList<System.Security.Claims.Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IEnumerable<string> claims = userClaims.Select(claim => $"{claim.Type}:{claim.Value}");

        return claims.ToCustomList();
    }


    public async Task<Result<CustomList<string>>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await Task.FromResult(_roleManager.Roles.Select(r => r.Name));
        return roles.ToCustomList();
    }

    public async Task<Result<CustomList<string>>> GetRoleClaimsAsync(string roleName, CancellationToken cancellationToken = default)
    {
        IdentityRole? role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result.Failure<CustomList<string>>(new Error("RoleNotFound", $"Role '{roleName}' not found."));
        }

        IList<System.Security.Claims.Claim> claims = await _roleManager.GetClaimsAsync(role);
        return claims.Select(c => $"{c.Type}:{c.Value}").ToCustomList();
    }


    public async Task<Result<CustomList<string>>> GetUserClaimsAsync(string userId, CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<CustomList<string>>(new Error("UserNotFound", $"User '{userId}' not found."));
        }

        IList<System.Security.Claims.Claim> claims = await _userManager.GetClaimsAsync(user);
        return claims.Select(a => a.Value).ToCustomList();
    }

    public async Task<Result<CustomList<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure<CustomList<string>>(new Error("UserNotFound", $"User '{userId}' not found."));
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);
        return roles.ToCustomList();
    }

}
