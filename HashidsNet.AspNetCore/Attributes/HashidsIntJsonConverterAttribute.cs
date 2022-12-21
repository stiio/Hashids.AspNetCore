using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

public class HashidsIntJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    public HashidsIntJsonConverterAttribute()
        : base(typeof(HashidsIntJsonConverter))
    {
    }
}