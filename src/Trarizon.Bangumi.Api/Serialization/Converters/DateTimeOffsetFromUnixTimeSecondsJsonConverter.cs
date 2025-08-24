using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters;
internal sealed class DateTimeOffsetFromUnixTimeSecondsJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.Number);
        long timestamp = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeSeconds(timestamp);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToUnixTimeSeconds());
    }
}
