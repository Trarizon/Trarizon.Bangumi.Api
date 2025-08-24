using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Models.CharacterModels;
using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.IndexModels;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.RevisionModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Routes;
using Trarizon.Bangumi.Collections;

namespace Trarizon.Bangumi;
public static class BangumiApisExtensions
{
    private const string ExperimentalDianosticId = "BgmExpr";

    [Experimental(ExperimentalDianosticId)]
    public static PageCollection<SearchResponsedSubject> SearchSubjects(this BangumiClient client, SearchSubjectsRequestBody? requestBody, int? pageLimit = null)
    {
        requestBody = requestBody?.Clone();
        return new PageCollection<SearchResponsedSubject>(pageLimit, (lmt, ofs, cancellationToken) => client.SearchPagedSubjectsAsync(requestBody, lmt, ofs, cancellationToken));
    }

    /// <summary>
    /// 获取条目集合
    /// </summary>
    /// <param name="client"></param>
    /// <param name="type">条目类型</param>
    /// <param name="queries">详细条目筛选选项</param>
    /// <param name="limit">每次迭代的单页最大数量</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static PageCollection<Subject> GetSubjects(this BangumiClient client, GetSubjectsQuery queries, int? limit = null)
    {
        queries = queries.Clone();
        return new PageCollection<Subject>(limit, (lmt, ofs, token) => client.GetPagedSubjectsAsync(queries, lmt, ofs, token));
    }

    public static PageCollection<Episode> GetEpisodes(this BangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? limit = null)
    {
        return new PageCollection<Episode>(limit, (lmt, ofs, token) => client.GetPagedEpisodesAsync(subjectId, episodeType, lmt, ofs, token));
    }

    [Experimental(ExperimentalDianosticId)]
    public static PageCollection<Character> SearchCharacters(this BangumiClient client, SearchCharactersRequestBody? requestBody, int? pageLimit = null)
    {
        requestBody = requestBody?.Clone();
        return new(pageLimit, (lmt, ofs, token) => client.SearchPagedCharactersAsync(requestBody, lmt, ofs, token));
    }

    [Experimental(ExperimentalDianosticId)]
    public static PageCollection<Person> SearchPersons(this BangumiClient client, SearchPersonsRequestBody? requestBody, int? pageLimit = null)
    {
        requestBody = requestBody?.Clone();
        return new(pageLimit, (lmt, ofs, token) => client.SearchPagedPersonsAsync(requestBody, lmt, ofs, token));
    }

    public static PageCollection<UserSubjectCollection> GetUserSubjectCollections(this BangumiClient client, string userName, SubjectType? subjectType = null, SubjectCollectionType? collectionType = null, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedUserSubjectCollectionsAsync(userName, subjectType, collectionType, lmt, ofs, token));
    }

    public static PageCollection<UserEpisodeCollection> GetUserSubjectEpisodeCollections(this BangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedUserSubjectEpisodeCollectionsAsync(subjectId, episodeType, lmt, ofs, token));
    }

    public static PageCollection<UserCharacterCollection> GetUserCharacterCollections(this BangumiClient client, string userName, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedUserCharacterCollectionsAsync(userName, lmt, ofs, token));
    }

    public static PageCollection<UserPersonCollection> GetUserPersonCollections(this BangumiClient client, string userName, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedUserPersonCollectionsAsync(userName, lmt, ofs, token));
    }

    public static PageCollection<PersonRevision> GetPersonRevisions(this BangumiClient client, uint personId, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedPersonRevisionsAsync(personId, lmt, ofs, token));
    }

    public static PageCollection<CharacterRevision> GetCharacterRevisions(this BangumiClient client, uint characterId, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedCharacterRevisionsAsync(characterId, lmt, ofs, token));
    }

    public static PageCollection<SubjectRevision> GetSubjectRevisions(this BangumiClient client, uint subjectId, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedSubjectRevisionsAsync(subjectId, lmt, ofs, token));
    }

    public static PageCollection<EpisodeRevision> GetEpisodeRevisions(this BangumiClient client, uint episodeId, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedEpisodeRevisionsAsync(episodeId, lmt, ofs, token));
    }

    public static PageCollection<IndexSubject> GetIndexSubjects(this BangumiClient client, uint indexId, SubjectType? subjectType = null, int? pageLimit = null)
    {
        return new(pageLimit, (lmt, ofs, token) => client.GetPagedIndexSubjectsAsync(indexId, subjectType, lmt, ofs, token));
    }
}
