using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models.Characters;
using Trarizon.Bangumi.Api.Models.Persons;
using Trarizon.Bangumi.Api.Models.Subjects;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string SearchUrl = V0Url + "/search";
    private const string SearchSubjectsUrl = SearchUrl + "/subjects";
    private const string SearchCharactersUrl = SearchUrl + "/characters";

    /// <summary>
    /// 搜索条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="pageLimit">单页最大数量，该值必须大于0，过大会被API限制在maxLimit内</param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/subject/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<SearchResponsedSubject>>> SearchPagedSubjectsAsync(this IBangumiClient client, SearchSubjectsRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchSubjectsUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody!, Json.Default.SearchSubjectsRequestBody,
            Json.Default.PagedDataSearchResponsedSubject, cancellationToken);
    }

    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/character/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<Character>>> SearchPagedCharactersAsync(this IBangumiClient client, SearchCharactersRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchCharactersUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody!, Json.Default.SearchCharactersRequestBody,
            Json.Default.PagedDataCharacter, cancellationToken);
    }

    /// <remarks>
    /// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/person/handle.go" />
    /// </remarks>
    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<Person>>> SearchPagedPersonsAsync(this IBangumiClient client, SearchPersonsRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchCharactersUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody!, Json.Default.SearchPersonsRequestBody,
            Json.Default.PagedDataPerson, cancellationToken);
    }
}
