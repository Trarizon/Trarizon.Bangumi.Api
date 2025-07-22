using Trarizon.Bangumi.Api.Http;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models.Episodes;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string EpisodesUrl = V0Url + "/episodes";

    /// <summary>
    /// 获取单页条目章节信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="subjectId"></param>
    /// <param name="episodeType">章节类型</param>
    /// <param name="pageLimit"></param>
    /// <param name="pageOffset"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<PagedData<Episode>>> GetPagedEpisodesAsync(this IBangumiClient client, uint subjectId, EpisodeType? episodeType = null, int? pageLimit = null, int? pageOffset = null, CancellationToken cancellationToken = default)
    {
        var builder = new QueryBuilder(EpisodesUrl);
        builder.AppendQuery("subject_id", subjectId);
        builder.CheckAppendQuery("type", episodeType?.ToQueryValue());
        builder.CheckAppendQuery("limit", pageLimit);
        builder.CheckAppendQuery("offset", pageOffset);

        return client.GetFromJsonWhenSuccessStatusCodeAsync(
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
    public static Task<BangumiApiResult<Episode>> GetEpisodeAsync(this IBangumiClient client, uint episodeId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{EpisodesUrl}/{episodeId}",
            Json.Default.Episode, cancellationToken);
    }
}
