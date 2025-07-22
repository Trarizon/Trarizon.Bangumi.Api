using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests.Entities;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class SearchSubjectsRequestBody
{
    [JsonInclude, JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    [JsonInclude, JsonPropertyName("sort"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SearchSubjectsSort Sort { get; set; }

    [JsonInclude, JsonPropertyName("filter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SearchSubjectsFilter? Filter { get; set; }

    public SearchSubjectsRequestBody Clone() => new()
    {
        Keyword = Keyword,
        Sort = Sort,
        Filter = Filter?.Clone(),
    };
}
