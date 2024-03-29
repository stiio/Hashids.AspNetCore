﻿using HashidsNet.AspNetCore.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace HashidsNet.AspNetCore.Binder;

internal class HashidsModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata is not DefaultModelMetadata defaultModelMetadata)
        {
            return null;
        }

        if (defaultModelMetadata.Attributes.Attributes.All(a => a is not HashidsBinderAttribute))
        {
            return null;
        }

        return new HashidsModelBinder(context.Services.GetRequiredService<IHashids>());
    }
}