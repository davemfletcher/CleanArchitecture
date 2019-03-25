using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Employees.Queries;
using CleanArchitecture.Application.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Web.Infrastructure.DependencyInjection
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(EmployeesWithManagers.QueryHandler).GetTypeInfo().Assembly, typeof(EmployeesWithManagers.Query).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            return services;
        }
    }
}
