using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Characters;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L263
/// <summary>
/// 条目关联角色
/// </summary>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedCharacter : ICharacter
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public CharacterType Type { get; internal set; }

    /// <summary>
    /// 角色图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet? Images { get; internal set; } // 源码非指针，scheme 明确nullabl

    /// <summary>
    /// 与条目的关系
    /// </summary>
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
