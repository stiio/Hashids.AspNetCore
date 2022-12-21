using HashidsNet.AspNetCore.Interfaces;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add binder to target property. (for route, query and form parameters).
/// </summary>
public class HashidsBinderAttribute : Attribute, IHashidsProperty
{
}