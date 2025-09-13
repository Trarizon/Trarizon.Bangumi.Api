using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models.Abstractions;
using Trarizon.Bangumi.Api.Responses.Models.Users;

namespace Trarizon.Bangumi.Api.Responses.Models.Revisions;
/// <summary>
/// 条目编辑历史
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L67">
/// SubjectRevision
/// </see>
/// </remarks>
public sealed class SubjectRevision : IRevision
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public RevisionType Type { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// 编辑记录详细数据
    /// </summary>
    [JsonInclude, JsonPropertyName("data")]
    public SubjectRevisionData? Data { get; internal set; }

#pragma warning disable CS8618    
    [JsonConstructor]
    internal SubjectRevision() { }
#pragma warning restore CS8618    
}
