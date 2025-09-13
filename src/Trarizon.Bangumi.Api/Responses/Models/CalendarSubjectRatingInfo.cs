using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models;
/// <summary>
/// 评分数据
/// </summary>
[DebuggerDisplay("Total: {Total}, Score: {Score}")]
public struct CalendarSubjectRatingInfo
{
    /// <summary>
    /// 总评分人数
    /// </summary>
    [JsonInclude, JsonPropertyName("total")]
    public int Total { get; internal set; }

    /// <summary>
    /// 评分详细统计
    /// </summary>
    [JsonInclude, JsonPropertyName("count")]
    public SubjectRatingCounts Counts { get; internal set; }

    /// <summary>
    /// 分数
    /// </summary>
    [JsonInclude, JsonPropertyName("score")]
    public double Score { get; internal set; }
}
