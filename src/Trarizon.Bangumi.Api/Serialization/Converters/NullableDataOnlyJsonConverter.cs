using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters;

internal partial class NullableDataOnlyJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.Null, JsonTokenType.String);
        if(reader.TokenType is JsonTokenType.Null)
            return null;
        if (reader.ValueTextEquals(""))
            return null;
        return DateOnly.Parse(reader.GetString()!);
    }
    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if(value == null) {
            writer.WriteStringValue("");
            return;
        }
        writer.WriteStringValue($"{value.Value:yyyy-MM-dd}");
    }
}
