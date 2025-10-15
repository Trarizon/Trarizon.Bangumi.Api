using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Models;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class NsfwFilterJsonConverter : JsonConverter<NsfwFilter>
{
    public override NsfwFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return NsfwFilter.FromJsonValue(JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.NullableBoolean));
    }

    public override void Write(Utf8JsonWriter writer, NsfwFilter value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToJsonValue(), BangumiJsonSerializerContext.Default.NullableBoolean);
    }
}
