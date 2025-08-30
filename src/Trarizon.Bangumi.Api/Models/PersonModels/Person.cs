using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
/// <summary>
/// 人物
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/person.go#L27">
/// PersonV0
/// </see>
/// </remarks>
public sealed class Person : IPerson, IPersonImagesProvider,IPersonCareersProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("type")]
    public PersonType Type { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("career")]
    public ImmutableArray<PersonCareer> Careers { get; internal set; }

    // api: 源码非指针，scheme明确nullable
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("images")]
    public PersonImageSet Images { get; internal set; }

    /// <summary>
    /// 人物简介
    /// </summary>
    [JsonInclude, JsonPropertyName("image")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 人物数据最后修改时间
    /// </summary>
    [JsonInclude, JsonPropertyName("last_modified")]
    public DateTimeOffset LastModifiedTime { get; internal set; }

    /// <summary>
    /// 人物信息列表
    /// </summary>
    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    /// <summary>
    /// 人物性别
    /// </summary>
    /// <remarks>
    /// 源码上看来只有[male, female, null]三个值 <see href="https://github.com/bangumi/server/blob/master/web/res/character.go#L44" />
    /// </remarks>
    [JsonInclude, JsonPropertyName("gender")]
    public Gender? Gender { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 人物血型
    /// </summary>
    [JsonInclude, JsonPropertyName("blood_type")]
    public BloodType? BloodType { get; internal set; }

    // src: *uint16
    /// <summary>
    /// 人物出生年份
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_year")]
    public int? BirthYear { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 人物出生月份
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_mon")]
    public int? BirthMonth { get; internal set; }

    // src: *uint8
    /// <summary>
    /// 人物出生日
    /// </summary>
    [JsonInclude, JsonPropertyName("birth_day")]
    public int? BirthDay { get; internal set; }

    /// <summary>
    /// 人物数据统计
    /// </summary>
    [JsonInclude, JsonPropertyName("stat")]
    public Statistics Statistics { get; internal set; }

    /// <summary>
    /// 人物默认图片
    /// </summary>
    [JsonInclude, JsonPropertyName("img")]
    public string ImageUrl { get; internal set; }

    /// <summary>
    /// 人物是否锁定
    /// </summary>
    [JsonInclude, JsonPropertyName("locked")]
    public bool IsLocked { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal Person() { }
#pragma warning restore CS8618 
}
