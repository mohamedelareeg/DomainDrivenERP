using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainDrivenERP.Application.Features.Authentication.Commands.Login;
using DomainDrivenERP.Application.Features.Authentication.Commands.Register;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.AppMetaData;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Identity.Data;
using DomainDrivenERP.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DomainDrivenERP.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IdentityDbContext _identityDbContext;
    private readonly IEmailsService _emailsService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings,
        IdentityDbContext identityDbContext,
        IEmailsService emailsService,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _identityDbContext = identityDbContext;
        _emailsService = emailsService;
        _roleManager = roleManager;
    }
    public async Task<Result<LoginCommandResult>> Login(string email, string password)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result.Failure<LoginCommandResult>(new Error("UserNotFound", "User not found."));
        }

        SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Result.Failure<LoginCommandResult>(new Error("InvalidCredentials", "Invalid credentials."));
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new LoginCommandResult
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    public async Task<Result<RegisterCommandResult>> Register(string firstName, string lastName, string email, string userName, string password)
    {
        ApplicationUser? existingUser = await _userManager.FindByNameAsync(userName);

        if (existingUser != null)
        {
            return Result.Failure<RegisterCommandResult>(new Error("UsernameExists", $"Username '{userName}' already exists."));
        }

        var user = new ApplicationUser
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            EmailConfirmed = true
        };

        ApplicationUser? existingEmail = await _userManager.FindByEmailAsync(email);

        if (existingEmail == null)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return Result.Success(new RegisterCommandResult { UserId = user.Id });
            }
            else
            {
                var errors = result.Errors.Select(a => a.Description).ToList();
                return Result.Failure<RegisterCommandResult>(new Error("BadRequestDetails", string.Join(", ", errors)));
            }
        }
        else
        {
            return Result.Failure<RegisterCommandResult>(new Error("EmailExists", $"Email '{email}' already exists."));
        }
    }
    public async Task<Result<bool>> SendResetPasswordCode(string email)
    {
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction trans = await _identityDbContext.Database.BeginTransactionAsync();

        try
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result.NotFound<bool>(new Error("UserNotFound", "User not found."));

            string chars = "0123456789";
            var random = new Random();
            string randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            user.Code = randomNumber;

            IdentityResult updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return Result.Failure<bool>(new Error("ErrorInUpdateUser", "Error occurred while updating user."));

            string message = $"Code To Reset Password: {user.Code}";

            await _emailsService.SendEmail(user.Email, message, "Reset Password", "Demo Company");

            await trans.CommitAsync();

            return true;
        }
        catch (Exception)
        {
            await trans.RollbackAsync();
            return Result.Failure<bool>(new Error("Failed", "Failed to send reset password code."));
        }
    }

    public async Task<Result<bool>> ConfirmAndResetPassword(string code, string email, string newPassword)
    {
        using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction trans = await _identityDbContext.Database.BeginTransactionAsync();
        try
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result.NotFound<bool>(new Error("UserNotFound", "User not found."));

            string? userCode = user.Code;

            if (userCode == code)
            {
                // Code is valid, proceed to reset the password
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newPassword);

                await trans.CommitAsync();

                return true;
            }

            return Result.Failure<bool>(new Error("InvalidCode", "Invalid reset code."));
        }
        catch (Exception)
        {
            await trans.RollbackAsync();
            return Result.Failure<bool>(new Error("Failed", "Failed to confirm and reset password."));
        }
    }


    #region Helpers
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        IEnumerable<Claim> claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
    #endregion
}
