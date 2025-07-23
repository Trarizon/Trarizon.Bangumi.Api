using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
/// <summary>
/// 编辑历史额外信息
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/revision.go#L33">
/// Extra
/// </see>
/// </remarks>
public sealed class RevisionExtra
{
    /// <summary>
    /// 图片
    /// </summary>
    [JsonInclude, JsonPropertyName("img")]
    public string? Image { get; internal set; }

    [JsonConstructor]
    internal RevisionExtra() { }
}
