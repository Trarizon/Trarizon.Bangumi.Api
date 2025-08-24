using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class InfoValueJsonConverter : JsonConverter<InfoValues>
{
    public override InfoValues Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType) {
            case JsonTokenType.StartArray:
                var pairs = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.InfoValueArray);
                return new InfoValues(ImmutableCollectionsMarshal.AsImmutableArray(pairs));
            case JsonTokenType.String:
                return new InfoValues(reader.GetString()!);
            case JsonTokenType.Null:
                return default;
            default:
                Throws.ThrowUnexpectedJsonToken(reader.TokenType, JsonTokenType.StartArray, JsonTokenType.String, JsonTokenType.Null);
                return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, InfoValues value, JsonSerializerOptions options)
    {
        if (value.IsRawValueString()) {
            writer.WriteStringValue(value.GetRawStringValue());
        }
        else {
            JsonSerializer.Serialize(writer, ImmutableCollectionsMarshal.AsArray(value.GetRawPairsValue())!, BangumiJsonSerializerContext.Default.InfoValueArray);
        }
    }
}
