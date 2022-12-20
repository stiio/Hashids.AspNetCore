using HashidsNet.AspNetCore.Binder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HashidsNet.AspNetCore.Options;

public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new HashidsModelBinderProvider());
    }
}