using Trarizon.Bangumi.Api.Http.Requests.Entities;
using Trarizon.Bangumi.Api.Models.Subjects;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class GetSubjectsQueries
{
    public SubjectCategory? Category { get; set; }
    public bool? IsSeries { get; set; }
    public string? GamePlatform { get; set; }
    public GetSubjectsSortKind? Sort { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }

    public GetSubjectsQueries Clone() => new()
    {
        Category = Category,
        IsSeries = IsSeries,
        GamePlatform = GamePlatform,
        Sort = Sort,
        Year = Year,
        Month = Month
    };
}
