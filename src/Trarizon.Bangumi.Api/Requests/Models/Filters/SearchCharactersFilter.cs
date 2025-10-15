using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Requests.Models;

namespace Trarizon.Bangumi.Api.Requests.Models.Filters;
/// <summary>
/// 角色搜索过滤器
/// </summary>
public sealed class SearchCharactersFilter
{
    /// <summary>
    /// R18条目筛选，无权限用户会忽略此属性
    /// </summary>
    [JsonInclude, JsonPropertyName("nsfw")]
    public NsfwFilter Nsfw { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public SearchCharactersFilter Clone() => new()
    {
        Nsfw = Nsfw,
    };

#pragma warning restore CS1591
}
