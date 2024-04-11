﻿using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Persistence.BackgroundJobs;
using CleanArchitectureWithDDD.Persistence.Caching;
using CleanArchitectureWithDDD.Persistence.Clients;
using CleanArchitectureWithDDD.Persistence.Data;
using CleanArchitectureWithDDD.Persistence.Idempotence;
using CleanArchitectureWithDDD.Persistence.Interceptors;
using CleanArchitectureWithDDD.Persistence.Repositories.Categories;
using CleanArchitectureWithDDD.Persistence.Repositories.Coa;
using CleanArchitectureWithDDD.Persistence.Repositories.Coas;
using CleanArchitectureWithDDD.Persistence.Repositories.Customers;
using CleanArchitectureWithDDD.Persistence.Repositories.Invoices;
using CleanArchitectureWithDDD.Persistence.Repositories.Journals;
using CleanArchitectureWithDDD.Persistence.Repositories.Orders;
using CleanArchitectureWithDDD.Persistence.Repositories.Products;
using CleanArchitectureWithDDD.Persistence.Repositories.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArchitectureWithDDD.Persistence;

public static class PersistenceDependencies
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // DB
        #region Interceptors
        // I Moved The Logic Inside UnitOfWork SaveChanges so I Remove the Interceptors DI 
        // services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>(); 
        // services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        #endregion
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            ConvertDomainEventsToOutboxMessagesInterceptor? interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            string connectionString = configuration.GetConnectionString("SqlServer");
            options.UseSqlServer(connectionString)
                // ,a=>a.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery) //All the Query will be spliting
            .AddInterceptors(interceptor);
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        // Quartz
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure.AddJob<ProcessOutboxMessagesJob>(jobKey).AddTrigger(trigger =>
            trigger.ForJob(jobKey).WithSimpleSchedule(schedule =>
            schedule.WithIntervalInSeconds(10).RepeatForever()));
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService();

        // Idempotency With MediatR Notification || Scrutor for Decorate
        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        // I Create the Repositories with many ways [ Choose the way you want and comment the others ]
        // Repositories With EF
        services.AddScoped<ICustomerRespository, CustomerRespository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICoaRepository, CoaRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Repositories with Dapper ( Same Logic Like The EF ) || I Recreate some of them only for showing the Difference only
        services.AddScoped<ICustomerRespository, CustomerSqlRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceSqlRepository>();
        services.AddScoped<ICoaRepository, CoaSqlRepository>();
        services.AddScoped<IJournalRepository, JournalSqlRepository>();
        services.AddScoped<ITransactionRepository, TransactionSqlRepository>();

        // Repositories with GenericRepository and Specification Pattern || I Recreate some of them only for showing the Difference only
        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<ICustomerRespository, CustomerSpecificationRepository>();
        services.AddScoped<IInvoiceRepository, InvoicesSpecificationRepository>();
        services.AddScoped<ICoaRepository, CoaSpecificationRepository>();
        services.AddScoped<IJournalRepository, JournalSpecificationRepository>();
        services.AddScoped<ITransactionRepository, TransactionSpecificationRepository>();

        // Caching
        services.AddMemoryCache();
        #region Another Caching  Ways
        // services.AddScoped<CustomerRespository>();
        // services.AddScoped<ICustomerRespository, CachedCustomerRepository>(); // First Way
        // services.AddScoped<ICustomerRespository>(provider => // Second Way
        // {
        //    CustomerRespository? customerRespository = provider.GetService<CustomerRespository>();
        //    return new CachedCustomerRepository(customerRespository,provider.GetService<IMemoryCache>());
        // });
        #endregion
        services.Decorate<ICustomerRespository, CachedCustomerRepository>();
        services.Decorate<IInvoiceRepository, CachedInvoiceRepository>();
        services.Decorate<ICoaRepository, CachedCoaRepository>();
        services.Decorate<IJournalRepository, CachedJournalRepository>();
        services.Decorate<ITransactionRepository, CachedTransactionRepository>();
        services.Decorate<IProductRepository, CachedProductRepository>();
        services.Decorate<IOrderRepository, CachedOrderRepository>();
        services.Decorate<ICategoryRepository, CachedCategoryRepository>();

        services.AddStackExchangeRedisCache(redisOptions => {
            string connectionString = configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connectionString;
        });
        services.AddSingleton<ICacheService, CacheService>();


        return services;

    }
}
