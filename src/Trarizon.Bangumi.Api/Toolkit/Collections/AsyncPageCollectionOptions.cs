namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// <see cref="AsyncPageCollection{T}"/>的配置
/// </summary>
public sealed class AsyncPageCollectionOptions
{
    internal static AsyncPageCollectionOptions Default { get; } = new()
    {
        RequestInterval = TimeSpan.FromMilliseconds(500),
    };

    /// <summary>
    /// 两次请求之间的时间间隔
    /// </summary>
    public TimeSpan RequestInterval { get; set; }

    /// <summary></summary>
    public AsyncPageCollectionOptions Clone() => new()
    {
        RequestInterval = RequestInterval
    };
}
