namespace Trarizon.Bangumi.Api;
/// <summary>
/// 构造BangumiClient的选项
/// </summary>
public sealed record class BangumiHttpClientOptions
{
    /// <summary>
    /// 服务器使用的域名，为null使用默认
    /// </summary>
    public string? BaseAddress { get; init; }
    
    /// <summary>
    /// User-Agent
    /// </summary>
    public required string UserAgent { get; init; }
    
    /// <summary>
    /// AccessToken，部分API需要AccessToken才能正常访问
    /// </summary>
    public string? AccessToken { get; init; }

    /// <summary>
    /// HttpClient超时设置
    /// </summary>
    public TimeSpan? Timeout { get; init; }

    /// <summary>
    /// 两次请求之间的时间间隔
    /// </summary>
    public TimeSpan? RequestInterval { get; init; }

    /// <summary>
    /// 最大重新请求次数
    /// </summary>
    public int MaxRetryCount { get; init; }
}
