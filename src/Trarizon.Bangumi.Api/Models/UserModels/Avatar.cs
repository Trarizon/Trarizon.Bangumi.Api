using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models.UserModels;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 用户头像尺寸
/// </summary>
public enum AvatarSize
{
    Small = SubjectImageSize.Small,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
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
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }
}

internal static class AvatarExtensions
{
    internal static string ToQueryString(this AvatarSize size) => size switch
    {
        AvatarSize.Small => "small",
        AvatarSize.Large => "large",
        AvatarSize.Medium => "medium",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };
}