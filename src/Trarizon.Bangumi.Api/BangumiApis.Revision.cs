using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models.Revisions;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string RevisionsUrl = V0Url + "/revisions";

    private static Task<BangumiApiResult<PagedData<Revision>>> GetPagedRevisionInternalAsync(this IBangumiClient client, string url, string idQueryName, uint id, int? limit = null, int? offset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(url);
        builder.AppendQuery(idQueryName, id);
        builder.CheckAppendQuery("limit", limit);
        builder.CheckAppendQuery("offset", offset);
        return client.GetFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            Json.Default.PagedDataRevision, cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<Revision>>> GetPagedPersonRevisionsAsync(this IBangumiClient client, uint personId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/person", "person_id", personId, pageLimit, pageOffset, cancellationToken);
    }

    public static Task<BangumiApiResult<PersonRevision>> GetPersonRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{RevisionsUrl}/persons/{revisionId}",
            Json.Default.PersonRevision, cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<Revision>>> GetPagedCharacterRevisionsAsync(this IBangumiClient client, uint characterId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/characters", "character_id", characterId, pageLimit, pageOffset, cancellationToken);
    }

    public static Task<BangumiApiResult<CharacterRevision>> GetCharacterRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{RevisionsUrl}/characters/{revisionId}",
            Json.Default.CharacterRevision, cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<Revision>>> GetPagedSubjectRevisionsAsync(this IBangumiClient client, uint subjectId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/subjects", "subject_id", subjectId, pageLimit, pageOffset, cancellationToken);
    }

    public static Task<BangumiApiResult<SubjectRevision>> GetSubjectRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{RevisionsUrl}/subjects/{revisionId}",
            Json.Default.SubjectRevision, cancellationToken);
    }

    public static Task<BangumiApiResult<PagedData<Revision>>> GetPagedEpisodeRevisionsAsync(this IBangumiClient client, uint episodeId, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        return GetPagedRevisionInternalAsync(client, $"{RevisionsUrl}/episodes", "episode_id", episodeId, pageLimit, pageOffset, cancellationToken);
    }

    public static Task<BangumiApiResult<EpisodeRevision>> GetEpisodeRevisionAsync(this IBangumiClient client, uint revisionId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{RevisionsUrl}/episodes/{revisionId}",
            Json.Default.EpisodeRevision, cancellationToken);
    }
}
