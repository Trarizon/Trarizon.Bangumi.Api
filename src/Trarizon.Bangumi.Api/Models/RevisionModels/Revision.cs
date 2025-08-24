using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.RevisionModels;
/// <summary>
/// 编辑历史
/// </summary>
public sealed class Revision : IRevision
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 编辑类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public RevisionType Type { get; internal set; }

    /// <summary>
    /// 编辑记录创建者
    /// </summary>
    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    /// <summary>
    /// 编辑摘要
    /// </summary>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Revision() { }
#pragma warning restore CS8618
}
