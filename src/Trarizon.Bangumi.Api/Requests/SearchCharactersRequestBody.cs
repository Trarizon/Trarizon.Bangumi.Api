using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// 
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/search/character/handle.go#L23">
/// Req
/// </see>
/// </remarks>
public sealed class SearchCharactersRequestBody
{
    /// <summary>
    /// 搜索关键字
    /// </summary>
    [JsonInclude, JsonPropertyName("keyword")]
    public string? Keyword { get; set; }

    /// <summary>
    /// 筛选条件
    /// </summary>
    [JsonInclude, JsonPropertyName("filter"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SearchCharactersFilter? Filter { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public SearchCharactersRequestBody Clone() => new()
    {
        Keyword = Keyword,
        Filter = Filter?.Clone(),
    };

#pragma warning restore CS1591
}
