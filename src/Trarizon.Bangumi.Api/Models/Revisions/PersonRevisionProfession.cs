using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class PersonRevisionProfession
{
    [JsonInclude,JsonPropertyName("writer")]
    public string? Writer { get; internal set; }

    [JsonInclude, JsonPropertyName("producer")]
    public string? Producer { get; internal set; }

    [JsonInclude, JsonPropertyName("mangaka")]
    public string? Mangaka { get; internal set; }

    [JsonInclude, JsonPropertyName("artist")]
    public string? Artist { get; internal set; }

    [JsonInclude, JsonPropertyName("seiyu")]
    public string? Seiyu { get; internal set; }

    [JsonInclude, JsonPropertyName("illustrator")]
    public string? Illustrator { get; internal set; }

    [JsonInclude, JsonPropertyName("actor")]
    public string? Actor { get; internal set; }

    [JsonConstructor]
    internal PersonRevisionProfession() { }
}
