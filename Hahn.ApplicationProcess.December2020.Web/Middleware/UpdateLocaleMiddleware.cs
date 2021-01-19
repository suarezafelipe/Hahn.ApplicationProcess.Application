using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hahn.ApplicationProcess.December2020.Web.Middleware
{
    public class UpdateLocaleMiddleware
    {
        private readonly RequestDelegate _next;


        public UpdateLocaleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("locale", out var locale))
            {
                CultureInfo.CurrentUICulture = new CultureInfo(locale);
            }

            await _next(context);
        }
    }
}
