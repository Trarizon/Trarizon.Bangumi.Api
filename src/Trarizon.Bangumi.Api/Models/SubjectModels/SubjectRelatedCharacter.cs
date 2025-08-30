using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.CharacterModels;
using Trarizon.Bangumi.Api.Models.PersonModels;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 条目关联角色
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L263">
/// SubjectRelatedCharacter
/// </see>
/// </remarks>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedCharacter : ICharacter, ICharacterImagesProvider
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

    /// <summary>
    /// 角色在条目中的身份
    /// </summary>
    /// <remarks>
    /// 文档返回string但是源码中只有4个值[主角, 配角, 客串, ""]，此处暂且保留string
    /// <br/>
    /// src: <see href="https://github.com/bangumi/server/blob/master/web/res/character.go#L49">
    /// map1
    /// </see>
    /// <see href="https://github.com/bangumi/server/blob/master/web/handler/subject/related_characters.go#L166">
    /// map2
    /// </see>
    /// </remarks>
    [JsonInclude, JsonPropertyName("relation")]
    public string Relation { get; internal set; }

    /// <summary>
    /// 演员列表
    /// </summary>
    [JsonInclude, JsonPropertyName("actors")]
    public ImmutableArray<PersonActor> Actors { get; internal set; } // 源码非指针，scheme nullable

#pragma warning disable CS8618
    [JsonConstructor]
    internal SubjectRelatedCharacter() { }
#pragma warning restore CS8618
}
