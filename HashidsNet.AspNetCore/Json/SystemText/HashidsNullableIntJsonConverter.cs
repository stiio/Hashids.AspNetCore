using System.Text.Json;
using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Extensions;

namespace HashidsNet.AspNetCore.Json.SystemText;

internal class HashidsNullableIntJsonConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var stringValue = reader.GetString();

        if (string.IsNullOrEmpty(stringValue))
        {
            return null;
        }

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
        if (value is not null)
        {
            writer.WriteStringValue(options.GetHashids().Encode(value.Value));
        }
    }
}