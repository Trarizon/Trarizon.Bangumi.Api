using System.Net.Http.Headers;

namespace Trarizon.Bangumi.Api;

/// <summary>
/// Bangumi API客户端
/// </summary>
public sealed class BangumiHttpClient : IBangumiClient, IDisposable
{
    private const string ApiServerBaseAddress = "https://api.bgm.tv";

    private readonly HttpClientHandler _httpClientHandler;

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <summary>
    /// 使用User-Agent和AccessToken创建默认BangumiClient
    /// </summary>
    /// <param name="userAgent"></param>
    /// <param name="accessToken">为null或空时，不设置AccessToken</param>
    public BangumiHttpClient(string userAgent, string? accessToken = null)
    {
        _httpClientHandler = new HttpClientHandler
        {
            AllowAutoRedirect = false
        };
        HttpClient = new HttpClient(_httpClientHandler)
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
    public BangumiHttpClient(BangumiHttpClientOptions options)
    {
        _httpClientHandler = new HttpClientHandler
        {
            AllowAutoRedirect = false
        };
        HttpClient = new HttpClient(_httpClientHandler)
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
    public void Dispose()
    {
        HttpClient.Dispose();
        _httpClientHandler.Dispose();
    }

    /// <inheritdoc/>
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
#if DEBUG
        Console.WriteLine($"Client requesting on {DateTime.Now} : {request.RequestUri}");
#endif
        return HttpClient.SendAsync(request, cancellationToken);
    }

}
