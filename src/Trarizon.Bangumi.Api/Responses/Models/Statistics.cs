using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models;
// https://github.com/bangumi/server/blob/master/web/res/common.go#L17
/// <summary>
/// 数据统计
/// </summary>
public struct Statistics
{
    // src: 属性均为uint32

    /// <summary>
    /// 评论数
    /// </summary>
    [JsonInclude, JsonPropertyName("comments")]
    public int CommentCount { get; internal set; }

    /// <summary>
    /// 收藏数
    /// </summary>
    [JsonInclude, JsonPropertyName("collects")]
    public int CollectCount { get; internal set; }
}
