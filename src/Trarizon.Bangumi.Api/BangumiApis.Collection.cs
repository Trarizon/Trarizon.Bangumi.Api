using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models.Episodes;
using Trarizon.Bangumi.Api.Models.Subjects;
using Trarizon.Bangumi.Api.Models.Users;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string UserCollectionsUrl = UsersUrl + "/-/collections";

    public static Task<BangumiApiResult<PagedData<UserCollectionSubject>>> GetPagedUserCollectionSubjectsAsync(this IBangumiClient client, string userName, SubjectType? subjectType = null, CollectionType? collectionType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UsersUrl}/{userName}/collections");
        builder.CheckAppendQuery("subject_type", subjectType?.ToQueryValue());
        builder.CheckAppendQuery("type", collectionType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            Json.Default.PagedDataUserCollectionSubject, cancellationToken);
    }

    public static Task<BangumiApiResult<UserCollectionSubject>> GetUserCollectionSubjectAsync(this IBangumiClient client, string userName, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/{subjectId}",
            Json.Default.UserCollectionSubject, cancellationToken);
    }

    /// <summary>
    /// 修改条目收藏状态, 如果不存在则创建，如果存在则修改
    /// <br/>
    /// 由于直接修改剧集条目的完成度可能会引起意料之外效果，只能用于修改书籍类条目的完成度。
    /// </summary>
    public static Task<BangumiApiResult> AddOrUpdateUserCollectionSubjectAsync(this IBangumiClient client, uint subjectId, UpdateUserCollectionSubjectRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, Json.Default.UpdateUserCollectionSubjectRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult> UpdateUserCollectionSubjectAsync(this IBangumiClient client, uint subjectId, UpdateUserCollectionSubjectRequestBody? requestBody, CancellationToken cancellationToken = default)
    {
        return client.PatchAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}",
            // Allow null
            requestBody!, Json.Default.UpdateUserCollectionSubjectRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<UserCollectionEpisode>>> GetPagedUserCollectionSubjectEpisodesAsync(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{UserCollectionsUrl}/{subjectId}/episodes");
        builder.CheckAppendQuery("offset", pageOffset);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("episode_type", episodeType?.ToQueryValue());

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            builder.Build(),
            Json.Default.PagedDataUserCollectionEpisode,
            cancellationToken);
    }

    public static Task<BangumiApiResult> UpdateUserCollectionSubjectEpisodesAsync(this IBangumiClient client,uint subjectId,UpdateUserCollectionSubjectEpisodesRequestBody requestBody,CancellationToken cancellationToken = default)
    {
        return client.PatchAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/{subjectId}/episodes",
            requestBody, Json.Default.UpdateUserCollectionSubjectEpisodesRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult<UserCollectionEpisode>> GetUserCollectionEpisodeAsync(this IBangumiClient client, uint episodeId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/-/episodes/{episodeId}",
            Json.Default.UserCollectionEpisode, cancellationToken);
    }

    public static Task<BangumiApiResult> UpdateUserCollectionEpisodeAsync(this IBangumiClient client, uint episodeId, UpdateUserCollectionEpisodeRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonEnsureSuccessStatusCodeAsync(
            $"{UserCollectionsUrl}/-/episodes/{episodeId}",
            requestBody, Json.Default.UpdateUserCollectionEpisodeRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<UserCollectionCharacter>>> GetPagedUserCollectionCharactersAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/-/characters",
            Json.Default.PagedDataUserCollectionCharacter, cancellationToken);
    }

    public static Task<BangumiApiResult<UserCollectionCharacter>> GetUserCollectionCharacterAsync(this IBangumiClient client, string userName, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/-/characters/{characterId}",
            Json.Default.UserCollectionCharacter, cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<UserCollectionPerson>>> GetPagedUserCollectionPersonsAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/-/persons",
            Json.Default.PagedDataUserCollectionPerson, cancellationToken);
    }

    public static Task<BangumiApiResult<UserCollectionPerson>> GetUserCollectionPersonAsync(this IBangumiClient client, string userName, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/collections/-/persons/{personId}",
            Json.Default.UserCollectionPerson, cancellationToken);
    }
}
