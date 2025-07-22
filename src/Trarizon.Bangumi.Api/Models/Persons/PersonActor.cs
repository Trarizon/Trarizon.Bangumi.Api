using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Persons;
// api: Person
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L282
/// <summary>
/// 演员
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class PersonActor : IPerson
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 人物名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 人物类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public PersonType Type { get; internal set; }

    /// <summary>
    /// 人物履历
    /// </summary>
    [JsonInclude, JsonPropertyName("career")]
    public ImmutableArray<PersonCareer> Careers { get; internal set; }

    // api: 源码非指针，scheme 明确nullable
    /// <summary>
    /// 人物图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet? Images { get; internal set; }

    /// <summary>
    /// 人物简介
    /// </summary>
    [JsonInclude, JsonPropertyName("short_summary")]
    public string ShortSummary { get; internal set; }

    /// <summary>
    /// 是否锁定
    /// </summary>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

#pragma warning disable CS8618 
    [JsonConstructor]
    internal PersonActor() { }
#pragma warning restore CS8618 
}
