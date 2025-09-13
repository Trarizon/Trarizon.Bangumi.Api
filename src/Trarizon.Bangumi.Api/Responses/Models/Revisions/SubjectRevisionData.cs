using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Responses.Models.Revisions;
/// <summary>
/// 条目编辑数据
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L54">
/// SubjectRevisionData
/// </see>
/// </remarks>
public sealed class SubjectRevisionData
{
    // TODO:有几个不明确的类型
    // src: uint32
    /// <summary>
    /// 条目章节数量
    /// </summary>
    [JsonInclude, JsonPropertyName("field_eps")]
    public int EpisodeCount { get; internal set; }

    /// <summary>
    /// 编辑条目信息表
    /// </summary>
    [JsonInclude, JsonPropertyName("field_infobox")]
    public string InfoBox { get; internal set; }

    /// <summary>
    /// 条目简介
    /// </summary>
    [JsonInclude, JsonPropertyName("field_summary")]
    public string Summary { get; internal set; }

    /// <summary>
    /// 条目名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// 条目中文名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    // src: uint16
    /// <summary>
    /// 条目平台ID
    /// </summary>
    [JsonInclude, JsonPropertyName("platform")]
    public int Platform { get; internal set; }

    /// <summary>
    /// 条目ID
    /// </summary>
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    /// <summary>
    /// 条目类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // 我也不知道这是什么

    // src: uint16
    [JsonInclude, JsonPropertyName("type_id")]
    public int TypeId { get; internal set; }

    [JsonInclude, JsonPropertyName("vote_field")]
    public string Vote { get; internal set; }

#pragma warning restore CS1591

#pragma warning disable CS8618          
    [JsonConstructor]
    internal SubjectRevisionData() { }
#pragma warning restore CS8618          
}
