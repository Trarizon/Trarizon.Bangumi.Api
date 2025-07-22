using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Serialization;
using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// https://github.com/bangumi/server/blob/master/web/res/subject.go#L67
public sealed class SubjectSlim : ISubject
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    [JsonInclude, JsonPropertyName("short_summary")]
    public string ShortSummary { get; internal set; }// TODO: Truncated Summary ?

    [JsonInclude, JsonPropertyName("date")]
    [JsonConverter(typeof(NullableDateOnlyToStringJsonConverter))]
    [GoSource<string>]
    public DateOnly? AirDate { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }

    [JsonInclude, JsonPropertyName("volumes")]
    [GoSource<uint>]
    public int VolumeCount { get; internal set; }

    [JsonInclude, JsonPropertyName("eps")]
    [GoSource<uint>]
    public int EpisodeCount { get; internal set; }

    [JsonInclude, JsonPropertyName("collection_total")]
    [GoSource<uint>]
    public int TotalCollectedUserCount { get; internal set; }

    [JsonInclude, JsonPropertyName("score")]
    public double Score { get; internal set; }

    [JsonInclude, JsonPropertyName("rank")]
    [GoSource<uint>]
    public int Rank { get; internal set; }

    /// <summary>
    /// 前10个tag
    /// </summary>
    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<SubjectTag> Tags10 { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal SubjectSlim() { }
#pragma warning restore CS8618
}
