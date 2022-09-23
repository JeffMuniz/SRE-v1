using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Integration.Api.Middlewares
{
    public static class LoggingRequestBodyMiddlewareExtensions
    {
        public static IApplicationBuilder UseTraceRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingRequestBodyMiddleware>();
        }
    }

    public class LoggingRequestBodyMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LoggingRequestBodyMiddleware(
            ILogger<LoggingRequestBodyMiddleware> logger,
            RequestDelegate next
        )
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.ToUpper() != "GET" && context.Request.ContentLength.GetValueOrDefault() > 0)
                _logger.LogInformation(await FormatRequest(context.Request));

            await _next(context);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            var headerAsText = Newtonsoft.Json.JsonConvert.SerializeObject(request.Headers);

            request.Body.Seek(0, SeekOrigin.Begin);

            var bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();

            request.Body.Seek(0, SeekOrigin.Begin);

            return $"Request body: {request.Method} {request.Path}{request.QueryString} {Environment.NewLine} Headers: {headerAsText} {Environment.NewLine} Body: {bodyAsText}";
        }
    }
}
