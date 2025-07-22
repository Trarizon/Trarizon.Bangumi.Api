using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Episodes;
using Trarizon.Bangumi.Api.Serialization;

namespace Trarizon.Bangumi.Api.Models.Users;
public sealed class UserCollectionEpisode
{
    // NOTE: API scheme页显示的没有subject_id，但实际返回结果有
    [JsonInclude, JsonPropertyName("episode")]
    public Episode Episode { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public EpisodeCollectionType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("updated_at")]
    [JsonConverter(typeof(DateTimeOffsetFromUnixTimeSecondsJsonConverter))]
    public DateTimeOffset UpdateTime { get; internal set; }

#pragma warning disable CS8618          
    [JsonConstructor]
    internal UserCollectionEpisode() { }
#pragma warning restore CS8618          
}
