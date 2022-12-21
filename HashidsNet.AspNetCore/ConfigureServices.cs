using HashidsNet.AspNetCore.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HashidsNet.AspNetCore;

public static class ConfigureServices
{
    public static void AddHashids(
        this IServiceCollection services,
        string salt = "",
        int minHashLength = 0,
        string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
        string seps = "cfhistuCFHISTU")
    {
        services.AddSingleton<IHashids>(_ => new Hashids(salt, minHashLength, alphabet, seps));

        services.ConfigureOptions<ConfigureSystemTextJsonOptions>();
    }
}