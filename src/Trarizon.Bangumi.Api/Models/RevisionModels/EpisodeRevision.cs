using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.RevisionModels;
/// <summary>
/// 章节编辑历史
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L92">
/// EpisodeRevision
/// </see>
/// </remarks>
public sealed class EpisodeRevision : IRevision
{
    /// <inheritdoc cref="SubjectRevision.Id"/>
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="SubjectRevision.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public RevisionType Type { get; internal set; }

    /// <inheritdoc cref="SubjectRevision.Creator"/>
    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    /// <inheritdoc cref="SubjectRevision.Summary"/>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <inheritdoc cref="SubjectRevision.CreatedTime"/>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; set; }

    /// <inheritdoc cref="SubjectRevision.Data"/>
    [JsonInclude, JsonPropertyName("data")]
    public IReadOnlyDictionary<string, EpisodeRevisionDataItem>? Data { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618    
    internal EpisodeRevision() { }
#pragma warning restore CS8618    
}
