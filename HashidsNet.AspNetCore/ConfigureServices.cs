using HashidsNet.AspNetCore.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HashidsNet.AspNetCore;

/// <summary>
/// Configure Services Extension
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Add singleton <see cref="IHashids"/>, hashids binder provider, hashids injection json provider and swagger filter.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="salt"></param>
    /// <param name="minHashLength"></param>
    /// <param name="alphabet"></param>
    /// <param name="seps"></param>
    public static void AddHashids(
        this IServiceCollection services,
        string salt = "",
        int minHashLength = 0,
        string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
        string seps = "cfhistuCFHISTU")
    {
        services.AddSingleton<IHashids>(_ => new Hashids(salt, minHashLength, alphabet, seps));

        services.ConfigureOptions<ConfigureSystemTextJsonOptions>();
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();
        services.ConfigureOptions<ConfigureMvcOptions>();
    }
}