using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// HTTP 请求详细信息
/// </summary>
public sealed class RequestDetail
{
    /// <summary>
    /// HTTP 请求路径
    /// </summary>
    [JsonInclude, JsonPropertyName("path")]
    public string? Path { get; internal set; }

    /// <summary>
    /// HTTP 请求方法
    /// </summary>
    [JsonInclude, JsonPropertyName("method")]
    public string? Method { get; internal set; }

    [JsonConstructor]
    internal RequestDetail() { }

    /// <summary>
    /// 输出格式化信息
    /// </summary>
    /// <returns></returns>
    public string ToDetailString() => $"{{ Path: '{Path}', Method: '{Method}' }}";
}
