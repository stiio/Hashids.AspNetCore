using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

public class HashidsNullableIntJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    public HashidsNullableIntJsonConverterAttribute()
        : base(typeof(HashidsNullableIntJsonConverter))
    {
    }
}