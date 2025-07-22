using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class UpdateUserCollectionSubjectEpisodesRequestBody
{
    [JsonInclude,JsonPropertyName("episode_id")]
    public required List<int> Episodes { get; set; }

    [JsonInclude,JsonPropertyName("type")]
    public required EpisodeCollectionType Type { get; set; }
}
