using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L44">
/// SubjectV0
/// </see>
/// </remarks>
[DebuggerDisplay("{Name}")]
public sealed class Subject : ISubject
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 条目类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <summary>
    /// 条目名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 条目中文名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <summary>
    /// 条目描述
    /// </summary>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 是否为书籍系列的主条目
    /// </summary>
    [JsonInclude, JsonPropertyName("series")]
    public bool IsSeries { get; internal set; }

    /// <summary>
    /// 是否为NSFW条目
    /// </summary>
    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

    /// <summary>
    /// 条目是否已锁定
    /// </summary>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

    // 源码内部存的也是string，所以这里选择不转为DateOnly
    /// <summary>
    /// 条目开始放送日期，格式为YYYY-MM-DD
    /// </summary>
    [JsonInclude, JsonPropertyName("date")]
    public string? Date { get; internal set; }
    
    /// <summary>
    /// 平台(TV, Web, 欧美剧, DLC...)
    /// </summary>
    [JsonInclude, JsonPropertyName("platform")]
    public string? Platform { get; internal set; }

    /// <summary>
    /// 条目图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }

    /// <summary>
    /// 条目信息
    /// </summary>
    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; } // 源码非指针，schceme nullable

    // src: uint32
    /// <summary>
    /// 书籍条目的册数
    /// </summary>
    /// <remarks>
    /// 由旧服务端从wiki中解析
    /// </remarks>
    [JsonInclude, JsonPropertyName("volumes")]
    public int VolumeCount { get; internal set; }

    // src: uint32
    /// <summary>
    /// 章节数量，或书籍条目的话数
    /// </summary>
    /// <remarks>
    /// 由旧服务端从wiki中解析
    /// </remarks>
    [JsonInclude, JsonPropertyName("eps")]
    public int EpisodeCount { get; internal set; }

    /// <summary>
    /// 数据库中的章节数量
    /// </summary>
    [JsonInclude, JsonPropertyName("total_episodes")]
    public long TotalEpisodeCount { get; internal set; }

    /// <summary>
    /// 条目评分信息
    /// </summary>
    [JsonInclude, JsonPropertyName("rating")]
    public RatingInfo Rating { get; internal set; }

    /// <summary>
    /// 条目收藏统计
    /// </summary>
    [JsonInclude, JsonPropertyName("collection")]
    public SubjectCollectionStatistics Collection { get; internal set; }

    /// <summary>
    /// 由维基人维护的tag
    /// </summary>
    [JsonInclude, JsonPropertyName("meta_tags")]
    public ImmutableArray<string> MetaTags { get; internal set; }

    /// <summary>
    /// 用户自定义的条目tag
    /// </summary>
    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<SubjectTag> Tags { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Subject() { }
#pragma warning restore CS8618 
}
