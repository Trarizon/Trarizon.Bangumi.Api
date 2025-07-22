using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Models.Episodes;
using Trarizon.Bangumi.Api.Models.Subjects;
using Trarizon.Bangumi.Collections;

namespace Trarizon.Bangumi;
public static class BangumiApisExtensions
{
    private const string ExperimentalDianosticId = "BgmExpr";

    [Experimental(ExperimentalDianosticId)]
    public static PageCollection<Subject> SearchSubjects(this IBangumiClient client, SearchSubjectsRequestBody requestBody, int? pageLimit = null, CancellationToken cancellationToken = default)
    {
        requestBody = requestBody.Clone();
        return new PageCollection<Subject>(pageLimit, cancellationToken,
            (lmt, ofs, cancellationToken) => client.SearchPagedSubjectsAsync(requestBody, lmt, ofs, cancellationToken));
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
    public static PageCollection<Subject> GetSubjects(this IBangumiClient client, SubjectType type, GetSubjectsQueries queries, int? limit = null, CancellationToken cancellationToken = default)
    {
        queries = queries.Clone();
        return new PageCollection<Subject>(limit, cancellationToken,
            (lmt, ofs, token) => client.GetPagedSubjectsAsync(type, queries, lmt, ofs, token));
    }

    public static PageCollection<Episode> GetEpisodes(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        return new PageCollection<Episode>(limit, cancellationToken,
            (lmt, ofs, token) => client.GetPagedEpisodesAsync(subjectId, episodeType, lmt, ofs, token));
    }
}
