using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Utilities;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

// src: https://github.com/bangumi/server/blob/master/web/handler/episode.go
partial class BangumiApis
{
    /// <summary>
    /// 获取单页条目章节信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="episodeType">章节类型</param>
    /// <param name="pagination"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PagedData<Episode>> GetPagedEpisodesAsync(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, Pagination pagination = default, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(ApiRoutes.EpisodesUrl);
        builder.AppendQuery("subject_id", subjectId);
        builder.TryAppendQuery("type", episodeType?.ToQueryValue());
        builder.AppendPagination(pagination);

        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            builder.Build(),
            Json.Default.PagedDataEpisode,
            cancellationToken);
    }

    /// <summary>
    /// 获取章节信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="episodeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Episode> GetEpisodeAsync(this IBangumiClient client, uint episodeId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            $"{ApiRoutes.EpisodesUrl}/{episodeId}",
            Json.Default.Episode, cancellationToken);
    }
}
