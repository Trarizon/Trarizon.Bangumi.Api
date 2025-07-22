using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization;
internal sealed class NullableDateOnlyToStringJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var tokenType = reader.TokenType;
        if (tokenType is JsonTokenType.Null)
            return null;
        if (tokenType is not JsonTokenType.String)
            Throws.ThrowUnexpectedJsonToken(reader.TokenType, JsonTokenType.String, JsonTokenType.Null);

        if (reader.ValueSpan.Length == 0)
            return null;
        return JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.DateOnly);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value is { } val) {
            JsonSerializer.Serialize(val, BangumiJsonSerializerContext.Default.DateOnly);
        }
        else {
            writer.WriteNullValue();
        }
    }
}

internal sealed class NullableDateOnlyToNotNullStringJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.String);
        if (reader.ValueSpan.Length == 0)
            return null;
        return JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.DateOnly);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value is { } val) {
            JsonSerializer.Serialize(val, BangumiJsonSerializerContext.Default.DateOnly);
        }
        else {
            writer.WriteStringValue("");
        }
    }
}
