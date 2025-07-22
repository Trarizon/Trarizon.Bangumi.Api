using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// RelatedPerson
// https://github.com/bangumi/server/blob/master/web/res/subject.go#L272
/// <summary>
/// 条目关联人物
/// </summary>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedPerson : IPerson
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
   
    /// <summary>
    /// 人物图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet? Images { get; internal set; } // 源码非指针，scheme 明确nullable
   
    /// <summary>
    /// 与条目的关系
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
