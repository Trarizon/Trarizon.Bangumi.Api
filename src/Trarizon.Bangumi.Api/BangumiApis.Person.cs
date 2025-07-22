using System.Collections.Immutable;
using System.ComponentModel;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Persons;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string PersonsUrl = V0Url + "/persons";

    public static Task<BangumiApiResult<Person>> GetPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync($"{PersonsUrl}/{personId}",
            Json.Default.Person, cancellationToken);
    }

    public static Task<BangumiApiResult<Uri>> GetPersonImageUrlAsync(this IBangumiClient client, uint personId, PersonImageSize imageSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/image?type={imageSize.ToUrlQueryString()}", cancellationToken)!;
    }

    public static Task<BangumiApiResult<ImmutableArray<PersonRelatedSubject>>> GetPersonRelatedSubjectsAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/subjects",
            Json.Default.ImmutableArrayPersonRelatedSubject, cancellationToken);
    }

    public static Task<BangumiApiResult<ImmutableArray<PersonRelatedCharacter>>> GetPersonRelatedCharactersAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/characters",
            Json.Default.ImmutableArrayPersonRelatedCharacter, cancellationToken);
    }

    public static Task<BangumiApiResult> CollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.PostEnsureSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
    }

    [Obsolete("Hide as route not implemented yet")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task<BangumiApiResult> UncollectPersonAsync(this IBangumiClient client, uint personId, CancellationToken cancellationToken = default)
    {
        return client.DeleteEnsureSuccessStatusCodeAsync(
            $"{PersonsUrl}/{personId}/collect", cancellationToken);
    }
}
