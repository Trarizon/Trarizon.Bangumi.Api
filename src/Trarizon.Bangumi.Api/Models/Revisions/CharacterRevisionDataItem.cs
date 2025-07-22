using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class CharacterRevisionDataItem
{
    [JsonInclude, JsonPropertyName("infobox")]
    public string CharacterInfoBox { get; internal set; }

    [JsonInclude, JsonPropertyName("summary")]
    public string CharacterSummary { get; internal set; }

    [JsonInclude, JsonPropertyName("prsnname")]
    public string CharacterName { get; internal set; }

    [JsonInclude, JsonPropertyName("extra")]
    public RevisionExtra RevisionExtra { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618           
    internal CharacterRevisionDataItem() { }
#pragma warning restore CS8618           
}
