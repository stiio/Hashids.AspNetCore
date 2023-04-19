using HashidsNet.AspNetCore.Swashbuckle.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HashidsNet.AspNetCore.Options;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SchemaFilter<HashidsSchemaFilter>();
    }
}