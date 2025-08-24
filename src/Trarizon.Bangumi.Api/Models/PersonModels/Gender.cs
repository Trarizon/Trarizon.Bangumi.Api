using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
/// <summary>
/// 性别
/// </summary>
/// <remarks>
/// src <see href="https://github.com/bangumi/server/blob/master/web/res/character.go#L44" />
/// </remarks>
[JsonConverter(typeof(StringEnumerationJsonConverter<Gender>))]
public readonly struct Gender : IStringEnumeration<Gender>
{
    /// <summary>
    /// 未知
    /// </summary>
    public static Gender Unknown => default;
    /// <summary>
    /// 男性
    /// </summary>
    public static Gender Male => new("male");
    /// <summary>
    /// 女性
    /// </summary>
    public static Gender Female => new("female");

    private readonly string _value;

    internal Gender(string value) => _value = value;

    /// <inheritdoc/>
    public string? StringValue => _value;

    static Gender IStringEnumeration<Gender>.Create(string? value) => new(value!);
}
