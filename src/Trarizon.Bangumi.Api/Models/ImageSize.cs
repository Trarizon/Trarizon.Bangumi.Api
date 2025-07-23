using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 条目图片尺寸
/// </summary>
public enum SubjectImageSize
{
    Small,
    Grid,
    Large,
    Medium,
    Common
}

/// <summary>
/// 人物图片尺寸
/// </summary>
public enum PersonImageSize
{
    Small = SubjectImageSize.Small,
    Grid = SubjectImageSize.Grid,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
}

/// <summary>
/// 用户头像尺寸
/// </summary>
public enum AvatarSize
{
    Small = SubjectImageSize.Small,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
}

internal static class ImageSizeExtensions
{
    internal static string ToUrlQueryString(this SubjectImageSize size) => size switch
    {
        SubjectImageSize.Small => "small",
        SubjectImageSize.Grid => "grid",
        SubjectImageSize.Large => "large",
        SubjectImageSize.Medium => "medium",
        SubjectImageSize.Common => "common",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };

    internal static string ToUrlQueryString(this PersonImageSize size) => size switch
    {
        PersonImageSize.Small => "small",
        PersonImageSize.Grid => "grid",
        PersonImageSize.Large => "large",
        PersonImageSize.Medium => "medium",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };

    internal static string ToUrlQueryString(this AvatarSize size) => size switch
    {
        AvatarSize.Small => "small",
        AvatarSize.Large => "large",
        AvatarSize.Medium => "medium",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };
}
