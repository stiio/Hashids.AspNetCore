using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add hashids long json converter for long property.
/// </summary>
public class HashidsLongJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashidsLongJsonConverterAttribute"/> class.
    /// </summary>
    public HashidsLongJsonConverterAttribute()
        : base(typeof(HashidsLongJsonConverter))
    {
    }
}