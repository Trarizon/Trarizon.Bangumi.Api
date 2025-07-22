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

    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<Subject>>> SearchPagedSubjectsAsync(this IBangumiClient client, SearchSubjectsRequestBody? requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchSubjectsUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody!, Json.Default.SearchSubjectsRequestBody,
            Json.Default.PagedDataSubject, cancellationToken);
    }


    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<Character>>> SearchPagedCharactersAsync(this IBangumiClient client, SearchCharactersRequestBody requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchCharactersUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody, Json.Default.SearchCharactersRequestBody,
            Json.Default.PagedDataCharacter, cancellationToken);
    }

    [Experimental(ExperimentalApiDiagnosticId)]
    public static Task<BangumiApiResult<PagedData<PersonActor>>> SearchPagedPersonsAsync(this IBangumiClient client, SearchPersonsRequestBody requestBody, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(SearchCharactersUrl);
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            requestBody, Json.Default.SearchPersonsRequestBody,
            Json.Default.PagedDataPersonActor, cancellationToken);
    }
}
