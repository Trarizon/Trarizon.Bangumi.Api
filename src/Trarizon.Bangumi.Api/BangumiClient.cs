using System.Net.Http.Headers;
using Trarizon.Bangumi.Api.Http;

namespace Trarizon.Bangumi.Api;
/// <summary>
/// Bangumi API客户端
/// </summary>
public interface IBangumiClient
{
    /// <summary>
    /// 使用的HttpClient
    /// </summary>
    HttpClient HttpClient { get; }
}

/// <summary>
/// Bangumi API客户端实现
/// </summary>
public sealed class BangumiClient : IBangumiClient, IDisposable
{
    private const string ApiServerBaseAddress = "https://api.bgm.tv";

    /// <inheritdoc />
    public HttpClient HttpClient { get; }

    /// <summary>
    /// 使用User-Agent和AccessToken创建默认BangumiClient
    /// </summary>
    /// <param name="userAgent"></param>
    /// <param name="accessToken">为null或空时，不设置AccessToken</param>
    public BangumiClient(string userAgent, string? accessToken = null)
    {
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiServerBaseAddress),
        };
        HttpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        if (!string.IsNullOrEmpty(accessToken))
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    /// <summary>
    /// 使用BangumiClientOptions创建BangumiClient
    /// </summary>
    /// <param name="options"></param>
    public BangumiClient(BangumiClientOptions options)
    {
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri(options.BaseAddress ?? ApiServerBaseAddress),
        };
        HttpClient.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
        if (options.AccessToken is not null)
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken);
        if (options.Timeout is { } timeout)
            HttpClient.Timeout = timeout;
    }

    /// <inheritdoc />
    public void Dispose() => HttpClient.Dispose();
}
