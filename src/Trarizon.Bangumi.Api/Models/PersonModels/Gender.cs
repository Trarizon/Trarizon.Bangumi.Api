using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Internal.Attributes;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Models.PersonModels;
/// <summary>
/// 性别
/// </summary>
/// <remarks>
/// src <see href="https://github.com/bangumi/server/blob/master/web/res/character.go#L44" />
/// </remarks>
[JsonConverter(typeof(GenderJsonConverter))]
[JsonStringEnum]
public enum Gender
{
    /// <summary>
    /// 男性
    /// </summary>
    Male,
    /// <summary>
    /// 女性
    /// </summary>
    Female
}
