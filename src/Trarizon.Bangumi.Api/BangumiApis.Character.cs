using System.Collections.Immutable;
using System.ComponentModel;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Characters;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/tree/master/web/handler/character

    private const string CharactersUrl = V0Url + "/characters";

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Character>> GetCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}",
            Json.Default.Character, cancellationToken);
    }

    /// <summary>
    /// 获取角色图片
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="imageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Uri>> GetCharacterImageUrlAsync(this IBangumiClient client, uint characterId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/image?type={imageSize.ToUrlQueryString()}", cancellationToken)!;
    }

    /// <summary>
    /// 获取角色关联条目
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<ImmutableArray<CharacterRelatedSubject>>> GetCharacterRelatedSubjectAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/subjects",
            Json.Default.ImmutableArrayCharacterRelatedSubject, cancellationToken);
    }

    /// <summary>
    /// 获取角色关联人物
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<ImmutableArray<CharacterRelatedPerson>>> GetCharacterRelatedPersonAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/persons",
            Json.Default.ImmutableArrayCharacterRelatedPerson, cancellationToken);
    }

    /// <summary>
    /// 收藏角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult> CollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/collect", cancellationToken);
    }

    /// <summary>
    /// 取消收藏角色
    /// </summary>
    /// <param name="client"></param>
    /// <param name="characterId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult> UncollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/collect", cancellationToken);
    }
}
