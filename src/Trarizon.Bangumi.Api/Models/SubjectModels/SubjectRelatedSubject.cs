using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 条目关联条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L154">
/// SubjectRelatedSubject
/// </see>
/// </remarks>
[DebuggerDisplay("[{Relation}] {Name}")]
public sealed class SubjectRelatedSubject : ISubject, ISubjectImagesProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="Subject.Type" />
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="Subject.Name" />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName" />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    // api: 源码非指针，scheme nullable
    /// <inheritdoc cref=" Subject.Images" />
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
