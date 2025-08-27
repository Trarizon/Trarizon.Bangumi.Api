using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.PersonModels;

namespace Trarizon.Bangumi.Api.Models.CharacterModels;
/// <summary>
/// 角色
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/character.go#L25">
/// CharacterV0
/// </see>
/// </remarks>
public sealed class Character : ICharacter
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public CharacterType Type { get; internal set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 是否NSFW
    /// </summary>
    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

    // api: 源码非指针，scheme明确nullabla
    /// <summary>
    /// 角色图片
    /// </summary>
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <summary>
    /// 角色简介
    /// </summary>
    [JsonInclude, JsonPropertyName("summary")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 是否锁定
    /// </summary>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

    /// <summary>
    /// 角色信息列表
    /// </summary>
    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    /// <summary>
    /// 性别
    /// </summary>
    [JsonInclude, JsonPropertyName("gender")]
    public Gender? Gender { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 角色血型
    /// </summary>
    [JsonInclude, JsonPropertyName("blood_type")]
    public BloodType? BloodType { get; internal set; }

    // src: *uint16
    /// <summary>
    /// 角色出生年份
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_year")]
    public int? BirthYear { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 角色出生月份
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_mon")]
    public int? BirthMonth { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 角色出生日
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_day")]
    public int? BirthDay { get; internal set; }

    /// <summary>
    /// 角色数据统计
    /// </summary>
    [JsonInclude, JsonPropertyName("stat")]
    public Statistics Statistics { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618
    internal Character() { }
#pragma warning restore CS8618
}
