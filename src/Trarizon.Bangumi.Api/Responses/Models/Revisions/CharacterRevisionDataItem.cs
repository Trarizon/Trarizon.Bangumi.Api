using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models.Revisions;
/// <summary>
/// 角色编辑数据
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L85">
/// CharaacterRevisionDataItem
/// </see>
/// </remarks>
public sealed class CharacterRevisionDataItem
{
    /// <summary>
    /// 角色信息表
    /// </summary>
    [JsonInclude, JsonPropertyName("infobox")]
    public string CharacterInfoBox { get; internal set; }

    /// <summary>
    /// 角色简介
    /// </summary>
    [JsonInclude, JsonPropertyName("summary")]
    public string CharacterSummary { get; internal set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string CharacterName { get; internal set; }

    /// <summary>
    /// 编辑历史额外信息
    /// </summary>
    [JsonInclude, JsonPropertyName("extra")]
    public RevisionExtra RevisionExtra { get; internal set; }

    [JsonConstructor]
#pragma warning disable CS8618           
    internal CharacterRevisionDataItem() { }
#pragma warning restore CS8618           
}
