using DomainDrivenERP.Domain.Abstractions.Infrastructure.Services;
using DomainDrivenERP.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenERP.Infrastructure;

public static class InfrustructureDependencies
{
    public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
