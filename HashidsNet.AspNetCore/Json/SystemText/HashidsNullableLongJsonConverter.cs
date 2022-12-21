using System.Text.Json;
using System.Text.Json.Serialization;

namespace HashidsNet.AspNetCore.Json.SystemText;

public class HashidsNullableLongJsonConverter : JsonConverter<long?>
{
    private readonly IHashids hashids;

    public HashidsNullableLongJsonConverter(IHashids hashids)
    {
        this.hashids = hashids;
    }

    public override bool CanConvert(Type typeToConvert) => false;

    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}