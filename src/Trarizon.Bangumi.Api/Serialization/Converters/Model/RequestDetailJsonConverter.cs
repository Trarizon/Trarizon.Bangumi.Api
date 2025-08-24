using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class RequestDetailJsonConverter : JsonConverter<RequestDetailUnion>
{
    public override RequestDetailUnion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var tokenType = reader.TokenType;
        if (tokenType is JsonTokenType.String) {
            var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
            return new(str!);
        }
        if (tokenType is JsonTokenType.StartObject) {
            var obj = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.RequestDetail);
            return new(obj!);
        }
        Throws.ThrowUnexpectedJsonToken(tokenType, JsonTokenType.String, JsonTokenType.StartObject);
        return default!;
    }

    public override void Write(Utf8JsonWriter writer, RequestDetailUnion value, JsonSerializerOptions options)
    {
        if (value.IsNull) {
            writer.WriteNullValue();
            return;
        }
        if (value.GetString() is { } str) {
            JsonSerializer.Serialize(writer, str, BangumiJsonSerializerContext.Default.String);
            return;
        }
        JsonSerializer.Serialize(writer, value.GetDetail()!, BangumiJsonSerializerContext.Default.RequestDetail);
    }
}
