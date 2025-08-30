using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.UserModels;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 用户头像尺寸
/// </summary>
/// <remarks>
/// 注释的尺寸是看网址猜的，不保证真的准确
/// </remarks>
[QueryStringEnum("Avatar")]
public enum AvatarSize
{
    /// <summary>
    /// 200x
    /// </summary>
    Large = SubjectImageSize.Large,
    /// <summary>
    /// 100x
    /// </summary>
    Medium = SubjectImageSize.Medium,
    /// <summary>
    /// 原图尺寸
    /// </summary>
    Small = SubjectImageSize.Small,
}

/// <summary>
/// 用户头像
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/user.go#L22">
/// Avatar
/// </see>
/// </remarks>
public struct Avatar
{
    /// <inheritdoc cref="AvatarSize.Large"/>
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    /// <inheritdoc cref="AvatarSize.Medium"/>
    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    /// <inheritdoc cref="AvatarSize.Small"/>
    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }
}
