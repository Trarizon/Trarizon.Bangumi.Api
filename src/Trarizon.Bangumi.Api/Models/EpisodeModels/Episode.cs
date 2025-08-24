using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Serialization.Converters;

namespace Trarizon.Bangumi.Api.Models.EpisodeModels;
// src: 
/// <summary>
/// 章节
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/episode.go#23">
/// Episode
/// </see>
/// </remarks>
[DebuggerDisplay("[{Ep}] {Name}")]
public sealed class Episode : IEpisode
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 章节类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public EpisodeType Type { get; internal set; }

    /// <summary>
    /// 章节名
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 章节中文名
    /// </summary>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <summary>
    /// 同类条目的排序和集数
    /// </summary>
    [JsonInclude, JsonPropertyName("sort")]
    public float Sort { get; internal set; }

    // api: 未指定为非null
    /// <summary>
    /// 条目内的集数, 从1开始。非本篇剧集的此字段无意义
    /// </summary>
    [JsonInclude, JsonPropertyName("ep")]
    public float Ep { get; internal set; }

    // src: int
    // ConvertModelEpisode的构造中，这个值就是WikiDuration parse来的
    /// <summary>
    /// 服务器解析的时长，无法解析时为0
    /// </summary>
    [JsonInclude, JsonPropertyName("duration_seconds")]
    [JsonConverter(typeof(TimeSpanBySecondsJsonConverter))]
    public TimeSpan Duration { get; internal set; }

    /// <summary>
    /// 维基人填写的时长
    /// </summary>
    [JsonInclude, JsonPropertyName("duration")]
    public string WikiDuration { get; internal set; }

    // src: string
    /// <summary>
    /// 放送日期
    /// </summary>
    [JsonInclude, JsonPropertyName("airdate")]
    public DateOnly? AirDate { get; internal set; }

    // api: scheme没有，实际存在
    /// <summary>
    /// 关联的条目ID
    /// </summary>
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    // src: uint32
    /// <summary>
    /// 评论数量
    /// </summary>
    [JsonInclude, JsonPropertyName("comment")]
    public int CommentCount { get; internal set; } 

    /// <summary>
    /// 简介
    /// </summary>
    [JsonInclude, JsonPropertyName("desc")]
    public string Description { get; internal set; }

    // src: uint8
    /// <summary>
    /// 音乐曲目的碟片数
    /// </summary>
    [JsonInclude, JsonPropertyName("disc")]
    public int DiscCount { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Episode() { }
#pragma warning restore CS8618
}
