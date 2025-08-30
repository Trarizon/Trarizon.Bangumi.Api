using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // routes: https://github.com/bangumi/server/blob/master/web/handler/subject/subject.go#L54
    //         https://github.com/bangumi/server/tree/master/web/handler/subject

    private const string CalendarUrl = "/calendar";
    private const string SubjectsUrl = V0Url + "/subjects";
    private const string SearchSubjectsUrl = SearchUrl + "/subjects";

    /// <summary>
    /// 获取每日放送
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Calendar> GetCalendarAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            CalendarUrl,
            Json.Default.Calendar, cancellationToken);
    }

    /// <summary>
    /// 搜索条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pageLimit">单页最大数量，该值必须大于0，过大会被API限制在maxLimit内</param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/subject/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<PagedData<SearchResponsedSubject>> SearchPagedSubjectsAsync(this IBangumiClient client, SearchSubjectsRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchSubjectsUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            builder.Build(),
            requestBody!, Json.Default.SearchSubjectsRequestBody,
            Json.Default.PagedDataSearchResponsedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取单页条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="query">详细条目筛选选项</param>
    /// <param name="pageLimit">单页最大数量</param>
    /// <param name="pageOffset">页面跳过的数量</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<Subject>> GetPagedSubjectsAsync(this IBangumiClient client, GetSubjectsQuery query, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SubjectsUrl);
        builder.AppendQuery("type", query.Category.SubjectType.ToQueryValue());
        if (query is not null) {
            builder.CheckAppendQuery("cat", query.Category.ToQueryValue());
            builder.CheckAppendQuery("series", query.IsSeries switch { true => "true", false => "false", null => null });
            builder.CheckAppendQuery("platform", query.GamePlatform);
            builder.CheckAppendQuery("sort", query.Sort?.ToQueryString());
            builder.CheckAppendQuery("year", query.Year);
            builder.CheckAppendQuery("month", query.Month);
            builder.CheckAppendQuery("limit", pageLimit);
            builder.CheckAppendQuery("offset", pageOffset);
        }

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            builder.Build(),
            Json.Default.PagedDataSubject, cancellationToken);
    }

    /// <summary>
    /// 获取条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Subject> GetSubjectAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{SubjectsUrl}/{subjectId}",
            Json.Default.Subject, cancellationToken);
    }

    /// <summary>
    /// 获取条目图片路径
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="imageSize">图片尺寸</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Uri> GetSubjectImageUrlAsync(this IBangumiClient client, uint subjectId, SubjectImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetHeadersLocationWhenStatusFoundOrThrowAsync(
            $"{SubjectsUrl}/{subjectId}/image?type={imageSize.ToQueryString()}",
            cancellationToken)!;
    }

    /// <summary>
    /// 获取条目相关人员信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<SubjectRelatedPerson>> GetSubjectRelatedPersonsAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{SubjectsUrl}/{subjectId}/persons",
            Json.Default.ImmutableArraySubjectRelatedPerson, cancellationToken);
    }

    /// <summary>
    /// 获取条目相关角色信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<SubjectRelatedCharacter>> GetSubjectRelatedCharactersAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{SubjectsUrl}/{subjectId}/characters",
            Json.Default.ImmutableArraySubjectRelatedCharacter, cancellationToken);
    }

    /// <summary>
    /// 获取条目相关条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<SubjectRelatedSubject>> GetSubjectRelatedSubjectsAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{SubjectsUrl}/{subjectId}/subjects",
            Json.Default.ImmutableArraySubjectRelatedSubject, cancellationToken);
    }
}
