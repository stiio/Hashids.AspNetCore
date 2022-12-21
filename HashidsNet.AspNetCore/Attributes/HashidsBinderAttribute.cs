using HashidsNet.AspNetCore.Binder;
using HashidsNet.AspNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HashidsNet.AspNetCore.Attributes;

public class HashidsBinderAttribute : ModelBinderAttribute, IHashidsProperty
{
    public HashidsBinderAttribute()
    {
        this.BinderType = typeof(HashidsModelBinder);
    }
}