using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.RevisionModels;
/// <summary>
/// 人物职业编辑信息
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L23">
/// Profession
/// </see>
/// </remarks>
public sealed class PersonRevisionProfession
{
    /// <inheritdoc cref="PersonModels.PersonCareer.Writer"/>
    [JsonInclude,JsonPropertyName("writer")]
    public string? Writer { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Producer"/>
    [JsonInclude, JsonPropertyName("producer")]
    public string? Producer { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Mangaka"/>
    [JsonInclude, JsonPropertyName("mangaka")]
    public string? Mangaka { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Artist"/>
    [JsonInclude, JsonPropertyName("artist")]
    public string? Artist { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Seiyu"/>
    [JsonInclude, JsonPropertyName("seiyu")]
    public string? Seiyu { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Illustrator"/>
    [JsonInclude, JsonPropertyName("illustrator")]
    public string? Illustrator { get; internal set; }

    /// <inheritdoc cref="PersonModels.PersonCareer.Actor"/>
    [JsonInclude, JsonPropertyName("actor")]
    public string? Actor { get; internal set; }

    [JsonConstructor]
    internal PersonRevisionProfession() { }
}
