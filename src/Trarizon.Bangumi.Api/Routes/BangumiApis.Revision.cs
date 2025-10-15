using System.Text.Json.Serialization.Metadata;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models.Abstractions;
using Trarizon.Bangumi.Api.Responses.Models.Revisions;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

// src: https://github.com/bangumi/server/blob/master/web/handler/revision.go
// https://github.com/bangumi/server/blob/master/web/handler/revision_episode.go
partial class BangumiApis
{
    private static Task<PagedData<TRevision>> GetPagedRevisionInternalAsync<TRevision>(this IBangumiClient client, string url, string idQueryName, uint id, Pagination pagination, JsonTypeInfo<PagedData<TRevision>> jsonTypeInfo, CancellationToken cancellationToken = default)
        where TRevision : IRevision
    {
        var builder = new QueryBuilder(url);
        builder.AppendQuery(idQueryName, id);
        builder.AppendPagination(pagination);
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(builder.Build(), jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    /// 获取单页人物编辑历史
    /// </summary>
    public static Task<PagedData<PersonRevision>> GetPagedPersonRevisionsAsync(this IBangumiClient client, uint personId, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{ApiRoutes.RevisionsUrl}/person", "person_id", personId, pagination,
            Json.Default.PagedDataPersonRevision, cancellationToken);
    }

    /// <summary>
    /// 获取人物编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="revisionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PersonRevision> GetPersonRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.RevisionsUrl}/persons/{revisionId}",
            Json.Default.PersonRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页角色编辑历史
    /// </summary>
    public static Task<PagedData<CharacterRevision>> GetPagedCharacterRevisionsAsync(this IBangumiClient client, uint characterId, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{ApiRoutes.RevisionsUrl}/characters", "character_id", characterId, pagination,
            Json.Default.PagedDataCharacterRevision, cancellationToken);
    }

    /// <summary>
    /// 获取角色编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="revisionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<CharacterRevision> GetCharacterRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.RevisionsUrl}/characters/{revisionId}",
            Json.Default.CharacterRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页条目编辑历史
    /// </summary>
    public static Task<PagedData<SubjectRevision>> GetPagedSubjectRevisionsAsync(this IBangumiClient client, uint subjectId, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{ApiRoutes.RevisionsUrl}/subjects", "subject_id", subjectId, pagination,
            Json.Default.PagedDataSubjectRevision, cancellationToken);
    }

    /// <summary>
    /// 获取条目编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="revisionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<SubjectRevision> GetSubjectRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.RevisionsUrl}/subjects/{revisionId}",
            Json.Default.SubjectRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页章节编辑历史
    /// </summary>
    public static Task<PagedData<EpisodeRevision>> GetPagedEpisodeRevisionsAsync(this IBangumiClient client, uint episodeId, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{ApiRoutes.RevisionsUrl}/episodes", "episode_id", episodeId, pagination,
            Json.Default.PagedDataEpisodeRevision, cancellationToken);
    }

    /// <summary>
    /// 获取章节编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="revisionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<EpisodeRevision> GetEpisodeRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.RevisionsUrl}/episodes/{revisionId}",
            Json.Default.EpisodeRevision, cancellationToken);
    }
}
