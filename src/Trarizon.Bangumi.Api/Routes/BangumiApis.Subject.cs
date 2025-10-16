using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Requests.Payloads;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Utilities;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;

// routes: https://github.com/bangumi/server/blob/master/web/handler/subject/subject.go#L54
//         https://github.com/bangumi/server/tree/master/web/handler/subject
partial class BangumiApis
{
    /// <summary>
    /// 获取每日放送
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Calendar> GetCalendarAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            ApiRoutes.CalendarUrl,
            Json.Default.Calendar, cancellationToken);
    }

    /// <summary>
    /// 搜索条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pagination">分页参数</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/subject/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<PagedData<SearchResponsedSubject>> SearchPagedSubjectsAsync(this IBangumiClient client, SearchSubjectsRequestBody? requestBody, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(ApiRoutes.SearchSubjectsUrl);
        builder.AppendPagination(pagination);

        return client.PostFromJsonOrThrowAsync(
            builder.Build(),
            requestBody!, Json.Default.SearchSubjectsRequestBody,
            Json.Default.PagedDataSearchResponsedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取单页条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="query">详细条目筛选选项</param>
    /// <param name="pagination">分页参数</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<Subject>> GetPagedSubjectsAsync(this IBangumiClient client, GetSubjectsQuery query, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(ApiRoutes.SubjectsUrl);
        builder.Append(query);
        builder.AppendPagination(pagination);

        return client.GetFromJsonOrThrowAsync(
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
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.SubjectsUrl}/{subjectId}",
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
        return client.GetHeadersLocationOrThrowAsync(
            $"{ApiRoutes.SubjectsUrl}/{subjectId}/image?type={imageSize.ToQueryString()}",
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
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.SubjectsUrl}/{subjectId}/persons",
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
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.SubjectsUrl}/{subjectId}/characters",
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
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.SubjectsUrl}/{subjectId}/subjects",
            Json.Default.ImmutableArraySubjectRelatedSubject, cancellationToken);
    }
}
