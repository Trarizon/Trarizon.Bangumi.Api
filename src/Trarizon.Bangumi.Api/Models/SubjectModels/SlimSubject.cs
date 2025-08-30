using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L67">
/// SlimSubjectV0
/// </see>
/// </remarks>
public sealed class SlimSubject : ISubject, ISubjectImagesProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <summary>
    /// 截断后的条目描述
    /// </summary>
    [JsonInclude, JsonPropertyName("short_summary")]
    public string TruncatedSummary { get; internal set; }

    // 使用string原因见Subject
    /// <inheritdoc cref="Subject.Date" />
    [JsonInclude, JsonPropertyName("date")]
    public string? Date { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }

    // src: uint32
    /// <inheritdoc cref="Subject.VolumeCount"/>
    [JsonInclude, JsonPropertyName("volumes")]
    public int VolumeCount { get; internal set; }

    // src: uint32
    /// <inheritdoc cref="Subject.EpisodeCount"/>
    [JsonInclude, JsonPropertyName("eps")]
    public int EpisodeCount { get; internal set; }

    // src: uint32
    /// <summary>
    /// 条目收藏人数
    /// </summary>
    [JsonInclude, JsonPropertyName("collection_total")]
    public int TotalCollectionCount { get; internal set; }

    /// <summary>
    /// 条目分数
    /// </summary>
    [JsonInclude, JsonPropertyName("score")]
    public double Score { get; internal set; }

    // src: uint32
    /// <summary>
    /// 条目排名
    /// </summary>
    [JsonInclude, JsonPropertyName("rank")]
    public int Rank { get; internal set; }

    /// <summary>
    /// 前10个tag
    /// </summary>
    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<SubjectTag> TagsPreview { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal SlimSubject() { }
#pragma warning restore CS8618
}
