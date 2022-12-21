using System.Text.Json;
using System.Text.Json.Serialization;

namespace HashidsNet.AspNetCore.Json.SystemText;

internal class HashidsInjectionJsonProvider : JsonConverter<object>
{
    public HashidsInjectionJsonProvider(IHashids hashids)
    {
        this.Hashids = hashids;
    }

    public IHashids Hashids { get; }

    public override bool CanConvert(Type typeToConvert) => false;

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}