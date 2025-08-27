using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.PersonModels;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class PersonCareerJsonConverter : JsonConverter<PersonCareer>
{
    public override PersonCareer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
        return PersonCareer.FromJsonStringValue(str!);
    }

    public override void Write(Utf8JsonWriter writer, PersonCareer value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToJsonStringValue(), BangumiJsonSerializerContext.Default.String);
    }
}
