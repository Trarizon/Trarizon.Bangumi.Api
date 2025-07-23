using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
// https://github.com/bangumi/server/blob/master/internal/model/revision.go#L100
/// <summary>
/// 章节编辑数据
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L101"> 
/// EpisodeRevisionDataItem
/// </see>
/// 实际实现<see href="https://github.com/bangumi/server/blob/master/internal/model/revision.go#L100">
/// EpisodeRevisionDataItem
/// </see>
/// </remarks>
public sealed class EpisodeRevisionDataItem
{
    // src: 源码内部数据全是string

    /// <summary>
    /// 章节放送日期
    /// </summary>
    [JsonInclude, JsonPropertyName("airdate")]
    public string AirDate { get; internal set; }

    /// <summary>
    /// 章节简介
    /// </summary>
    [JsonInclude, JsonPropertyName("desc")]
    public string Description { get; internal set; }

    /// <summary>
    /// 章节时长
    /// </summary>
    [JsonInclude, JsonPropertyName("duration")]
    public string Duration { get; internal set; }

    /// <summary>
    /// 章节名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 章节中文名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <summary>
    /// 章节排序
    /// </summary>
    [JsonInclude, JsonPropertyName("sort")]
    public string Sort { get; internal set; }

    /// <summary>
    /// 章节类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public string Type { get; internal set; }

#pragma warning disable CS8618             
    [JsonConstructor]
    internal EpisodeRevisionDataItem() { }
#pragma warning restore CS8618             
}
