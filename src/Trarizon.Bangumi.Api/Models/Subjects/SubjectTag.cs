using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// src: https://github.com/bangumi/server/blob/master/web/res/subject.go#L38
/// <summary>
/// 条目tag
/// </summary>
[DebuggerDisplay("{Name} (Count = {Count})")]
public struct SubjectTag
{
    /// <summary>
    /// Tag名称
    /// </summary>
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    // src: uint
    /// <summary>
    /// Tag数量
    /// </summary>
    [JsonInclude, JsonPropertyName("count")]
    public int Count { get; internal set; }

    // src: uint
    // TODO: API页定义不存在，但是返回结果存在
    // search结果存在值，其他值都是0
    //[JsonInclude, JsonPropertyName("total_cont")]
    //public int TotalCount { get; internal set; }
}
