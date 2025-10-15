using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Payloads;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

// API页有大量错误，Index参考源码
// src: https://github.com/bangumi/server/tree/master/web/handler/index
partial class BangumiApis
{
    /// <summary>
    /// 创建新目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiIndex> CreateIndexAsync(this IBangumiClient client, AddIndexRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            ApiRoutes.IndicesUrl,
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
    public static Task<BangumiIndex> GetIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}", Json.Default.BangumiIndex, cancellationToken);
    }

    /// <summary>
    /// 更新目录信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="requestBody"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task UpdateIndexInfoAsync(this IBangumiClient client, uint indexId, UpdateIndexInfoRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}",
            requestBody, Json.Default.UpdateIndexInfoRequestBody,
            cancellationToken);
    }

    /// <summary>
    /// 获取单页目录内条目信息
    /// </summary>
    public static Task<PagedData<IndexSubject>> GetPagedIndexSubjectsAsync(this IBangumiClient client, uint indexId, SubjectType? subjectType = null, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder($"{ApiRoutes.IndicesUrl}/{indexId}/subjects");
        builder.TryAppendQuery("type", subjectType?.ToQueryValue());
        builder.AppendPagination(pagination);

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(builder.Build(),
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
    public static Task<IndexSubject> AddSubjectToIndexAsync(this IBangumiClient client, uint indexId, AddIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PostAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}/subjects",
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
    public static Task<IndexSubject> UpdateIndexSubjectAsync(this IBangumiClient client, uint indexId, uint subjectId, UpdateIndexSubjectRequestBody requestBody, CancellationToken cancellationToken = default)
    {
        return client.PutAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}/subjects/{subjectId}",
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
    public static Task RemoveSubjectFromIndexAsync(this IBangumiClient client, uint indexId, uint subjectId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}/subjects/{subjectId}",
            cancellationToken);
    }

    /// <summary>
    /// 收藏目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task CollectIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }

    /// <summary>
    /// 取消收藏目录
    /// </summary>
    /// <param name="client"></param>
    /// <param name="indexId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task UncollectIndexAsync(this IBangumiClient client, uint indexId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.IndicesUrl}/{indexId}/collect",
            cancellationToken);
    }
}
