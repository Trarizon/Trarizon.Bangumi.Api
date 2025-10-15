using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Payloads;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

    // src: https://github.com/bangumi/server/tree/master/web/handler/person
partial class BangumiApis
{
    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/person/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<PagedData<Person>> SearchPagedPersonsAsync(this IBangumiClient client, SearchPersonsRequestBody? requestBody, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(ApiRoutes.SearchPersonsUrl);
        builder.AppendPagination(pagination);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            builder.Build(),
            requestBody!, Json.Default.SearchPersonsRequestBody,
            Json.Default.PagedDataPerson, cancellationToken);
    }

    /// <summary>
    /// 获取人物信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Person> GetPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync($"{ApiRoutes.PersonsUrl}/{personId}",
            Json.Default.Person, cancellationToken);
    }

    /// <summary>
    /// 获取人物图片
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="imageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Uri> GetPersonImageUrlAsync(this IBangumiClient client, uint personId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetHeadersLocationWhenStatusFoundOrThrowAsync(
            $"{ApiRoutes.PersonsUrl}/{personId}/image?type={imageSize.ToQueryString()}", cancellationToken)!;
    }

    /// <summary>
    /// 获取人物关联条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<PersonRelatedSubject>> GetPersonRelatedSubjectsAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.PersonsUrl}/{personId}/subjects",
            Json.Default.ImmutableArrayPersonRelatedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取人物关联角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<PersonRelatedCharacter>> GetPersonRelatedCharactersAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.PersonsUrl}/{personId}/characters",
            Json.Default.ImmutableArrayPersonRelatedCharacter, cancellationToken);
    }

    /// <summary>
    /// 收藏人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task CollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.PersonsUrl}/{personId}/collect", cancellationToken);
    }

    /// <summary>
    /// 取消收藏人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task UncollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.PersonsUrl}/{personId}/collect", cancellationToken);
    }
}
