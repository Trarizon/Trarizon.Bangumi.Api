using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// 
/// </summary>
public sealed class AddIndexSubjectRequestBody
{
    /// <inheritdoc cref="Subject.Id"/>
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; set; }

    /// <inheritdoc cref="UpdateIndexSubjectRequestBody.Sort"/>
    [JsonInclude, JsonPropertyName("sort")]
    public int? Sort { get; set; }

    /// <inheritdoc cref="IndexSubject.Comment"/>
    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; set; }

#pragma warning disable CS1591 

    public AddIndexSubjectRequestBody Clone() => new()
    {
        SubjectId = SubjectId,
        Sort = Sort,
        Comment = Comment,
    };

#pragma warning restore CS1591 
}
