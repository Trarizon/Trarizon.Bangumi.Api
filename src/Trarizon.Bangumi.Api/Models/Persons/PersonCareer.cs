using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models.Persons;
// src: string
/// <summary>
/// 人物履历
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/model/person.go#L42">
/// Careers
/// </see>
/// </remarks>
[JsonConverter(typeof(StringEnumerationJsonConverter<PersonCareer>))]
public readonly struct PersonCareer : IStringEnumeration<PersonCareer>, IEquatable<PersonCareer>, IEquatable<string>
{
    /// <summary>
    /// 制作人员
    /// </summary>
    public static PersonCareer Producer => new("producer");
    /// <summary>
    /// 漫画家
    /// </summary>
    public static PersonCareer Mangaka => new("mangaka");
    /// <summary>
    /// 音乐人
    /// </summary>
    public static PersonCareer Artist => new("artist");
    /// <summary>
    /// 声优
    /// </summary>
    public static PersonCareer Seiyu => new("seiyu");
    /// <summary>
    /// 作家
    /// </summary>
    public static PersonCareer Writer => new("writer");
    /// <summary>
    /// 插画家
    /// </summary>
    public static PersonCareer Illustrator => new("illustrator");
    /// <summary>
    /// 演员
    /// </summary>
    public static PersonCareer Actor => new("actor");

    private readonly string _name;

    /// <summary>
    /// 字符串值
    /// </summary>
    public string? StringValue => _name;

    private PersonCareer(string name)
    {
        _name = name;
    }

    static PersonCareer IStringEnumeration<PersonCareer>.Create(string? value) => new(value!);

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public static implicit operator string(PersonCareer value) => value._name;

    public override string ToString() => _name;

    public bool Equals(PersonCareer other) => _name.Equals(other._name);
    public bool Equals(string? other) => _name.Equals(other);
    public override bool Equals(object? obj) => obj is PersonCareer career && Equals(career);
    public static bool operator ==(PersonCareer left, PersonCareer right) => left._name == right._name;
    public static bool operator !=(PersonCareer left, PersonCareer right) => left._name != right._name;
    
    public override int GetHashCode() => _name.GetHashCode();

#pragma warning restore CS1591
}
