using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class InfoBoxJsonConverter : JsonConverter<InfoBox>
{
    public override InfoBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var arr = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.InfoPropertyArray);
        return new InfoBox(ImmutableCollectionsMarshal.AsImmutableArray(arr ?? []));
    }

    public override void Write(Utf8JsonWriter writer, InfoBox value, JsonSerializerOptions options)
    {
        var arr = ImmutableCollectionsMarshal.AsArray(value.Properties);
        JsonSerializer.Serialize(writer, arr!, BangumiJsonSerializerContext.Default.InfoPropertyArray);
    }
}
