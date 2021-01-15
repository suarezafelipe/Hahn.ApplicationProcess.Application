using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.December2020.Web.StartupExtensions
{
    public static class Swagger
    {
        // This is done to avoid too much code in the Startup.cs class, as it tends to grow pretty quickly in most projects.
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "Hahn.ApplicationProcess.December2020.Web", Version = "v1"});
                c.ExampleFilters();
                var apiXml = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicationProcess.December2020.Web.xml");
                c.IncludeXmlComments(apiXml);
                var domainXml = Path.Combine(AppContext.BaseDirectory,
                    @"Hahn.ApplicationProcess.December2020.Domain.xml");
                c.IncludeXmlComments(domainXml);
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            return services;
        }
    }
}