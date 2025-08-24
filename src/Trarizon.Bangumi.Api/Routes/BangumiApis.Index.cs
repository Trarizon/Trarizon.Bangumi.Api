using Trarizon.Bangumi.Api.Models.IndexModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // API页有大量错误，Index参考源码
    // src: https://github.com/bangumi/server/tree/master/web/handler/index

    private const string IndicesUrl = V0Url + "/indices";

    /// <summary>
    /// 创建新目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<BangumiIndex>> CreateIndexAsync(this BangumiClient client, AddIndexRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(
            IndicesUrl,
            requestBody, Json.Default.AddIndexRequestBody,
            Json.Default.BangumiIndex, cancellationToken);
    }

    /// <summary>
    /// 获取目录信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<BangumiIndex>> GetIndexAsync(this BangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}", Json.Default.BangumiIndex, cancellationToken);
    }

    /// <summary>
    /// 更新目录信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> UpdateIndexInfoAsync(this BangumiClient client, uint indexId, UpdateIndexInfoRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}",
            requestBody, Json.Default.UpdateIndexInfoRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页目录内条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="subjectType"></param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<PagedData<IndexSubject>>> GetPagedIndexSubjectsAsync(this BangumiClient client, uint indexId, SubjectType? subjectType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{IndicesUrl}/{indexId}/subjects");
        builder.CheckAppendQuery("type", subjectType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(builder.Build(),
            Json.Default.PagedDataIndexSubject, cancellationToken);
    }

    /// <summary>
    /// 将条目添加到目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<IndexSubject>> AddSubjectToIndexAsync(this BangumiClient client, uint indexId, AddIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects",
            requestBody, Json.Default.AddIndexSubjectRequestBody,
            Json.Default.IndexSubject, cancellationToken);
    }

    /// <summary>
    /// 更新目录内条目信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="subjectId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<IndexSubject>> UpdateIndexSubjectAsync(this BangumiClient client, uint indexId, uint subjectId, UpdateIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonAndFromJsonWhenSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects/{subjectId}",
            requestBody, Json.Default.UpdateIndexSubjectRequestBody,
            Json.Default.IndexSubject, cancellationToken);
    }

    /// <summary>
    /// 将条目从目录移除
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="subjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> RemoveSubjectFromIndexAsync(this BangumiClient client, uint indexId, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/subjects/{subjectId}",
            cancellationToken);
    }

    /// <summary>
    /// 收藏目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> CollectIndexAsync(this BangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }

    /// <summary>
    /// 取消收藏目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> UncollectIndexAsync(this BangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }
}
