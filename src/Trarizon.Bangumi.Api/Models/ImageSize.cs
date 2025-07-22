using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models;
public enum SubjectImageSize
{
    Small,
    Grid,
    Large,
    Medium,
    Common
}

public enum PersonImageSize
{
    Small = SubjectImageSize.Small,
    Grid = SubjectImageSize.Grid,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
}

public enum AvatarSize
{
    Small = SubjectImageSize.Small,
    Large = SubjectImageSize.Large,
    Medium = SubjectImageSize.Medium,
}

public static class ImageSizeExtensions
{
    public static string ToUrlQueryString(this SubjectImageSize size) => size switch
    {
        SubjectImageSize.Small => "small",
        SubjectImageSize.Grid => "grid",
        SubjectImageSize.Large => "large",
        SubjectImageSize.Medium => "medium",
        SubjectImageSize.Common => "common",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };

    public static string ToUrlQueryString(this PersonImageSize size) => size switch
    {
        PersonImageSize.Small => "small",
        PersonImageSize.Grid => "grid",
        PersonImageSize.Large => "large",
        PersonImageSize.Medium => "medium",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };

    public static string ToUrlQueryString(this AvatarSize size) => size switch
    {
        AvatarSize.Small => "small",
        AvatarSize.Large => "large",
        AvatarSize.Medium => "medium",
        _ => Throws.ThrowUnknownEnumValue<string>(size),
    };
}
