using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters;
internal sealed class StringEnumerationJsonConverter<T> : JsonConverter<T>
    where T : IStringEnumeration<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
        return T.Create(str);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var val = value.StringValue;
        if (val is null) {
            writer.WriteNullValue();
            return;
        }

        JsonSerializer.Serialize(writer, val, BangumiJsonSerializerContext.Default.String);
    }
}
