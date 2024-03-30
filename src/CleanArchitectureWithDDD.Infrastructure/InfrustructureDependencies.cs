using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWithDDD.Infrastructure;

public static class InfrustructureDependencies
{
    public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
    {
        //Services
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
