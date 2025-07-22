using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// api: SubjectRelation
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L154
/// <summary>
/// 条目关联条目
/// </summary>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedSubject : ISubject
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }
  
    /// <summary>
    /// 条目类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }
   
    /// <summary>
    /// 条目名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }
   
    /// <summary>
    /// 条目中文名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }
   
    // api: 源码非指针，scheme nullable
    /// <summary>
    /// 条目图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }
  
    /// <summary>
    /// 与原条目的关系
    /// </summary>
    [JsonInclude, JsonPropertyName("relation")]
    public string Relation { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal SubjectRelatedSubject() { }
#pragma warning restore CS8618
}
