using HashidsNet.AspNetCore.Json.SystemText;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HashidsNet.AspNetCore.Options;

internal class ConfigureSystemTextJsonOptions : IConfigureOptions<JsonOptions>
{
    private readonly IHashids hashids;

    public ConfigureSystemTextJsonOptions(IHashids hashids)
    {
        this.hashids = hashids;
    }

    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Add(new HashidsInjectionJsonProvider(this.hashids));
    }
}