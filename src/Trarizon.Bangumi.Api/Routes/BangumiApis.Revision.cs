using System.Text.Json.Serialization.Metadata;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models.Abstractions;
using Trarizon.Bangumi.Api.Responses.Models.Revisions;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/blob/master/web/handler/revision.go
    // https://github.com/bangumi/server/blob/master/web/handler/revision_episode.go

    private const string RevisionsUrl = V0Url + "/revisions";

    private static Task<PagedData<TRevision>> GetPagedRevisionInternalAsync<TRevision>(this IBangumiClient client, string url, string idQueryName, uint id, int? limit, int? offset, JsonTypeInfo<PagedData<TRevision>> jsonTypeInfo, CancellationToken cancellationToken = default)
        where TRevision : IRevision
    {
        var builder = new QueryBuilder(url);
        builder.AppendQuery(idQueryName, id);
        builder.CheckAppendQuery("limit", limit);
        builder.CheckAppendQuery("offset", offset);
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(builder.Build(), jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    /// 获取单页人物编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<PersonRevision>> GetPagedPersonRevisionsAsync(this IBangumiClient client, uint personId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/person", "person_id", personId, pageLimit, pageOffset,
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
            $"{RevisionsUrl}/persons/{revisionId}",
            Json.Default.PersonRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页角色编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<CharacterRevision>> GetPagedCharacterRevisionsAsync(this IBangumiClient client, uint characterId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/characters", "character_id", characterId, pageLimit, pageOffset,
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
            $"{RevisionsUrl}/characters/{revisionId}",
            Json.Default.CharacterRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页条目编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<SubjectRevision>> GetPagedSubjectRevisionsAsync(this IBangumiClient client, uint subjectId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/subjects", "subject_id", subjectId, pageLimit, pageOffset,
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
            $"{RevisionsUrl}/subjects/{revisionId}",
            Json.Default.SubjectRevision, cancellationToken);
    }

    /// <summary>
    /// 获取单页章节编辑历史
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<EpisodeRevision>> GetPagedEpisodeRevisionsAsync(this IBangumiClient client, uint episodeId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/episodes", "episode_id", episodeId, pageLimit, pageOffset,
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
            $"{RevisionsUrl}/episodes/{revisionId}",
            Json.Default.EpisodeRevision, cancellationToken);
    }
}
