using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class SearchPersonsRequestBody
{
    [JsonInclude, JsonPropertyName("keyword")]
    public required string Keyword { get; set; }

    [JsonInclude, JsonPropertyName("filter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SearchPersonsFilter? Filter { get; set; }

    public SearchPersonsRequestBody Clone() => new()
    {
        Keyword = Keyword,
        Filter = Filter?.Clone(),
    };
}
