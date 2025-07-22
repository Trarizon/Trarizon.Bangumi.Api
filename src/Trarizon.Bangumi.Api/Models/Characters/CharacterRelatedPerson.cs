using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Persons;
using Trarizon.Bangumi.Api.Models.Subjects;

namespace Trarizon.Bangumi.Api.Models.Characters;
// CharacterPerson
// https://github.com/bangumi/server/blob/master/web/res/subject.go#L233
public sealed class CharacterRelatedPerson : IPerson
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public CharacterType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet? Images { get; internal set; } // 源码非指针，scheme 明确nullable

    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    [JsonInclude, JsonPropertyName("subject_type")]
    public SubjectType SubjectType { get; internal set; }

    [JsonInclude, JsonPropertyName("subject_name")]
    public string SubjectName { get; internal set; }

    [JsonInclude, JsonPropertyName("subject_name_cn")]
    public string SubjectChineseName { get; internal set; }

    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; } // 源码非指针，scheme nullalbe

#pragma warning disable CS8618      
    [JsonConstructor]
    internal CharacterRelatedPerson() { }
#pragma warning restore CS8618      
}
