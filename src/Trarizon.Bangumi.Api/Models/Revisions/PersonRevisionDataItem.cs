using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class PersonRevisionDataItem
{
    [JsonInclude, JsonPropertyName("prsn_infobox")]
    public string PersonInfoBox { get; internal set; }

    [JsonInclude, JsonPropertyName("prsn_summary")]
    public string PersonSummary { get; internal set; }

    [JsonInclude, JsonPropertyName("profession")]
    public PersonRevisionProfession RevisionProfession { get; internal set; }

    [JsonInclude, JsonPropertyName("extra")]
    public RevisionExtra RevisionExtra { get; internal set; }

    [JsonInclude, JsonPropertyName("prsnname")]
    public string PersonName { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618           
    internal PersonRevisionDataItem() { }
#pragma warning restore CS8618           
}
