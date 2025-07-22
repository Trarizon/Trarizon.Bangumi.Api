using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models.Revisions;
[GoSource("https://github.com/bangumi/server/blob/master/web/res/revision.go#L37")]
public sealed class RevisionExtra
{
    [JsonInclude,JsonPropertyName("img")]
    public string? Image { get; internal set; }

    [JsonConstructor]
    internal RevisionExtra() { }
}
