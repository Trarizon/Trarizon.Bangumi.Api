using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class Revision : IRevision
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
    public DateTimeOffset CreatedTime { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Revision() { }
#pragma warning restore CS8618
}
