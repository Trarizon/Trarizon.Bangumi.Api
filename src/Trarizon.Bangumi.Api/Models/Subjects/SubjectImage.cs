using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Subjects;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 条目相关图片链接
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/image.go#L47">
/// SubjectImages
/// </see>
/// </remarks>
public struct SubjectImageSet
{
    /// <summary>
    /// 原图尺寸
    /// </summary>
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }
  
    /// <summary>
    /// 400x
    /// </summary>
    [JsonInclude, JsonPropertyName("common")]
    public string Common { get; internal set; }
  
    /// <summary>
    /// 800x
    /// </summary>
    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }
  
    /// <summary>
    /// 200x
    /// </summary>
    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }
  
    /// <summary>
    /// 100x
    /// </summary>
    [JsonInclude, JsonPropertyName("grid")]
    public string Grid { get; internal set; }
}
