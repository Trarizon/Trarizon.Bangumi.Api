using Trarizon.Bangumi.Api.Internal.Attributes;

namespace Trarizon.Bangumi.Api.Requests.Models;
/// <summary>
/// 排序
/// </summary>
[QueryStringEnum]
public enum GetSubjectsSortKind
{
    /// <summary>
    /// 日期
    /// </summary>
    Date,
    /// <summary>
    /// 排名
    /// </summary>
    Rank
}
