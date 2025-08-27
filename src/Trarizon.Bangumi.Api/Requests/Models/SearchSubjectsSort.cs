using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Requests.Models;
/// <summary>
/// 条目搜索结果排序方式
/// </summary>
[JsonConverter(typeof(SearchSubjectsSortJsonConverter))]
[JsonStringEnum]
public enum SearchSubjectsSort
{
    /// <summary>
    /// 匹配程度（默认）
    /// </summary>
    Match,
    /// <summary>
    /// 收藏人数
    /// </summary>
    Heat,
    /// <summary>
    /// 排名由高到低
    /// </summary>
    Rank,
    /// <summary>
    /// 评分
    /// </summary>
    Score
}

