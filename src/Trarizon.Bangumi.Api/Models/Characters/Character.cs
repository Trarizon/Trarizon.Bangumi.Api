using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Characters;
// "https://github.com/bangumi/server/blob/master/web/res/character.go#L25
public sealed class Character : ICharacter
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public CharacterType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    // https://github.com/bangumi/server/blob/master/web/res/character.go#L44
    // [1: male, 2: female]
    [JsonInclude, JsonPropertyName("gender")]
    public string? Gender { get; internal set; }

    // byte
    [JsonInclude, JsonPropertyName("blood_type")]
    public BloodType? BloodType { get; internal set; }

    // ushort
    [JsonInclude, JsonPropertyName("birth_year")]
    [GoSource<ushort?>]
    public int? BirthYear { get; internal set; }

    // byte
    [JsonInclude, JsonPropertyName("birth_mon")]
    [GoSource<byte>]
    public int? BirthMonth { get; internal set; }

    // byte
    [JsonInclude, JsonPropertyName("birth_day")]
    [GoSource<byte>]
    public int? BirthDay { get; internal set; }

    [JsonInclude, JsonPropertyName("stat")]
    public Stat Stat { get; internal set; }

    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618
    internal Character() { }
#pragma warning restore CS8618
}
