using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Revisions;
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

    /// <summary>
    /// 编辑记录类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public RevisionType Type { get; internal set; }

    /// <summary>
    /// 编辑记录创建者
    /// </summary>
    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    /// <summary>
    /// 编辑记录摘要
    /// </summary>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 编辑记录创建时间
    /// </summary>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// 编辑历史详细数据
    /// </summary>
    [JsonInclude, JsonPropertyName("data")]
    public SubjectRevisionData? Data { get; internal set; }

#pragma warning disable CS8618    
    [JsonConstructor]
    internal SubjectRevision() { }
#pragma warning restore CS8618    
}
