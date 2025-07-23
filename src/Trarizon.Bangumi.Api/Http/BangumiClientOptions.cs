namespace Trarizon.Bangumi.Api.Http;
/// <summary>
/// 构造BangumiClient的选项
/// </summary>
public sealed class BangumiClientOptions
{
    /// <summary>
    /// 服务器使用的域名，为null使用默认
    /// </summary>
    public string? BaseAddress { get; set; }
    
    /// <summary>
    /// User-Agent
    /// </summary>
    public required string UserAgent { get; set; }
    
    /// <summary>
    /// AccessToken，部分API需要AccessToken才能正常访问
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// HttpClient超时设置
    /// </summary>
    public TimeSpan? Timeout { get; set; }
}
