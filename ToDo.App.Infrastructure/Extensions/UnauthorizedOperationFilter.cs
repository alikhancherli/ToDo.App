using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToDo.App.Infrastructure.Extensions
{
    public class UnauthorizedOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerFilters = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var controllerMetadta = context.ApiDescription.ActionDescriptor.EndpointMetadata;

            bool allowAnonymous = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() ||
           context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if ((controllerFilters.Any(p => p.Filter is AuthorizeFilter) || controllerMetadta.Any(p => p is AuthorizeAttribute)) && !allowAnonymous)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse() { Description = "401 Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse() { Description = "403 Forbidden" });
            }
        }
    }
}
