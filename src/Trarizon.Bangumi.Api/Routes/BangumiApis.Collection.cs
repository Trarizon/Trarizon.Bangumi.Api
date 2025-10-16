using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Payloads;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Responses.Models.Collections;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

// src: https://github.com/bangumi/server/tree/master/web/handler/user
partial class BangumiApis
{
    /// <summary>
    /// 获取单页用户条目收藏信息
    /// </summary>
    public static Task<PagedData<UserSubjectCollection>> GetPagedUserSubjectCollectionsAsync(this IBangumiClient client, string userName, SubjectType? subjectType = null, SubjectCollectionType? collectionType = null, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{ApiRoutes.UsersUrl}/{userName}/collections");
        builder.TryAppendQuery("subject_type", subjectType?.ToQueryValue());
        builder.TryAppendQuery("type", collectionType?.ToQueryValue());
        builder.AppendPagination(pagination);

        return client.GetFromJsonOrThrowAsync(builder.Build(),
            Json.Default.PagedDataUserSubjectCollection, cancellationToken);
    }

    /// <summary>
    /// 获取用户条目收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<UserSubjectCollection> GetUserSubjectCollectionAsync(this IBangumiClient client, string userName, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.UsersUrl}/{userName}/collections/{subjectId}",
            Json.Default.UserSubjectCollection, cancellationToken);
    }

    /// <summary>
    /// 修改条目收藏信息, 如果不存在则创建，如果存在则修改
    /// <br/>
    /// 由于直接修改剧集条目的完成度可能会引起意料之外效果，只能用于修改书籍类条目的完成度。
    /// </summary>
    public static Task AddOrUpdateUserSubjectCollectionAsync(this IBangumiClient client, uint subjectId, UpdateUserSubjectCollectionRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostOrThrowAsync(
            $"{BangumiApiRoutes.UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, Json.Default.UpdateUserSubjectCollectionRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 修改条目收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task UpdateUserSubjectCollectionAsync(this IBangumiClient client, uint subjectId, UpdateUserSubjectCollectionRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PatchOrThrowAsync(
            $"{BangumiApiRoutes.UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, Json.Default.UpdateUserSubjectCollectionRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页用户章节收藏信息
    /// </summary>
    public static Task<PagedData<UserEpisodeCollection>> GetPagedUserSubjectEpisodeCollectionsAsync(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{BangumiApiRoutes.UserCollectionsUrl}/{subjectId}/episodes");
        builder.AppendPagination(pagination);
        builder.TryAppendQuery("episode_type", episodeType?.ToQueryValue());

        return client.GetFromJsonOrThrowAsync(
            builder.Build(),
            Json.Default.PagedDataUserEpisodeCollection,
            cancellationToken);
    }

    /// <summary>
    /// 修改用户对单个条目的章节收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task UpdateUserSubjectEpisodeCollectionsAsync(this IBangumiClient client, uint subjectId, UpdateUserSubjectEpisodeCollectionsRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PatchOrThrowAsync(
            $"{BangumiApiRoutes.UserCollectionsUrl}/{subjectId}/episodes",
            requestBody, Json.Default.UpdateUserSubjectEpisodeCollectionsRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取用户章节收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<UserEpisodeCollection> GetUserCollectionEpisodeAsync(this IBangumiClient client, uint episodeId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{BangumiApiRoutes.UserCollectionsUrl}/-/episodes/{episodeId}",
            Json.Default.UserEpisodeCollection, cancellationToken);
    }

    /// <summary>
    /// 修改用户章节收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task UpdateUserEpisodeCollectionAsync(this IBangumiClient client, uint episodeId, UpdateUserEpisodeCollectionRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutOrThrowAsync(
            $"{BangumiApiRoutes.UserCollectionsUrl}/-/episodes/{episodeId}",
            requestBody, Json.Default.UpdateUserEpisodeCollectionRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页用户角色收藏信息
    /// </summary>
    public static Task<PagedData<UserCharacterCollection>> GetPagedUserCharacterCollectionsAsync(this IBangumiClient client, string userName,Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{ApiRoutes.UsersUrl}/{userName}/collections/-/characters");
        builder.AppendPagination(pagination);

        return client.GetFromJsonOrThrowAsync(
            builder.Build(),
            Json.Default.PagedDataUserCharacterCollection, cancellationToken);
    }

    /// <summary>
    /// 获取用户角色收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<UserCharacterCollection> GetUserCharacterCollectionAsync(this IBangumiClient client, string userName, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.UsersUrl}/{userName}/collections/-/characters/{characterId}",
            Json.Default.UserCharacterCollection, cancellationToken);
    }

    /// <summary>
    /// 获取单页用户人物收藏信息
    /// </summary>
    public static Task<PagedData<UserPersonCollection>> GetPagedUserPersonCollectionsAsync(this IBangumiClient client, string userName, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{ApiRoutes.UsersUrl}/{userName}/collections/-/persons");
        builder.AppendPagination(pagination);

        return client.GetFromJsonOrThrowAsync(
            builder.Build(),
            Json.Default.PagedDataUserPersonCollection, cancellationToken);
    }

    /// <summary>
    /// 获取用户人物收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<UserPersonCollection> GetUserPersonCollectionAsync(this IBangumiClient client, string userName, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.UsersUrl}/{userName}/collections/-/persons/{personId}",
            Json.Default.UserPersonCollection, cancellationToken);
    }
}
