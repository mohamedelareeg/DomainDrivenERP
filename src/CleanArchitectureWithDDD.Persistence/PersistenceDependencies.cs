using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Persistence.BackgroundJobs;
using CleanArchitectureWithDDD.Persistence.Clients;
using CleanArchitectureWithDDD.Persistence.Idempotence;
using CleanArchitectureWithDDD.Persistence.Interceptors;
using CleanArchitectureWithDDD.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArchitectureWithDDD.Persistence;

public static class PersistenceDependencies
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //DB
        #region Interceptors
        // I Moved The Logic Inside UnitOfWork SaveChanges so I Remove the Interceptors DI 
        //services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>(); 
        //services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        #endregion
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            ConvertDomainEventsToOutboxMessagesInterceptor? interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            //var connectionString = "Server=.;Database=CleanDDD;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=true;";
            string connectionString = configuration.GetConnectionString("Database");
            options.UseSqlServer(connectionString).AddInterceptors(interceptor);
        }
        );
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        //Quartz
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure.AddJob<ProcessOutboxMessagesJob>(jobKey).AddTrigger(trigger =>
            trigger.ForJob(jobKey).WithSimpleSchedule(schedule =>
            schedule.WithIntervalInSeconds(10).RepeatForever()));
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService();

        //Idempotency With MediatR Notification || Scrutor for Decorate
        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        //Repositories
        services.AddScoped<ICustomerRespository, CustomerRespository>();
        return services;

    }
}
