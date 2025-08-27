using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/tree/master/web/handler/person

    private const string PersonsUrl = V0Url + "/persons";
    private const string SearchPersonsUrl = SearchUrl + "/persons";

    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/person/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<PagedData<Person>> SearchPagedPersonsAsync(this IBangumiClient client, SearchPersonsRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchPersonsUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

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
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync($"{PersonsUrl}/{personId}",
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
            $"{PersonsUrl}/{personId}/image?type={imageSize.ToQueryString()}", cancellationToken)!;
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
            $"{PersonsUrl}/{personId}/subjects",
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
            $"{PersonsUrl}/{personId}/characters",
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
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
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
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
    }
}
