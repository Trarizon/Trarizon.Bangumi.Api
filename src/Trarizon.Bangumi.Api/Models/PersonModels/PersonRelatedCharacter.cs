using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.CharacterModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
/// <summary>
/// 人物关联角色
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L221">
/// PersonRelatedCharacter
/// </see>
/// </remarks>
public sealed class PersonRelatedCharacter : ICharacter, ICharacterImagesProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public CharacterType Type { get; internal set; }

    // api: 源码非指针，scheme 明确nullable
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <inheritdoc cref="Subject.Id"/>
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

    // api: 源码非指针，scheme nullable
    /// <inheritdoc cref="SubjectRelatedCharacter.Relation"/>
    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal PersonRelatedCharacter() { }
#pragma warning restore CS8618      
}
