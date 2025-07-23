using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
/// <summary>
/// 排序
/// </summary>
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

internal static class SubjectsSortKindExtensions
{
    public static string ToQueryString(this GetSubjectsSortKind subjectsSortKind) => subjectsSortKind switch
    {
        GetSubjectsSortKind.Date => "date",
        GetSubjectsSortKind.Rank => "sort",
        _ => Throws.ThrowUnknownEnumValue<string>(subjectsSortKind),
    };
}