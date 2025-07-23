using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests.Entities;

namespace Trarizon.Bangumi.Api.Http.Requests;
/// <summary>
/// 
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/subject/handle.go#L46">
/// Req
/// </see>
/// </remarks>
public sealed class SearchSubjectsRequestBody
{
    /// <summary>
    /// 搜索关键字
    /// </summary>
    [JsonInclude, JsonPropertyName("keyword")]
    public string? Keyword { get; set; }

    /// <summary>
    /// 结果排序
    /// </summary>
    [JsonInclude, JsonPropertyName("sort"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SearchSubjectsSort Sort { get; set; }

    /// <summary>
    /// 筛选条件
    /// </summary>
    [JsonInclude, JsonPropertyName("filter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SearchSubjectsFilter? Filter { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public SearchSubjectsRequestBody Clone() => new()
    {
        Keyword = Keyword,
        Sort = Sort,
        Filter = Filter?.Clone(),
    };

#pragma warning restore CS1591
}
