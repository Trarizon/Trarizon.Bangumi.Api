using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Serialization.Converters;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// 条目搜索过滤器，各条件之间是且的关系
/// </summary>
public sealed class SearchSubjectsFilter
{
    /// <summary>
    /// 条目类型，或结合
    /// </summary>
    [JsonInclude, JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<SubjectType> Types { get => field ??= []; set; }

    /// <summary>
    /// 公共标签，与结合
    /// </summary>
    [JsonInclude, JsonPropertyName("meta_tags"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> MetaTags { get => field ??= []; set; }

    /// <summary>
    /// 标签，与结合
    /// </summary>
    [JsonInclude, JsonPropertyName("tag"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Tags { get => field ??= []; set; }

    /// <summary>
    /// 播出日期/发售日期
    /// </summary>
    [JsonInclude, JsonPropertyName("air_date"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(ListJsonConverter<ComparisonFilter<DateOnly>, ComparisonFilterDateOnlyJsonConverter>))]
    public List<ComparisonFilter<DateOnly>> AirDate { get => field ??= []; set; }

    /// <summary>
    /// 评分
    /// </summary>
    [JsonInclude, JsonPropertyName("rating"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(ListJsonConverter<ComparisonFilter<int>, ComparisonFilterUtf8JsonConverter<int>>))]
    public List<ComparisonFilter<int>> Rating { get => field ??= []; set; }

    /// <summary>
    /// 排名
    /// </summary>
    [JsonInclude, JsonPropertyName("rank"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(ListJsonConverter<ComparisonFilter<int>, ComparisonFilterUtf8JsonConverter<int>>))]
    public List<ComparisonFilter<int>> Rank { get => field ??= []; set; }

    /// <summary>
    /// R18条目筛选，无权限用户会忽略此属性
    /// </summary>
    [JsonInclude, JsonPropertyName("nsfw")]
    public NsfwFilter Nsfw { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public SearchSubjectsFilter Clone() => new()
    {
        Types = [.. Types],
        MetaTags = [.. MetaTags],
        Tags = [.. Tags],
        AirDate = [.. AirDate],
        Rating = [.. Rating],
        Rank = [.. Rank],
        Nsfw = Nsfw,
    };

#pragma warning restore CS1591
}
