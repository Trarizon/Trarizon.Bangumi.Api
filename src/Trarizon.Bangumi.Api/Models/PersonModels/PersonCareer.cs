using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;
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
[JsonStringEnum]
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
