using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Golden.Raspberry.Awards.Web.Api.Helpers
{
    public class OperationFilterHelper : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParameter = context.ApiDescription.ActionDescriptor.Parameters.FirstOrDefault(p => p.ParameterType == typeof(IFormFile));

            if (fileParameter != null)
            {
                foreach (var requestContent in operation.RequestBody.Content)
                {
                    requestContent.Value.Schema.Properties.Clear();
                    requestContent.Value.Schema.AdditionalPropertiesAllowed = true;
                    requestContent.Value.Schema.Type = "object";
                }

                operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(
                    fileParameter.Name,
                    new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    }
                );
            }
        }
    }
}
