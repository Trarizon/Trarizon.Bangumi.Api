using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models.Collections;
/// <summary>
/// 用户的人物收藏
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/collection.go#L74">
/// PersonCollection
/// </see>.
/// </remarks>
public sealed class UserPersonCollection
{
    /// <inheritdoc cref="Person.Id"/>
    [JsonInclude, JsonPropertyName("id")]
    public uint PersonId { get; internal set; }

    /// <inheritdoc cref="Person.Name"/>
    [JsonInclude, JsonPropertyName("name")]
    public string PersonName { get; internal set; }

    /// <inheritdoc cref="Person.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public PersonType PersonType { get; internal set; }

    // Scheme存在但json不存在
    //[JsonInclude, JsonPropertyName("career")]
    //public ImmutableArray<PersonCareer> Careers { get; internal set; }

    /// <inheritdoc cref="Person.Images"/>
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <summary>
    /// 收藏创建时间
    /// </summary>
    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal UserPersonCollection() { }
#pragma warning restore CS8618
}
