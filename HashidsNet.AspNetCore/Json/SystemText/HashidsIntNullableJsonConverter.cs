using System.Text.Json;
using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Extensions;

namespace HashidsNet.AspNetCore.Json.SystemText;

internal class HashidsIntNullableJsonConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var stringValue = reader.GetString();

        try
        {
            var value = options.GetHashids().DecodeSingle(stringValue);

            return value;
        }
        catch (NoResultException)
        {
            throw new JsonException("Invalid hash string.");
        }
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(options.GetHashids().Encode((int)value));
    }
}