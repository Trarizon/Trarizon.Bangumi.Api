using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests.Entities;

namespace Trarizon.Bangumi.Api.Serialization.Http;
internal sealed class NsfwFilterJsonConverter : JsonConverter<NsfwFilter>
{
    public override NsfwFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return NsfwFilter.FromRequestJson(JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.NullableBoolean));
    }

    public override void Write(Utf8JsonWriter writer, NsfwFilter value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToRequestJsonValue(), BangumiJsonSerializerContext.Default.NullableBoolean);
    }
}
