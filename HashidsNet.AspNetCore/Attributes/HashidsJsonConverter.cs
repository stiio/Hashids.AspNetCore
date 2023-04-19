using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

/// <summary>
/// Add hashids json converter for property.
/// </summary>
public class HashidsJsonConverter : JsonConverterAttribute, IHashidsProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashidsJsonConverter"/> class.
    /// </summary>
    public HashidsJsonConverter()
        : base(typeof(HashidsJsonConverterFactory))
    {
    }
}