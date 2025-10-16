namespace Trarizon.Bangumi.Api;

/// <summary>
/// Bangumi API客户端
/// </summary>
public interface IBangumiClient : IDisposable
{
    /// <summary>
    /// 发送HTTP请求
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);
}
