using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
public enum GetSubjectsSortKind
{
    Date,
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