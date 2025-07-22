using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization;
internal sealed class DayOfWeekByNameTupleIdJsonConverter : JsonConverter<DayOfWeek>
{
    public override DayOfWeek Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.StartObject);

        DayOfWeek? day = null;

        while (true) {
            reader.Read();
            if (reader.TokenType is JsonTokenType.EndObject or JsonTokenType.None)
                break;
            if (reader.TokenType is not JsonTokenType.PropertyName)
                Throws.ThrowUnexpectedJsonToken(reader.TokenType, JsonTokenType.EndObject, JsonTokenType.PropertyName);

            if (reader.ValueTextEquals("id"u8)) {
                reader.Read();
                Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.Number);

                day = DayOfWeek.FromBangumiWeekdayId(reader.GetInt32());
            }
            else {
                reader.Read();
            }
        }

        if (day is { } res)
            return res;

        Throws.ThrowInvalidOperation("Weekday id not found.");
        return default;
    }

    public override void Write(Utf8JsonWriter writer, DayOfWeek value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("en"u8, value.ToEnglishShortName());
        writer.WriteString("cn"u8, value.ToChineseName());
        writer.WriteString("ja"u8, value.ToJapaneseName());
        writer.WriteNumber("id"u8, value.ToBangumiWeekdayId());
        writer.WriteEndObject();
    }

}
