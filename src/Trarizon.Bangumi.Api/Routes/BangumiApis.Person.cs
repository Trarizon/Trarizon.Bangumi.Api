using System.Collections.Immutable;
using System.ComponentModel;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Responses;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/tree/master/web/handler/person

    private const string PersonsUrl = V0Url + "/persons";

    /// <summary>
    /// 获取人物信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Person>> GetPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync($"{PersonsUrl}/{personId}",
            Json.Default.Person, cancellationToken);
    }

    /// <summary>
    /// 获取人物图片
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="imageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Uri>> GetPersonImageUrlAsync(this IBangumiClient client, uint personId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/image?type={imageSize.ToUrlQueryString()}", cancellationToken)!;
    }

    /// <summary>
    /// 获取人物关联条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<ImmutableArray<PersonRelatedSubject>>> GetPersonRelatedSubjectsAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/subjects",
            Json.Default.ImmutableArrayPersonRelatedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取人物关联角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<ImmutableArray<PersonRelatedCharacter>>> GetPersonRelatedCharactersAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/characters",
            Json.Default.ImmutableArrayPersonRelatedCharacter, cancellationToken);
    }

    /// <summary>
    /// 收藏人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> CollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
    }

    /// <summary>
    /// 取消收藏人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult> UncollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
    }
}
