using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Responses.Models.Collections;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/tree/master/web/handler/user

    private const string UserCollectionsUrl = UsersUrl + "/-/collections";

    /// <summary>
    /// 获取单页用户条目收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="subjectType"></param>
    /// <param name="collectionType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<UserSubjectCollection>> GetPagedUserSubjectCollectionsAsync(this IBangumiClient client, string userName, SubjectType? subjectType = null, SubjectCollectionType? collectionType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections");
        builder.CheckAppendQuery("subject_type", subjectType?.ToQueryValue());
        builder.CheckAppendQuery("type", collectionType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(builder.Build(),
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
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{UsersUrl}/{userName}/collections/{subjectId}",
            Json.Default.UserSubjectCollection, cancellationToken);
    }

    /// <summary>
    /// 修改条目收藏信息, 如果不存在则创建，如果存在则修改
    /// <br/>
    /// 由于直接修改剧集条目的完成度可能会引起意料之外效果，只能用于修改书籍类条目的完成度。
    /// </summary>
    public static Task AddOrUpdateUserSubjectCollectionAsync(this IBangumiClient client, uint subjectId, UpdateUserSubjectCollectionRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonEnsureSuccessStatusCodeOrThrowAsync(
            $"{UserCollectionsUrl}/{subjectId}",
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
        return client.PatchAsJsonEnsureSuccessStatusCodeOrThrowAsync(
            $"{UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, Json.Default.UpdateUserSubjectCollectionRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页用户章节收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="episodeType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<UserEpisodeCollection>> GetPagedUserSubjectEpisodeCollectionsAsync(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UserCollectionsUrl}/{subjectId}/episodes");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("episode_type", episodeType?.ToQueryValue());

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
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
        return client.PatchAsJsonEnsureSuccessStatusCodeOrThrowAsync(
            $"{UserCollectionsUrl}/{subjectId}/episodes",
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
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{UserCollectionsUrl}/-/episodes/{episodeId}",
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
        return client.PutAsJsonEnsureSuccessStatusCodeOrThrowAsync(
            $"{UserCollectionsUrl}/-/episodes/{episodeId}",
            requestBody, Json.Default.UpdateUserEpisodeCollectionRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页用户角色收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<UserCharacterCollection>> GetPagedUserCharacterCollectionsAsync(this IBangumiClient client, string userName, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections/-/characters");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
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
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{UsersUrl}/{userName}/collections/-/characters/{characterId}",
            Json.Default.UserCharacterCollection, cancellationToken);
    }

    /// <summary>
    /// 获取单页用户人物收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<UserPersonCollection>> GetPagedUserPersonCollectionsAsync(this IBangumiClient client, string userName, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections/-/persons");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
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
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{UsersUrl}/{userName}/collections/-/persons/{personId}",
            Json.Default.UserPersonCollection, cancellationToken);
    }
}
