using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models.Revisions;
/// <summary>
/// 人物编辑数据
/// </summary>
public sealed class PersonRevisionDataItem
{
    /// <summary>
    /// 人物信息表
    /// </summary>
    [JsonInclude, JsonPropertyName("prsn_infobox")]
    public string PersonInfoBox { get; internal set; }

    /// <summary>
    /// 人物简介
    /// </summary>
    [JsonInclude, JsonPropertyName("prsn_summary")]
    public string PersonSummary { get; internal set; }

    /// <summary>
    /// 人物职业
    /// </summary>
    [JsonInclude, JsonPropertyName("profession")]
    public PersonRevisionProfession RevisionProfession { get; internal set; }

    /// <summary>
    /// 编辑历史额外信息
    /// </summary>
    [JsonInclude, JsonPropertyName("extra")]
    public RevisionExtra RevisionExtra { get; internal set; }

    /// <summary>
    /// 人物名称
    /// </summary>
    [JsonInclude, JsonPropertyName("prsn_name")]
    public string PersonName { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618           
    internal PersonRevisionDataItem() { }
#pragma warning restore CS8618           
}
