using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class GenderJsonConverter : JsonConverter<Gender>
{
    public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
        return Gender.FromJsonStringValue(str);
    }

    public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
    {
        var str = value.ToJsonStringValue();
        JsonSerializer.Serialize(writer, str, BangumiJsonSerializerContext.Default.String);
    }
}

internal sealed class PersonCareerJsonConverter : JsonConverter<PersonCareer>
{
    public override PersonCareer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
        return PersonCareer.FromJsonStringValue(str);
    }

    public override void Write(Utf8JsonWriter writer, PersonCareer value, JsonSerializerOptions options)
    {
        var str = value.ToJsonStringValue();
        JsonSerializer.Serialize(writer, str, BangumiJsonSerializerContext.Default.String);
    }
}

internal sealed class SearchSubjectsSortJsonConverter : JsonConverter<SearchSubjectsSort>
{
    public override SearchSubjectsSort Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.String);
        return SearchSubjectsSort.FromJsonStringValue(str);
    }

    public override void Write(Utf8JsonWriter writer, SearchSubjectsSort value, JsonSerializerOptions options)
    {
        var str = value.ToJsonStringValue();
        JsonSerializer.Serialize(writer, str, BangumiJsonSerializerContext.Default.String);
    }
}
