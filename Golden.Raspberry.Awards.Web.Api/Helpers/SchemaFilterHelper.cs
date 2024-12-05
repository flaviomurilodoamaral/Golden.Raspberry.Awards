using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Golden.Raspberry.Awards.Web.Api.Helpers
{
    public class SchemaFilterHelper : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumType = context.Type;
                var enumDescriptions = new Dictionary<string, string>();

                foreach (var enumValue in Enum.GetValues(enumType))
                {
                    var memberInfo = enumType.GetMember(enumValue.ToString()).FirstOrDefault();

                    if (memberInfo != null)
                    {
                        var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                        if (descriptionAttribute != null)
                        {
                            enumDescriptions.Add(enumValue.ToString(), descriptionAttribute.Description);
                        }
                    }
                }

                if (enumDescriptions.Any())
                {
                    schema.Enum.Clear();

                    foreach (var enumEntry in enumDescriptions)
                    {
                        schema.Enum.Add(new OpenApiString(enumEntry.Value));
                    }
                }
            }
        }
    }
}
