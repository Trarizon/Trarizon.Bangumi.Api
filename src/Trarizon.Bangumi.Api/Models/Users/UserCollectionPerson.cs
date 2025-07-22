using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Users;
// UserPersonCollection
// https://github.com/bangumi/server/blob/master/web/res/collection.go
public sealed class UserCollectionPerson : IPerson
{
    [JsonInclude, JsonPropertyName("id")]
    public uint PersonId { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string PersonName { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public PersonType PersonType { get; internal set; }

    // Scheme存在但json不存在
    //[JsonInclude, JsonPropertyName("career")]
    //public ImmutableArray<PersonCareer> Careers { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal UserCollectionPerson() { }
#pragma warning restore CS8618            

    uint IPerson.Id => PersonId;
}
