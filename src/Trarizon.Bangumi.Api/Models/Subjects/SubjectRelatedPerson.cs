using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Subjects;
/// <summary>
/// 条目关联人物
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L272">
/// SubjectRelatedPerson
/// </see>
/// </remarks>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedPerson : IPerson
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

    /// <inheritdoc cref="Person.Careers" />
    [JsonInclude, JsonPropertyName("career")]
    public ImmutableArray<PersonCareer> Careers { get; internal set; }

    /// <inheritdoc cref="Person.Images" />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; } // 源码非指针，scheme 明确nullable

    /// <summary>
    /// 人物在条目中的职位
    /// </summary>
    [JsonInclude, JsonPropertyName("relation")]
    public string RelationToSubject { get; internal set; }

    /// <summary>
    /// 参与章节/曲目
    /// </summary>
    [JsonInclude, JsonPropertyName("eps")]
    public string Episodes { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal SubjectRelatedPerson() { }
#pragma warning restore CS8618 
}
