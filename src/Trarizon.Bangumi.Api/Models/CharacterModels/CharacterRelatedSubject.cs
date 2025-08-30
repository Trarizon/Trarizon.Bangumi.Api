using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.CharacterModels;
/// <summary>
/// 角色关联条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L245">
/// CharacterRelatedSubject
/// </see>
/// </remarks>
public sealed class CharacterRelatedSubject : ISubject, ISubjectImageUrlProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="SubjectRelatedCharacter.Relation"/>
    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("image")]
    public string ImageUrl { get; internal set; } // 源码非指针，scheme nullable

#pragma warning disable CS8618       
    [JsonConstructor]
    internal CharacterRelatedSubject() { }
#pragma warning restore CS8618          
}
