using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// Bangumi API返回的错误
/// </summary>
public sealed class RequestError
{
    /// <summary>
    /// 标题
    /// </summary>
    [JsonInclude, JsonPropertyName("title")]
    public string Title { get; internal set; }

    /// <summary>
    /// 详细信息
    /// </summary>
    [JsonInclude, JsonPropertyName("details")]
    public RequestDetailUnion Details { get; internal set; }
    
    //public string RequestId { get; internal set; }
    
    /// <summary>
    /// 错误描述
    /// </summary>
    [JsonInclude, JsonPropertyName("description")]
    public string Description { get; internal set; }

#pragma warning disable CS8618 
    [JsonConstructor]
    internal RequestError() { }
#pragma warning restore CS8618 
}
