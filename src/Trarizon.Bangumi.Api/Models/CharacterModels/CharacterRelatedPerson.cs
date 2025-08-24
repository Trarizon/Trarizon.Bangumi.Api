using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.CharacterModels;
/// <summary>
/// 角色关联人物
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L233">
/// CharacterRelatedPerson
/// </see>
/// </remarks>
public sealed class CharacterRelatedPerson : IPerson
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="Person.Name" />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Person.Type" />
    [JsonInclude, JsonPropertyName("type")]
    public PersonType Type { get; internal set; }

    // api: 源码非指针，scheme 明确nullable
    /// <inheritdoc cref="Person.Images" />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <inheritdoc cref="ISubject.Id"/>
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    /// <inheritdoc cref="Subject.Type"/>
    [JsonInclude, JsonPropertyName("subject_type")]
    public SubjectType SubjectType { get; internal set; }

    /// <inheritdoc cref="Subject.Name"/>
    [JsonInclude, JsonPropertyName("subject_name")]
    public string SubjectName { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName"/>
    [JsonInclude, JsonPropertyName("subject_name_cn")]
    public string SubjectChineseName { get; internal set; }

    /// <inheritdoc cref="SubjectRelatedCharacter.Relation"/>
    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; } // 源码非指针，scheme nullalbe

#pragma warning disable CS8618      
    [JsonConstructor]
    internal CharacterRelatedPerson() { }
#pragma warning restore CS8618      
}
