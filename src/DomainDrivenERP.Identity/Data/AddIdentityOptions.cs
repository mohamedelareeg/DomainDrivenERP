using DomainDrivenERP.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenERP.Identity.Data;

public static class AddIdentityOptions
{
    public static void SetOptions(IServiceCollection services, DefaultIdentityOptions defaultIdentityOptions)
    {
        try
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = defaultIdentityOptions.PasswordRequireDigit;
                options.Password.RequiredLength = defaultIdentityOptions.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = defaultIdentityOptions.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = defaultIdentityOptions.PasswordRequireUppercase;
                options.Password.RequireLowercase = defaultIdentityOptions.PasswordRequireLowercase;
                options.Password.RequiredUniqueChars = defaultIdentityOptions.PasswordRequiredUniqueChars;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(defaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = defaultIdentityOptions.LockoutMaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = defaultIdentityOptions.LockoutAllowedForNewUsers;

                options.User.RequireUniqueEmail = defaultIdentityOptions.UserRequireUniqueEmail;

                options.SignIn.RequireConfirmedEmail = defaultIdentityOptions.SignInRequireConfirmedEmail;
            }).AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
