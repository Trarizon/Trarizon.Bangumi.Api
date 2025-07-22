using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Revisions;
// https://github.com/bangumi/server/blob/master/web/res/revision.go#L92
public sealed class EpisodeRevision : IRevision
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public RevisionType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; set; }

    [JsonInclude, JsonPropertyName("data")]
    public IReadOnlyDictionary<string, EpisodeRevisionDataItem>? Data { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618    
    internal EpisodeRevision() { }
#pragma warning restore CS8618    
}
