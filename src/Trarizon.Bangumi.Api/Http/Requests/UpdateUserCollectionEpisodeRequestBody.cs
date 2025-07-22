using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Api.Http.Requests;
// https://github.com/bangumi/server/blob/master/web/req/collection.go#L87
public sealed class UpdateUserCollectionEpisodeRequestBody
{
    [JsonInclude, JsonPropertyName("type")]
    public required EpisodeCollectionType Type { get; set; }

    public UpdateUserCollectionEpisodeRequestBody Clone() => new()
    {
        Type = Type,
    };
}
