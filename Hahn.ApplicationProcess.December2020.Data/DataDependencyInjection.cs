using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public static class DataDependencyInjection
    {
        public static IServiceCollection AddDataRegistrations(this IServiceCollection services)
        {
            // EF in-memory DB connection
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("hahnMemoryDb"));

            // This ensures automatic registration of all interfaces/implementations in the DataDependencyInjection assembly
            Assembly.GetAssembly(typeof(DataDependencyInjection))?
                .ExportedTypes
                .Where(t => t.IsClass)
                .SelectMany(t => t.GetInterfaces(), (c, i) => new {Class = c, Interface = i})
                .ToList()
                .ForEach(x => services.AddTransient(x.Interface, x.Class));

            return services;
        }
    }
}
