using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Subjects;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class SubjectRevisionData
{
    // TODO:有几个不明确的类型
    [JsonInclude,JsonPropertyName("field_eps")]
    public int EpisodeCount { get; internal set; }

    [JsonInclude, JsonPropertyName("field_infobox")]
    public string InfoBox { get; internal set; }

    [JsonInclude, JsonPropertyName("field_summary")]
    public string Summary { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    [JsonInclude, JsonPropertyName("platform")]
    public int Platform { get; internal set; }

    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("type_id")]
    public int TypeId { get; internal set; }

    [JsonInclude, JsonPropertyName("vote_field")]
    public string Vote { get; internal set; }

#pragma warning disable CS8618          
    [JsonConstructor]
    internal SubjectRevisionData() { }
#pragma warning restore CS8618          
}
