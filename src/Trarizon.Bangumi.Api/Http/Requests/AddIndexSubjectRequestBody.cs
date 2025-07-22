using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class AddIndexSubjectRequestBody
{
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; set; }

    [JsonInclude, JsonPropertyName("sort")]
    public int? Sort { get; set; }

    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; set; }

    public AddIndexSubjectRequestBody Clone() => new()
    {
        SubjectId = SubjectId,
        Sort = Sort,
        Comment = Comment,
    };
}
