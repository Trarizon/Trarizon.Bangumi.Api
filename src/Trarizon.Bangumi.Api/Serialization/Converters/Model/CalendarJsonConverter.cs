using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Serialization.Converters.Model;
internal sealed class CalendarJsonConverter : JsonConverter<Calendar>
{
    public override Calendar? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var arr = JsonSerializer.Deserialize(ref reader, BangumiJsonSerializerContext.Default.CalendarDayArray);
        if (arr is null)
            return null;
        return new Calendar(ImmutableCollectionsMarshal.AsImmutableArray(arr));
    }

    public override void Write(Utf8JsonWriter writer, Calendar value, JsonSerializerOptions options)
    {
        var arr = ImmutableCollectionsMarshal.AsArray(value.Days);
        Debug.Assert(arr is not null);
        JsonSerializer.Serialize(writer, arr, BangumiJsonSerializerContext.Default.CalendarDayArray);
    }
}
