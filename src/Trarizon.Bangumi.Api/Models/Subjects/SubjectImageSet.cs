using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Subjects;
// src: https://github.com/bangumi/server/blob/master/web/res/image.go#L47
/// <summary>
/// 条目相关图片链接
/// </summary>
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
