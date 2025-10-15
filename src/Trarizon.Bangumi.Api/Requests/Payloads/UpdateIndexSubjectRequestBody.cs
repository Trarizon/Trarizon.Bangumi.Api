using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Requests.Payloads;
/// <summary>
/// 
/// </summary>
public sealed class UpdateIndexSubjectRequestBody
{
    /// <summary>
    /// 目录内排序
    /// </summary>
    [JsonInclude, JsonPropertyName("sort")]
    public int? Sort { get; set; }

    /// <inheritdoc cref="IndexSubject.Comment"/>
    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public UpdateIndexSubjectRequestBody Clone() => new()
    {
        Sort = Sort,
        Comment = Comment,
    };

#pragma warning restore CS1591 
}
