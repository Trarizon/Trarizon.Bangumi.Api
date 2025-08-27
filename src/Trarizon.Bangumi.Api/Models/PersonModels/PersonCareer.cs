using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
// src: string
/// <summary>
/// 人物履历
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/model/person.go#L42">
/// Careers
/// </see>
/// </remarks>
[JsonConverter(typeof(PersonCareerJsonConverter))]
public enum PersonCareer
{
    /// <summary>
    /// 制作人员
    /// </summary>
    Producer,
    /// <summary>
    /// 漫画家
    /// </summary>
    Mangaka,
    /// <summary>
    /// 音乐人
    /// </summary>
    Artist,
    /// <summary>
    /// 声优
    /// </summary>
    Seiyu,
    /// <summary>
    /// 作家
    /// </summary>
    Writer,
    /// <summary>
    /// 插画家
    /// </summary>
    Illustrator,
    /// <summary>
    /// 演员
    /// </summary>
    Actor,
}

internal static class PersonCareerExtensions
{
    extension(PersonCareer career)
    {
        internal static PersonCareer FromJsonStringValue(string str) => str switch
        {
            "producer" => PersonCareer.Producer,
            "mangaka" => PersonCareer.Mangaka,
            "artist" => PersonCareer.Artist,
            "seiyu" => PersonCareer.Seiyu,
            "writer" => PersonCareer.Writer,
            "illustrator" => PersonCareer.Illustrator,
            "actor" => PersonCareer.Actor,
            _ => Throws.ThrowUnknownEnumCastValue<PersonCareer>(str),
        };

        internal string ToJsonStringValue() => career switch
        {
            PersonCareer.Producer => "producer",
            PersonCareer.Mangaka => "mangaka",
            PersonCareer.Artist => "artist",
            PersonCareer.Seiyu => "seiyu",
            PersonCareer.Writer => "writer",
            PersonCareer.Illustrator => "illustrator",
            PersonCareer.Actor => "actor",
            _ => Throws.ThrowUnknownEnumValue<string>(career),
        };
    }
}