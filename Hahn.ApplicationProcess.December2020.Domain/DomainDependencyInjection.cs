using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.December2020.Domain
{
    public static class DomainDependencyInjection
    {
        public static IServiceCollection AddDomainRegistrations(this IServiceCollection services)
        {
            // This ensures automatic registration of all interfaces/implementations in the DomainDependencyInjection assembly
            Assembly.GetAssembly(typeof(DomainDependencyInjection))?
                .ExportedTypes
                .Where(t => t.IsClass)
                .SelectMany(t => t.GetInterfaces(), (c, i) => new {Class = c, Interface = i})
                .ToList()
                .ForEach(x => services.AddTransient(x.Interface, x.Class));

            return services;
        }
    }
}
