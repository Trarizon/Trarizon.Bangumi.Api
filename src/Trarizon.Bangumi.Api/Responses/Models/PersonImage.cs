using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;

namespace Trarizon.Bangumi.Api.Responses.Models;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 人物图片尺寸
/// </summary>
/// <remarks>
/// 注释的尺寸是看网址猜的，不保证真的准确
/// </remarks>
[QueryStringEnum("PersonImage")]
public enum PersonImageSize
{
    /// <summary>
    /// 原图尺寸
    /// </summary>
    Large,
    /// <summary>
    /// 400x
    /// </summary>
    Medium,
    /// <summary>
    /// 100x
    /// </summary>
    Small,
    /// <summary>
    /// 75x75
    /// </summary>
    Grid,
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
    /// <inheritdoc cref="PersonImageSize.Large"/>
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    /// <inheritdoc cref="PersonImageSize.Medium"/>
    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    /// <inheritdoc cref="PersonImageSize.Small"/>
    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }

    /// <inheritdoc cref="PersonImageSize.Grid"/>
    [JsonInclude, JsonPropertyName("grid")]
    public string Grid { get; internal set; }
}
