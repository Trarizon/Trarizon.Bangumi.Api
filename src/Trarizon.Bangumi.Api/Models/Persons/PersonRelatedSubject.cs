using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Subjects;

namespace Trarizon.Bangumi.Api.Models.Persons;
// https://github.com/bangumi/server/blob/master/web/res/subject.go#L212
public sealed class PersonRelatedSubject : ISubject
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("staff")]
    public string Staff { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    [JsonInclude, JsonPropertyName("image")]
    public string ImageUrl { get; internal set; } // 源码非指针，scheme nullable

#pragma warning disable CS8618       
    [JsonConstructor]
    internal PersonRelatedSubject() { }
#pragma warning restore CS8618          
}
