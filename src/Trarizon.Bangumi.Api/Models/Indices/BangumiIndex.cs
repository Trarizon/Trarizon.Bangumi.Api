using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Revisions;

namespace Trarizon.Bangumi.Api.Models.Indices;
// 'Index' is widely used in BCL, so we name it as 'BangumiIndex'.
// But for other types, we still use `Index` as base name.
/// <summary>
/// 目录
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/index.go#L24">
/// Index
/// </see>
/// </remarks>
public sealed class BangumiIndex : IIndex
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    [JsonInclude, JsonPropertyName("title")]
    public string Title { get; internal set; }

    /// <summary>
    /// 目录介绍
    /// </summary>
    [JsonInclude, JsonPropertyName("desc")]
    public string Description { get; internal set; }

    // src: uint32
    /// <summary>
    /// 目录内条目数量
    /// </summary>
    [JsonInclude, JsonPropertyName("total")]
    public int SubjectCount { get; internal set; }

    /// <summary>
    /// 目录数据统计
    /// </summary>
    [JsonInclude, JsonPropertyName("stat")]
    public Statistics Statistics { get; internal set; }

    /// <summary>
    /// 目录创建时间
    /// </summary>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; internal set; }

    /// <summary>
    /// 目录更新时间
    /// </summary>
    [JsonInclude, JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedTime { get; internal set; }

    /// <summary>
    /// 目录创建者
    /// </summary>
    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    /// <summary>
    /// 目录是否为Nsfw
    /// </summary>
    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal BangumiIndex() { }
#pragma warning restore CS8618
}
