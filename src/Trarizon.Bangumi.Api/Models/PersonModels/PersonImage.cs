using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 人物图片尺寸
/// </summary>
[QueryStringEnum("PersonImage")]
public enum PersonImageSize
{
    Small = SubjectImageSize.Small,
    Grid = SubjectImageSize.Grid,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
}

/// <summary>
/// 人物相关图片链接
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/image.go#L25">
/// PersonImages
/// </see>
/// </remarks>
public struct PersonImageSet
{
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }

    [JsonInclude, JsonPropertyName("grid")]
    public string Grid { get; internal set; }
}
