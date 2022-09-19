using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Product.Enrichment.macnaima.Api.Backend.Application.Usecases.Shared.Models;
using System;

namespace Product.Enrichment.macnaima.Api.Infrastructure.Web.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                var message = "An unhandled error occurred in application";
                _logger.LogCritical(context.Exception, message);
                context.Result = new ObjectResult(ErrorBuilder.Create(0, message))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
