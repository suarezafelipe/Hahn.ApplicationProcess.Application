using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.December2020.Data;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.Validators;
using Hahn.ApplicationProcess.December2020.Web.Middleware;
using Hahn.ApplicationProcess.December2020.Web.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hahn.ApplicationProcess.December2020.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>());
            services.AddDomainRegistrations();
            services.AddDataRegistrations();
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.December2020.Web v1"));
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<UpdateLocaleMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder.WithOrigins("http://localhost:8080", "https://productionUrl.com")
                .AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}