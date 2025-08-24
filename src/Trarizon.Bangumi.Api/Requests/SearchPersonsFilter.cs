using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.PersonModels;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// 人物搜索过滤器
/// </summary>
public sealed class SearchPersonsFilter
{
    /// <inheritdoc cref="Person.Careers"/>
    [JsonInclude, JsonPropertyName("career"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<PersonCareer> Careers { get => field ??= []; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public SearchPersonsFilter Clone() => new()
    {
        Careers = [.. Careers],
    };

#pragma warning restore CS1591
}
