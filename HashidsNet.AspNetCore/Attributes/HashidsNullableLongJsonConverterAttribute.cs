using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

public class HashidsNullableLongJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    public HashidsNullableLongJsonConverterAttribute()
        : base(typeof(HashidsNullableLongJsonConverter))
    {
    }
}