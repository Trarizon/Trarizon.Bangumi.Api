using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.CharacterModels;
using Trarizon.Bangumi.Api.Models.PersonModels;

namespace Trarizon.Bangumi.Api.Models.UserModels;
/// <summary>
/// 用户的角色收藏
/// </summary>
/// <remarks>
/// 与<see cref="UserPersonCollection"/>对应同一个类型
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/collection.go#L74">
/// PersonCollection
/// </see>.
/// </remarks>
public sealed class UserCharacterCollection
{
    /// <inheritdoc cref="Character.Id" />
    [JsonInclude, JsonPropertyName("id")]
    public uint CharacterId { get; internal set; }

    /// <inheritdoc cref="Character.Name" />
    [JsonInclude, JsonPropertyName("name")]
    public string CharacterName { get; internal set; }

    /// <inheritdoc cref="Character.Type" />
    [JsonInclude, JsonPropertyName("type")]
    public CharacterType CharacterType { get; internal set; }

    /// <inheritdoc cref="Character.Images" />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <inheritdoc cref="UserPersonCollection.CreatedTime"/>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTime CreatedTime { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal UserCharacterCollection() { }
#pragma warning restore CS8618            
}
