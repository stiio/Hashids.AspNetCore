using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Interfaces;
using HashidsNet.AspNetCore.Json.SystemText;

namespace HashidsNet.AspNetCore.Attributes;

public class HashidsLongJsonConverterAttribute : JsonConverterAttribute, IHashidsProperty
{
    public HashidsLongJsonConverterAttribute()
        : base(typeof(HashidsLongJsonConverter))
    {
    }
}