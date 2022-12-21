using HashidsNet.AspNetCore.Binder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HashidsNet.AspNetCore.Options;

internal class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new HashidsModelBinderProvider());
    }
}