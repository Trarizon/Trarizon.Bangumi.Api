using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Models.CharacterModels;
using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.IndexModels;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.RevisionModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Routes;
using Trarizon.Bangumi.Api.Toolkit.Collections;

namespace Trarizon.Bangumi.Api.Toolkit;
/// <summary>
/// Bangumi API的扩展实现
/// </summary>
public static class BangumiApiExtensions
{
    /// <summary>
    /// 搜索条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [Experimental(BangumiApis.ExperimentalApiDiagnosticId)]
    public static AsyncPageCollection<SearchResponsedSubject> SearchSubjects(this BangumiClient client, SearchSubjectsRequestBody? requestBody, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        requestBody = requestBody?.Clone();
        return new AsyncPageCollection<SearchResponsedSubject>(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, cancellationToken) => client.SearchPagedSubjectsAsync(requestBody, lmt, ofs, cancellationToken));
    }

    /// <summary>
    /// 获取条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="query"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<Subject> GetSubjects(this BangumiClient client, GetSubjectsQuery query, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        query = query.Clone();
        return new AsyncPageCollection<Subject>(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedSubjectsAsync(query, lmt, ofs, token));
    }

    /// <summary>
    /// 获取章节信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="episodeType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<Episode> GetEpisodes(this BangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new AsyncPageCollection<Episode>(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedEpisodesAsync(subjectId, episodeType, lmt, ofs, token));
    }

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [Experimental(BangumiApis.ExperimentalApiDiagnosticId)]
    public static AsyncPageCollection<Character> SearchCharacters(this BangumiClient client, SearchCharactersRequestBody? requestBody, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        requestBody = requestBody?.Clone();
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.SearchPagedCharactersAsync(requestBody, lmt, ofs, token));
    }

    /// <summary>
    /// 获取人物信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [Experimental(BangumiApis.ExperimentalApiDiagnosticId)]
    public static AsyncPageCollection<Person> SearchPersons(this BangumiClient client, SearchPersonsRequestBody? requestBody, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        requestBody = requestBody?.Clone();
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.SearchPagedPersonsAsync(requestBody, lmt, ofs, token));
    }

    /// <summary>
    /// 获取用户条目收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="subjectType"></param>
    /// <param name="collectionType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<UserSubjectCollection> GetUserSubjectCollections(this BangumiClient client, string userName, SubjectType? subjectType = null, SubjectCollectionType? collectionType = null, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedUserSubjectCollectionsAsync(userName, subjectType, collectionType, lmt, ofs, token));
    }

    /// <summary>
    /// 获取用户章节收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="episodeType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<UserEpisodeCollection> GetUserSubjectEpisodeCollections(this BangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedUserSubjectEpisodeCollectionsAsync(subjectId, episodeType, lmt, ofs, token));
    }

    /// <summary>
    /// 获取用户角色收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<UserCharacterCollection> GetUserCharacterCollections(this BangumiClient client, string userName, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedUserCharacterCollectionsAsync(userName, lmt, ofs, token));
    }

    /// <summary>
    /// 获取用户人物收藏
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<UserPersonCollection> GetUserPersonCollections(this BangumiClient client, string userName, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedUserPersonCollectionsAsync(userName, lmt, ofs, token));
    }

    /// <summary>
    /// 获取人物编辑记录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<PersonRevision> GetPersonRevisions(this BangumiClient client, uint personId, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedPersonRevisionsAsync(personId, lmt, ofs, token));
    }

    /// <summary>
    /// 获取角色编辑记录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<CharacterRevision> GetCharacterRevisions(this BangumiClient client, uint characterId, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedCharacterRevisionsAsync(characterId, lmt, ofs, token));
    }

    /// <summary>
    /// 获取条目编辑记录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<SubjectRevision> GetSubjectRevisions(this BangumiClient client, uint subjectId, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedSubjectRevisionsAsync(subjectId, lmt, ofs, token));
    }

    /// <summary>
    /// 获取章节编辑记录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<EpisodeRevision> GetEpisodeRevisions(this BangumiClient client, uint episodeId, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedEpisodeRevisionsAsync(episodeId, lmt, ofs, token));
    }

    /// <summary>
    /// 获取目录编辑记录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="subjectType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AsyncPageCollection<IndexSubject> GetIndexSubjects(this BangumiClient client, uint indexId, SubjectType? subjectType = null, int? pageLimit = null, AsyncPageCollectionOptions? options = null)
    {
        return new(pageLimit, options ?? AsyncPageCollectionOptions.Default,
            (lmt, ofs, token) => client.GetPagedIndexSubjectsAsync(indexId, subjectType, lmt, ofs, token));
    }
}
