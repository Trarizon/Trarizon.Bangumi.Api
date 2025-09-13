using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Responses.Models;
/// <summary>
/// 人物关联条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L212">
/// PersonRelatedSubject
/// </see>
/// </remarks>
public sealed class PersonRelatedSubject : ISubject, ISubjectImageUrlProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    /// <inheritdoc cref="SubjectRelatedPerson.RelationToSubject"/>
    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    // api: 源码非指针，scheme nullable
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("image")]
    public string ImageUrl { get; internal set; }

#pragma warning disable CS8618       
    [JsonConstructor]
    internal PersonRelatedSubject() { }
#pragma warning restore CS8618          
}
