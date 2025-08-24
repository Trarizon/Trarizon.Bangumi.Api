using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
// src: Legacy_SubjectSmall
/// <summary>
/// 每日放送中的条目信息
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class CalendarSubject : ISubject
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 条目路径
    /// </summary>
    [JsonInclude, JsonPropertyName("url")]
    public string Url { get; internal set; }

    /// <inheritdoc cref="Subject.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="Subject.Name"/>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName"/>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    // api: 有返回，但是全空
    /// <inheritdoc cref="Subject.Summary"/>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    // api: string
    /// <inheritdoc cref="Subject.Date"/>
    [JsonInclude, JsonPropertyName("air_date")]
    public DateOnly AirDate { get; internal set; }

    /// <summary>
    /// 放送星期
    /// </summary>
    [JsonInclude, JsonPropertyName("air_weekday")]
    public DayOfWeek AirWeekDay { get; internal set; }

    /// <inheritdoc cref="Subject.Images"/>
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet? Images { get; internal set; }

    // api: 实际返回结果不存在
    /// <summary>
    /// 条目章节数量
    /// </summary>
    /// <remarks>
    /// 目前不知道eps和eps_count有什么区别
    /// </remarks>
    [JsonInclude, JsonPropertyName("eps")]
    public int Episodes { get; internal set; }

    // api: 实际返回结果不存在
    /// <summary>
    /// 条目章节数量
    /// </summary>
    [JsonInclude, JsonPropertyName("eps_count")]
    public int EpisodeCount { get; internal set; }

    // api: 没有rank
    /// <summary>
    /// 条目评分
    /// </summary>
    [JsonInclude, JsonPropertyName("rating")]
    public CalendarSubjectRatingInfo Rating { get; internal set; }

    /// <summary>
    /// 条目排名
    /// </summary>
    [JsonInclude, JsonPropertyName("rank")]
    public int Rank { get; internal set; }

    // api: 只返回了doing
    /// <summary>
    /// 条目收藏数据
    /// </summary>
    [JsonInclude, JsonPropertyName("collection")]
    public SubjectCollectionStatistics Collection { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal CalendarSubject() { }
#pragma warning restore CS8618 
}
