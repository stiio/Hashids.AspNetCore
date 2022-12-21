using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add hashids json converter for integer property.
/// </summary>
public class HashidsIntJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashidsIntJsonConverterAttribute"/> class.
    /// </summary>
    public HashidsIntJsonConverterAttribute()
        : base(typeof(HashidsIntJsonConverter))
    {
    }
}