using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add hashids json converter for int? property.
/// </summary>
public class HashidsNullableIntJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashidsNullableIntJsonConverterAttribute"/> class.
    /// </summary>
    public HashidsNullableIntJsonConverterAttribute()
        : base(typeof(HashidsNullableIntJsonConverter))
    {
    }
}