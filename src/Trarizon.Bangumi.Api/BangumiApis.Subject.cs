using System.Collections.Immutable;
using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Requests.Entities;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Subjects;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    // routes: https://github.com/bangumi/server/blob/master/web/handler/subject/subject.go#L54
    // https://github.com/bangumi/server/tree/master/web/handler/subject

    private const string CalendarUrl = "/calendar";
    private const string SubjectsUrl = V0Url + "/subjects";

    /// <summary>
    /// 获取每日放送
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Calendar>> GetCalendarAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            CalendarUrl,
            Json.Default.Calendar, cancellationToken);
    }

    /// <summary>
    /// 获取单页条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="queries">详细条目筛选选项</param>
    /// <param name="pageLimit">单页最大数量</param>
    /// <param name="pageOffset">页面跳过的数量</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<PagedData<Subject>>> GetPagedSubjectsAsync(this IBangumiClient client, GetSubjectsQueries queries, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SubjectsUrl);
        builder.AppendQuery("type", queries.Category.SubjectType);
        if (queries is not null) {
            builder.CheckAppendQuery("cat", queries.Category.ToQueryValue());
            builder.CheckAppendQuery("series", queries.IsSeries switch { true => "true", false => "false", null => null });
            builder.CheckAppendQuery("platform", queries.GamePlatform);
            builder.CheckAppendQuery("sort", queries.Sort?.ToQueryString());
            builder.CheckAppendQuery("year", queries.Year);
            builder.CheckAppendQuery("month", queries.Month);
            builder.CheckAppendQuery("limit", pageLimit);
            builder.CheckAppendQuery("offset", pageOffset);
        }

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<Subject>> GetSubjectAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<Uri>> GetSubjectImageUrlAsync(this IBangumiClient client, uint subjectId, SubjectImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{SubjectsUrl}/{subjectId}/image?type={imageSize.ToUrlQueryString()}",
            cancellationToken)!;
    }

    /// <summary>
    /// 获取条目相关人员信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<ImmutableArray<SubjectRelatedPerson>>> GetSubjectRelatedPersonsAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<ImmutableArray<SubjectRelatedCharacter>>> GetSubjectRelatedCharactersAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<ImmutableArray<SubjectRelatedSubject>>> GetSubjectRelatedSubjectsAsync(this IBangumiClient client, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{SubjectsUrl}/{subjectId}/subjects",
            Json.Default.ImmutableArraySubjectRelatedSubject, cancellationToken);
    }
}
