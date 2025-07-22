using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Indices;
using Trarizon.Bangumi.Api.Models.Subjects;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string IndicesUrl = V0Url + "/indices";

    [Experimental(ExperimentalApiDiagnosticId)]
    [Obsolete("Not implemented?")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult<BangumiIndex>> CreateIndexAsync(this IBangumiClient client, UpdateIndexInfoRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(
            IndicesUrl,
            requestBody, Json.Default.UpdateIndexInfoRequestBody,
            Json.Default.BangumiIndex, cancellationToken);
    }

    public static Task<BangumiApiResult<BangumiIndex>> GetIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}", Json.Default.BangumiIndex, cancellationToken);
    }

    public static Task<BangumiApiResult<BangumiIndex>> UpdateIndexInfoAsync(this IBangumiClient client, uint indexId, UpdateIndexInfoRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonAndFromJsonWhenSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}",
            requestBody, Json.Default.UpdateIndexInfoRequestBody,
            Json.Default.BangumiIndex, cancellationToken);
    }

    [Experimental(ExperimentalApiDiagnosticId)]
    [Obsolete("Not implemented?")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult<PagedData<Subject>>> GetPagedIndexSubjectsAsync(this IBangumiClient client, uint indexId, SubjectType? subjectType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{IndicesUrl}/{indexId}/subjects");
        builder.CheckAppendQuery("type", subjectType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            Json.Default.PagedDataSubject, cancellationToken);
    }

    public static Task<BangumiApiResult> AddSubjectToIndexAsync(this IBangumiClient client, uint indexId, AddIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects",
            requestBody, Json.Default.AddIndexSubjectRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult> UpdateIndexSubjectAsync(this IBangumiClient client, uint indexId, uint subjectId, UpdateIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects/{subjectId}",
            requestBody, Json.Default.UpdateIndexSubjectRequestBody,
            cancellationToken);
    }

    public static Task<BangumiApiResult> RemoveSubjectFromIndexAsync(this IBangumiClient client, uint indexId, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects/{subjectId}",
            cancellationToken);
    }

    public static Task<BangumiApiResult> CollectIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }

    public static Task<BangumiApiResult> UncollectIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }
}
