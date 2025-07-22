using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class SearchPersonsFilter
{
    [JsonInclude, JsonPropertyName("career"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<PersonCareer> Careers { get => field ??= []; set; }

    public SearchPersonsFilter Clone() => new()
    {
        Careers = [.. Careers],
    };
}
