using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
[JsonConverter(typeof(StringEnumerationJsonConverter<SearchSubjectsSort>))]
public struct SearchSubjectsSort : IStringEnumeration<SearchSubjectsSort>
{
    /// <summary>
    /// 匹配程度（默认）
    /// </summary>
    public static SearchSubjectsSort Match => new("match");
    /// <summary>
    /// 收藏人数
    /// </summary>
    public static SearchSubjectsSort Heat => new("heat");
    /// <summary>
    /// 排名由高到低
    /// </summary>
    public static SearchSubjectsSort Rank => new("rank");
    /// <summary>
    /// 评分
    /// </summary>
    public static SearchSubjectsSort Score => new("score");

    internal string? _str;

    private SearchSubjectsSort(string str)
    {
        _str = str;
    }

    readonly string? IStringEnumeration<SearchSubjectsSort>.StringValue => _str;
    static SearchSubjectsSort IStringEnumeration<SearchSubjectsSort>.Create(string? value) => new(value!);
}
