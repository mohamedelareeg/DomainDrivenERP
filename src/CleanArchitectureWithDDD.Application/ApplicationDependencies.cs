using CleanArchitectureWithDDD.Application.Behaviors;
using CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWithDDD.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        // Mapping Profiles
        services.AddAutoMapper(typeof(RetriveCustomerMapping));
        return services;
    }
}
