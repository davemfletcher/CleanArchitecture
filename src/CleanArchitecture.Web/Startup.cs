using System;
using System.Reflection;
using CleanArchitecture.Application.Employees.Commands;
using CleanArchitecture.Application.Employees.Queries;
using CleanArchitecture.Application.Infrastructure;
using CleanArchitecture.Persistence;
using CleanArchitecture.Web.Infrastructure.DependencyInjection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            CurrentEnvironment = currentEnvironment;
            Configuration = configuration;
        }

        private IHostingEnvironment CurrentEnvironment { get; set; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNotifications();

            // services.AddMediatR(typeof(EmployeesWithManagers).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(EmployeesWithManagers.QueryHandler).GetTypeInfo().Assembly, typeof(EmployeesWithManagers.Query).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            var edibConnectionString = Configuration["connectionStrings:NorthwindDatabase"];
            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(edibConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    })
            );

            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, NorthwindDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                db.Database.Migrate();
                NorthwindInitializer.Initialize(db);
            } 

            app.UseMvc();
        }
    }
}
