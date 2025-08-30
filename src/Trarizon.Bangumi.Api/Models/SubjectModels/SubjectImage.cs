using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 条目图片尺寸
/// </summary>
/// <remarks>
/// 注释的尺寸是看网址猜的，不保证真的准确
/// </remarks>
[QueryStringEnum("SubjectImage")]
public enum SubjectImageSize
{
    /// <summary>
    /// 原图尺寸
    /// </summary>
    Large,
    /// <summary>
    /// 800x
    /// </summary>
    Medium,
    /// <summary>
    /// 400x
    /// </summary>
    Common,
    /// <summary>
    /// 200x
    /// </summary>
    Small,
    /// <summary>
    /// 100x
    /// </summary>
    Grid,
}

/// <summary>
/// 条目相关图片链接
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/image.go#L47">
/// SubjectImages
/// </see>
/// </remarks>
public struct SubjectImageSet
{
    /// <inheritdoc cref="SubjectImageSize.Large"/>
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    /// <inheritdoc cref="SubjectImageSize.Common"/>
    [JsonInclude, JsonPropertyName("common")]
    public string Common { get; internal set; }

    /// <inheritdoc cref="SubjectImageSize.Medium"/>
    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    /// <inheritdoc cref="SubjectImageSize.Small"/>
    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }

    /// <inheritdoc cref="SubjectImageSize.Grid"/>
    [JsonInclude, JsonPropertyName("grid")]
    public string Grid { get; internal set; }
}
