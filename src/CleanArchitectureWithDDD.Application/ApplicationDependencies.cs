﻿using CleanArchitectureWithDDD.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            });
            services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationPiplineBehavior<,>));
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly,includeInternalTypes:true);
            return services;
        }
    }
}