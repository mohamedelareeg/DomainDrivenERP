using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Persistence.BackgroundJobs;
using CleanArchitectureWithDDD.Persistence.Interceptors;
using CleanArchitectureWithDDD.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence
{
    public static class PersistenceDependencies
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
        {
            //DB
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                var connectionString = "Server=.;Database=CleanDDD;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=true;";
                options.UseSqlServer(connectionString).AddInterceptors(interceptor);
            }
            );
            services.AddTransient<IUnitOfWork,UnitOfWork>();

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

            //Repositories
            services.AddScoped<ICustomerRespository,CustomerRespository>();
            return services;

        }
    }
}
