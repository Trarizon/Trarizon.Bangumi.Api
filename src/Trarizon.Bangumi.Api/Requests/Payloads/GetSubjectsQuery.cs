using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Requests.Payloads;
/// <summary>
/// 
/// </summary>
public sealed class GetSubjectsQuery
{
    /// <summary>
    /// 条目类型与分类筛选
    /// </summary>
    public required SubjectCategory Category { get; set; }

    /// <inheritdoc cref="Subject.IsSeries" />
    public bool? IsSeries { get; set; }

    /// <summary>
    /// 平台，仅对游戏类型有效
    /// </summary>
    public string? GamePlatform { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public SubjectsSortKind? Sort { get; set; }
    /// <summary>
    /// 年份
    /// </summary>
    public int? Year { get; set; }
    /// <summary>
    /// 月份
    /// </summary>
    public int? Month { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public GetSubjectsQuery Clone() => new()
    {
        Category = Category,
        IsSeries = IsSeries,
        GamePlatform = GamePlatform,
        Sort = Sort,
        Year = Year,
        Month = Month
    };

#pragma warning restore CS1591
}

internal static class GetSubjectsQueryExtensions
{
    internal static void Append(this QueryBuilder builder, GetSubjectsQuery query)
    {
        builder.AppendQuery("type", query.Category.SubjectType.ToQueryValue());
        builder.TryAppendQuery("cat", query.Category.ToQueryValue());
        builder.TryAppendQuery("series", query.IsSeries switch { true => "true", false => "false", null => null });
        builder.TryAppendQuery("platform", query.GamePlatform);
        builder.TryAppendQuery("sort", query.Sort?.ToQueryString());
        builder.TryAppendQuery("year", query.Year);
        builder.TryAppendQuery("month", query.Month);
    }
}
