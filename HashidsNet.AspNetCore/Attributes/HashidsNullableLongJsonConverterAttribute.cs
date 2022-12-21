using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add hashids json converter for long? property.
/// </summary>
public class HashidsNullableLongJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashidsNullableLongJsonConverterAttribute"/> class.
    /// </summary>
    public HashidsNullableLongJsonConverterAttribute()
        : base(typeof(HashidsNullableLongJsonConverter))
    {
    }
}