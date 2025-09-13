using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Responses.Models;
/// <summary>
/// 演员
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L282">
/// Actor
/// </see>
/// </remarks>
[DebuggerDisplay("{Name}")]
public sealed class PersonActor : IPerson, IPersonImagesProvider, IPersonCareersProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public PersonType Type { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("career")]
    public ImmutableArray<PersonCareer> Careers { get; internal set; }

    // api: 源码非指针，scheme 明确nullable
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <inheritdoc cref="Person.Summary"/>
    [JsonInclude, JsonPropertyName("short_summary")]
    public string ShortSummary { get; internal set; }

    /// <inheritdoc cref="Person.IsLocked"/>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

#pragma warning disable CS8618 
    [JsonConstructor]
    internal PersonActor() { }
#pragma warning restore CS8618 
}
