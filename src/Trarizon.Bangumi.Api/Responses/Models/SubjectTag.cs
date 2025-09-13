using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses.Models;
/// <summary>
/// 条目tag
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L38">
/// SubjectTag
/// </see>
/// </remarks>
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
    /// <remarks>
    /// 该值目前仅在Search结果中存在，其他使用的 <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L144">ToSubjectV0</see>
    /// 没有进行赋值
    /// </remarks>
    [JsonInclude, JsonPropertyName("total_cont")]
    public int TotalCount { get; internal set; }
}
