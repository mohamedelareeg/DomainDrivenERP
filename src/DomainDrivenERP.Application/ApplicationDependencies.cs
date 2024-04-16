using DomainDrivenERP.Application.Behaviors;
using DomainDrivenERP.Application.Features.Customers.Queries.RetriveCustomer;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenERP.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        //MediatR PopleLines
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        //Fluent Validation
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        // Mapping Profiles
        services.AddAutoMapper(typeof(RetriveCustomerMapping));
        return services;
    }
}
