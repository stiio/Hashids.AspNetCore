using System.Text.Json;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Extensions;

internal static class JsonSerializerOptionsExtensions
{
    public static IHashids GetHashids(this JsonSerializerOptions options)
    {
        return options.Converters.OfType<HashidsInjectionJsonProvider>().First().Hashids;
    }
}