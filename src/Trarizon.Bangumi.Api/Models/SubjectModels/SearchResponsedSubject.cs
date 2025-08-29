using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/subject/handle.go#L46">
/// ResponseSubject
/// </see>
/// </remarks>
public sealed class SearchResponsedSubject : ISubject, ISubjectBasicInfo
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="Subject.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="Subject.Name"/>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName"/>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <inheritdoc cref="Subject.Summary"/>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <inheritdoc cref="Subject.IsSeries"/>
    [JsonInclude, JsonPropertyName("series")]
    public bool IsSeries { get; internal set; }

    /// <inheritdoc cref="Subject.IsLocked"/>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

    /// <inheritdoc cref="Subject.IsNsfw"/>
    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

    // src: string
    /// <inheritdoc cref="Subject.Date"/>
    [JsonInclude, JsonPropertyName("date")]
    public string? Date { get; internal set; }

    /// <inheritdoc cref="Subject.Platform"/>
    [JsonInclude, JsonPropertyName("platform")]
    public string? Platform { get; internal set; }

    /// <inheritdoc cref="Subject.Images"/>
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }

    /// <summary>
    /// 条目默认图片
    /// </summary>
    [JsonInclude, JsonPropertyName("image")]
    public string ImageUrl { get; internal set; }

    // api: 源码非指针，schceme nullable
    /// <inheritdoc cref="Subject.InfoBox"/>
    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    // src: uint32
    /// <inheritdoc cref="Subject.VolumeCount"/>
    [JsonInclude, JsonPropertyName("volumes")]
    public int VolumeCount { get; internal set; }

    // src: uint32
    /// <inheritdoc cref="Subject.EpisodeCount"/>
    [JsonInclude, JsonPropertyName("eps")]
    public int EpisodeCount { get; internal set; }

    /// <inheritdoc cref="Subject.Rating"/>
    [JsonInclude, JsonPropertyName("rating")]
    public RatingInfo Rating { get; internal set; }

    /// <inheritdoc cref="Subject.Collection"/>
    [JsonInclude, JsonPropertyName("collection")]
    public SubjectCollectionStatistics Collection { get; internal set; }

    /// <inheritdoc cref="Subject.MetaTags"/>
    [JsonInclude, JsonPropertyName("meta_tags")]
    public ImmutableArray<string> MetaTags { get; internal set; }

    /// <inheritdoc cref="Subject.Tags"/>
    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<SubjectTag> Tags { get; internal set; }

#pragma warning disable CS8618             
    [JsonConstructor]
    internal SearchResponsedSubject() { }
#pragma warning restore CS8618             
}
