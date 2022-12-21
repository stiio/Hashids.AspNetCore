using HashidsNet.AspNetCore.Swashbuckle.Filters;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HashidsNet.AspNetCore.Options;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SchemaGeneratorOptions.SchemaFilters.Add(new HashidsSchemaFilter());
    }
}