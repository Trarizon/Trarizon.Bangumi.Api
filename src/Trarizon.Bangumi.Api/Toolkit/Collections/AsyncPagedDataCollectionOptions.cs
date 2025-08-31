namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// <see cref="AsyncPagedDataCollection{T}"/>的配置
/// </summary>
public sealed class AsyncPageCollectionOptions
{
    internal static AsyncPageCollectionOptions Default { get; } = new()
    {
        RequestInterval = TimeSpan.FromMilliseconds(250),
        MaxRetryCount = 3,
    };

    /// <summary>
    /// 两次请求之间的时间间隔
    /// </summary>
    public TimeSpan RequestInterval { get; set; }

    /// <summary>
    /// 最大重试次数
    /// </summary>
    public int MaxRetryCount { get; set; }

    /// <summary>
    /// 重试间隔，默认使用<see cref="RequestInterval"/>
    /// </summary>
    public TimeSpan? RetryInterval { get; set; }

    /// <summary></summary>
    public AsyncPageCollectionOptions Clone() => new()
    {
        RequestInterval = RequestInterval
    };
}
