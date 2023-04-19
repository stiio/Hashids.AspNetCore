using HashidsNet.AspNetCore.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HashidsNet.AspNetCore.Swashbuckle.Filters;

internal class HashidsSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (IsHashids(context))
        {
            if (schema.Type == "array" && schema.Items != null)
            {
                schema.Items.Type = "string";
                schema.Items.Format = null;
                return;
            }

            schema.Type = "string";
            schema.Format = null;
        }
    }

    private static bool IsHashids(SchemaFilterContext context)
    {
        return context.ParameterInfo?.CustomAttributes.Any(a => a.AttributeType.IsAssignableTo(typeof(IHashidsProperty))) == true
            || context.MemberInfo?.CustomAttributes.Any(a => a.AttributeType.IsAssignableTo(typeof(IHashidsProperty))) == true;
    }
}