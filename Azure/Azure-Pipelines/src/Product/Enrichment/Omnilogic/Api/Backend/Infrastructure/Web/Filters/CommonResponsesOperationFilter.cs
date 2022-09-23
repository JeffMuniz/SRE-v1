using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Product.Enrichment.Macnaima.Api.Infrastructure.Web.Filters
{
    public class CommonResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context
                .MethodInfo
                .DeclaringType
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
                operation.Responses.Add(
                    StatusCodes.Status401Unauthorized.ToString(), 
                    new OpenApiResponse { Description = "Unauthorized" }
                );

            operation.Responses.Add(
                StatusCodes.Status500InternalServerError.ToString(),
                new OpenApiResponse { Description = "Internal Server Error" }
            );
        }
    }
}
