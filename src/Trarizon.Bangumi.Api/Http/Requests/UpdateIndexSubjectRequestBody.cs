using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class UpdateIndexSubjectRequestBody
{
    [JsonInclude, JsonPropertyName("sort")]
    public int? Sort { get; set; }

    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; set; }

    public UpdateIndexSubjectRequestBody Clone() => new()
    {
        Sort = Sort,
        Comment = Comment,
    };
}
