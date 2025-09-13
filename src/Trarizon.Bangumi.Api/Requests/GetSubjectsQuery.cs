using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Requests;
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
    public GetSubjectsSortKind? Sort { get; set; }
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
