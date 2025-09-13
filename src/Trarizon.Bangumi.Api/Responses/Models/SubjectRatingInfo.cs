using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Responses.Models;
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L205
/// <summary>
/// 评分数据
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L205">
/// Rating
/// </see>
/// </remarks>
[DebuggerDisplay("Rank: {Rank}, Total: {Total}, Score: {Score}")]
public struct SubjectRatingInfo
{
    // uint32
    /// <summary>
    /// 排名
    /// </summary>
    [JsonInclude, JsonPropertyName("rank")]
    public int Rank { get; internal set; }

    // uint32
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

/// <summary>
/// 各分数的评分人数
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L192">
/// Count
/// </see>
/// </remarks>
[JsonConverter(typeof(RatingSetCountsJsonPropertyConverter))]
public readonly struct SubjectRatingCounts
{
    internal readonly int[] _counts;

    #region Number Properties

    /// <summary>
    /// 1分人数
    /// </summary>
    public int One => _counts[0];

    /// <summary>
    /// 2分人数
    /// </summary>
    public int Two => _counts[1];

    /// <summary>
    /// 3分人数
    /// </summary>
    public int Three => _counts[2];

    /// <summary>
    /// 4分人数
    /// </summary>
    public int Four => _counts[3];

    /// <summary>
    /// 5分人数
    /// </summary>
    public int Five => _counts[4];

    /// <summary>
    /// 6分人数
    /// </summary>
    public int Six => _counts[5];

    /// <summary>
    /// 7分人数
    /// </summary>
    public int Seven => _counts[6];

    /// <summary>
    /// 8分人数
    /// </summary>
    public int Eight => _counts[7];

    /// <summary>
    /// 9分人数
    /// </summary>
    public int Nine => _counts[8];

    /// <summary>
    /// 10分人数
    /// </summary>
    public int Ten => _counts[9];

    #endregion

    /// <summary>
    /// 获取指定分数的评分人数。<paramref name="score"/>范围为1~10
    /// </summary>
    /// <param name="score">评分1~10</param>
    /// <returns>指定分数的评分人数</returns>
    public int this[int score] => _counts[score - 1];

    internal SubjectRatingCounts(int[] counts)
    {
        Debug.Assert(counts.Length == 10);
        _counts = counts;
    }

    /// <summary>
    /// 获取分数的列表
    /// </summary>
    public ReadOnlySpan<int> AsSpan() => _counts.AsSpan();
}
