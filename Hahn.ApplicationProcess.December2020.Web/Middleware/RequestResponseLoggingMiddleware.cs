using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Hahn.ApplicationProcess.December2020.Web.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                .CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await InvokeNextAndLogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            _logger.LogInformation($"Http *Request* Information:{Environment.NewLine}" +
                                   $"{context.Request.Method} {context.Request.Scheme}/{context.Request.Host}/{context.Request.Path} " +
                                   $"| QueryString: {context.Request.QueryString} " +
                                   $"| Request Body: {ReadRequestStream(requestStream)}");

            context.Request.Body.Position = 0;
        }
        private static string ReadRequestStream(Stream stream)
        {
            stream.Position = 0;
            using StreamReader streamReader = new(stream);
            var requestBody = streamReader.ReadToEnd();

            return requestBody;
        }

        private async Task InvokeNextAndLogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Http *Response* Information:{Environment.NewLine}" +
                                   $"{context.Request.Method} {context.Request.Scheme}/{context.Request.Host}/{context.Request.Path} " +
                                   $"| Response Code: {context.Response.StatusCode} " +
                                   $"| Response Body: {text}");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
