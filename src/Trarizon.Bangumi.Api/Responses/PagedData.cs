using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// 表示单页搜索结果
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedData<T> 
{
    /// <summary>
    /// 搜索结果总数
    /// </summary>
    [JsonInclude, JsonPropertyName("total")]
    public long Total { get; internal set; }

    /// <summary>
    /// 最大单页数量
    /// </summary>
    [JsonInclude, JsonPropertyName("limit")]
    public int Limit { get; internal set; }

    /// <summary>
    /// 当前页面偏移
    /// </summary>
    [JsonInclude, JsonPropertyName("offset")]
    public int Offset { get; internal set; }

    /// <summary>
    /// 页面结果数据
    /// </summary>
    [JsonInclude, JsonPropertyName("data")]
    public ImmutableArray<T> Datas { get; internal set; }

    [JsonConstructor]
    internal PagedData() { }
}
