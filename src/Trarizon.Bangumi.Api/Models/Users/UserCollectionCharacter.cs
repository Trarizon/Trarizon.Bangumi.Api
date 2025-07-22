using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Characters;
using Trarizon.Bangumi.Api.Models.Persons;

namespace Trarizon.Bangumi.Api.Models.Users;
// UserCharacterCollection
public sealed class UserCollectionCharacter
{
    [JsonInclude, JsonPropertyName("id")]
    public uint CharacterId { get; internal set; }

    [JsonInclude, JsonPropertyName("name")]
    public string CharacterName { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public CharacterType CharacterType { get; internal set; }

    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    [JsonInclude, JsonPropertyName("created_at")]
    public DateTime CreatedTime { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal UserCollectionCharacter() { }
#pragma warning restore CS8618            
}
