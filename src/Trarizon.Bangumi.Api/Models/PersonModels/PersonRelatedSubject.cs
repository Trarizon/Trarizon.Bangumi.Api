using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
/// <summary>
/// 人物关联条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L212">
/// PersonRelatedSubject
/// </see>
/// </remarks>
public sealed class PersonRelatedSubject : ISubject, ISubjectBasicInfo
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="Subject.Type" />
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="SubjectRelatedPerson.RelationToSubject"/>
    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; }

    /// <inheritdoc cref="Subject.Name" />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName" />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    // api: 源码非指针，scheme nullable
    /// <inheritdoc cref="SearchResponsedSubject.ImageUrl"/>
    [JsonInclude, JsonPropertyName("image")]
    public string ImageUrl { get; internal set; }

#pragma warning disable CS8618       
    [JsonConstructor]
    internal PersonRelatedSubject() { }
#pragma warning restore CS8618          
}
