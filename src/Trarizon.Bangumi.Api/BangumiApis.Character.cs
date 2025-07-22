using System.Collections.Immutable;
using System.ComponentModel;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Characters;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string CharactersUrl = V0Url + "/characters";

    public static Task<BangumiApiResult<Character>> GetCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}",
            Json.Default.Character, cancellationToken);
    }

    public static Task<BangumiApiResult<Uri>> GetCharacterImageUrlAsync(this IBangumiClient client, uint characterId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/image?type={imageSize.ToUrlQueryString()}", cancellationToken)!;
    }

    public static Task<BangumiApiResult<ImmutableArray< CharacterRelatedSubject>>> GetCharacterRelatedSubjectAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/subjects",
            Json.Default.ImmutableArrayCharacterRelatedSubject, cancellationToken);
    }

    public static Task<BangumiApiResult<ImmutableArray<CharacterRelatedPerson>>> GetCharacterRelatedPersonAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/persons",
            Json.Default.ImmutableArrayCharacterRelatedPerson, cancellationToken);
    }

    public static Task<BangumiApiResult> CollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/collect", cancellationToken);
    }

    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult> UncollectCharacterAsync(this IBangumiClient client, uint characterId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{CharactersUrl}/{characterId}/collect", cancellationToken);
    }
}
