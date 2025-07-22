using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Subjects;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Models;
internal sealed class RatingSetCountsJsonPropertyConverter : JsonConverter<RatingCounts>
{
    public override RatingCounts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(reader.TokenType is JsonTokenType.StartObject);
        Span<byte> propName = stackalloc byte[1];
        int[] result = new int[10];
        while (reader.Read()) {
            var tokenType = reader.TokenType;
            if (tokenType is JsonTokenType.EndObject)
                break;

            if (tokenType is not JsonTokenType.PropertyName)
                Throws.ThrowUnexpectedJsonToken(tokenType, JsonTokenType.EndObject, JsonTokenType.PropertyName);

            switch (reader.ValueSpan) {
                case [>= (byte)'1' and <= (byte)'9' and var d]:
                    reader.Read();
                    if (reader.TokenType is not JsonTokenType.Number)
                        Throws.ThrowUnexpectedJsonToken(reader.TokenType, JsonTokenType.Number);
                    result[d - '1'] = reader.GetInt32();
                    break;
                case [(byte)'1', (byte)'0']:
                    reader.Read();
                    if (reader.TokenType is not JsonTokenType.Number)
                        Throws.ThrowUnexpectedJsonToken(reader.TokenType, JsonTokenType.Number);
                    result[9] = reader.GetInt32();
                    break;
            }
        }
        return new RatingCounts(result);
    }

    public override void Write(Utf8JsonWriter writer, RatingCounts value, JsonSerializerOptions options)
    {
        var array = value._counts;
        Debug.Assert(array.Length == 10);
        writer.WriteStartObject();

        for (int i = 0; i < 9; i++) {
            var prop = (byte)('1' + i);
            writer.WriteNumber(JsonEncodedText.Encode(new ReadOnlySpan<byte>(in prop)), array[i]);
        }
        writer.WriteNumber(JsonEncodedText.Encode("10"u8), array[9]);
        writer.WriteEndObject();
    }
}
