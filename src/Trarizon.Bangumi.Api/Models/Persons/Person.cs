using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Persons;
// PersonDetail
// https://github.com/bangumi/server/blob/master/web/res/person.go#L27
public sealed class Person : IPerson
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public PersonType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("career")]
    public ImmutableArray<PersonCareer> Careers { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet? Images { get; internal set; }

    [JsonInclude, JsonPropertyName("image")]
    public string Summary { get; internal set; }

    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

    [JsonInclude, JsonPropertyName("last_modified")]
    public DateTimeOffset LastModifiedTime { get; internal set; }

    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    [JsonInclude, JsonPropertyName("gender")]
    public string? Gender { get; internal set; }

    [JsonInclude, JsonPropertyName("blood_type")]
    [GoSource<byte?>]
    public BloodType? BloodType { get; internal set; }

    [JsonInclude, JsonPropertyName("birth_year")]
    [GoSource<ushort?>]
    public int? BirthYear { get; internal set; }

    [JsonInclude, JsonPropertyName("birth_mon")]
    [GoSource<byte?>]
    public int? BirthMonth { get; internal set; }

    [JsonInclude, JsonPropertyName("birth_day")]
    [GoSource<byte?>]
    public int? BirthDay { get; internal set; }

    [JsonInclude, JsonPropertyName("stat")]
    public Stat Stat { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Person() { }
#pragma warning restore CS8618 
}
