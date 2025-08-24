using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
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
    public static Task<BangumiApiResult<PagedData<UserSubjectCollection>>> GetPagedUserSubjectCollectionsAsync(this BangumiClient client, string userName, SubjectType? subjectType = null, SubjectCollectionType? collectionType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections");
        builder.CheckAppendQuery("subject_type", subjectType?.ToQueryValue());
        builder.CheckAppendQuery("type", collectionType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
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
    public static Task<BangumiApiResult<UserSubjectCollection>> GetUserSubjectCollectionAsync(this BangumiClient client, string userName, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/{subjectId}",
            Json.Default.UserSubjectCollection, cancellationToken);
    }

    /// <summary>
    /// 修改条目收藏信息, 如果不存在则创建，如果存在则修改
    /// <br/>
    /// 由于直接修改剧集条目的完成度可能会引起意料之外效果，只能用于修改书籍类条目的完成度。
    /// </summary>
    public static Task<BangumiApiResult> AddOrUpdateUserSubjectCollectionAsync(this BangumiClient client, uint subjectId, UpdateUserSubjectCollectionRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, (System.Text.Json.Serialization.Metadata.JsonTypeInfo<UpdateUserSubjectCollectionRequestBody>)Json.Default.UpdateUserSubjectCollectionRequestBody,
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
    public static Task<BangumiApiResult> UpdateUserSubjectCollectionAsync(this BangumiClient client, uint subjectId, UpdateUserSubjectCollectionRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PatchAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, (System.Text.Json.Serialization.Metadata.JsonTypeInfo<UpdateUserSubjectCollectionRequestBody>)Json.Default.UpdateUserSubjectCollectionRequestBody,
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
    public static Task<BangumiApiResult<PagedData<UserEpisodeCollection>>> GetPagedUserSubjectEpisodeCollectionsAsync(this BangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UserCollectionsUrl}/{subjectId}/episodes");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("episode_type", episodeType?.ToQueryValue());

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult> UpdateUserSubjectEpisodeCollectionsAsync(this BangumiClient client, uint subjectId, UpdateUserSubjectEpisodeCollectionsRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PatchAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}/episodes",
            requestBody, (System.Text.Json.Serialization.Metadata.JsonTypeInfo<UpdateUserSubjectEpisodeCollectionsRequestBody>)Json.Default.UpdateUserSubjectEpisodeCollectionsRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取用户章节收藏信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<UserEpisodeCollection>> GetUserCollectionEpisodeAsync(this BangumiClient client, uint episodeId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult> UpdateUserEpisodeCollectionAsync(this BangumiClient client, uint episodeId, UpdateUserEpisodeCollectionRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/-/episodes/{episodeId}",
            requestBody, (System.Text.Json.Serialization.Metadata.JsonTypeInfo<UpdateUserEpisodeCollectionRequestBody>)Json.Default.UpdateUserEpisodeCollectionRequestBody,
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
    public static Task<BangumiApiResult<PagedData<UserCharacterCollection>>> GetPagedUserCharacterCollectionsAsync(this BangumiClient client, string userName, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections/-/characters");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<UserCharacterCollection>> GetUserCharacterCollectionAsync(this BangumiClient client, string userName, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<PagedData<UserPersonCollection>>> GetPagedUserPersonCollectionsAsync(this BangumiClient client, string userName, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections/-/persons");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<UserPersonCollection>> GetUserPersonCollectionAsync(this BangumiClient client, string userName, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/-/persons/{personId}",
            Json.Default.UserPersonCollection, cancellationToken);
    }
}
