using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Indices;

namespace Trarizon.Bangumi.Api.Http.Requests;
/// <summary>
/// 
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/req/index.go#L19">
/// IndexBasicInfo
/// </see>
/// </remarks>
public sealed class AddIndexRequestBody
{
    /// <inheritdoc cref="BangumiIndex.Title"/>
    /// <remarks>
    /// 目录名称若为空，会导致网页端无法直接点击超链接，因此此处设计为<see langword="required"/>
    /// <br/>
    /// 若坚持命名为空字符串，可以直接赋值为<c>""</c>
    /// </remarks>
    [JsonInclude, JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <inheritdoc cref="BangumiIndex.Description"/>
    [JsonInclude, JsonPropertyName("description")]
    public string? Description { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public AddIndexRequestBody Clone() => new()
    {
        Title = Title,
        Description = Description,
    };

    public UpdateIndexInfoRequestBody ToUpdateIndexInfoRequestBody() => new()
    {
        Title = Title,
        Description = Description,
    };

#pragma warning restore CS1591
}
