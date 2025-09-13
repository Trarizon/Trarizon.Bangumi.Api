using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models;
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L180
/// <summary>
/// 条目收藏统计
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L180">
/// SubjectCollectionStat
/// </see>
/// </remarks>

[DebuggerDisplay("{Wish} + {Collect} + {Doing} + {OnHold} + {Dropped}")]
public struct SubjectCollectionStatistics
{
    // 以下属性源码均为uint32

    /// <summary>
    /// 想看
    /// </summary>
    [JsonInclude, JsonPropertyName("wish")]
    public int Wish { get; internal set; }

    /// <summary>
    /// 已看
    /// </summary>
    [JsonInclude, JsonPropertyName("collect")]
    public int Collect { get; internal set; }

    /// <summary>
    /// 在看
    /// </summary>
    [JsonInclude, JsonPropertyName("doing")]
    public int Doing { get; internal set; }

    /// <summary>
    /// 搁置
    /// </summary>
    [JsonInclude, JsonPropertyName("on_hold")]
    public int OnHold { get; internal set; }

    /// <summary>
    /// 抛弃
    /// </summary>
    [JsonInclude, JsonPropertyName("dropped")]
    public int Dropped { get; internal set; }
}
