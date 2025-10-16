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

// src: https://github.com/bangumi/server/tree/master/web/handler/character
partial class BangumiApis
{
    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/character/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<PagedData<Character>> SearchPagedCharactersAsync(this IBangumiClient client, SearchCharactersRequestBody? requestBody, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(ApiRoutes.SearchCharactersUrl);
        builder.AppendPagination(pagination);

        return client.PostFromJsonOrThrowAsync(
            builder.Build(),
            requestBody!, Json.Default.SearchCharactersRequestBody,
            Json.Default.PagedDataCharacter, cancellationToken);
    }

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Character> GetCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}",
            Json.Default.Character, cancellationToken);
    }

    /// <summary>
    /// 获取角色图片
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="imageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Uri> GetCharacterImageUrlAsync(this IBangumiClient client, uint characterId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetHeadersLocationOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}/image?type={imageSize.ToQueryString()}",
            cancellationToken);
    }

    /// <summary>
    /// 获取角色关联条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<CharacterRelatedSubject>> GetCharacterRelatedSubjectAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}/subjects",
            Json.Default.ImmutableArrayCharacterRelatedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取角色关联人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<ImmutableArray<CharacterRelatedPerson>> GetCharacterRelatedPersonAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}/persons",
            Json.Default.ImmutableArrayCharacterRelatedPerson, cancellationToken);
    }

    /// <summary>
    /// 收藏角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task CollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.PostOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}/collect", cancellationToken);
    }

    /// <summary>
    /// 取消收藏角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task UncollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.DeleteOrThrowAsync(
            $"{ApiRoutes.CharactersUrl}/{characterId}/collect", cancellationToken);
    }
}
