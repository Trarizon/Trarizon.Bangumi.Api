using System.Net.Http.Headers;
using Trarizon.Bangumi.Api.Http;

namespace Trarizon.Bangumi.Api;
public interface IBangumiClient
{
    HttpClient HttpClient { get; }
}

public sealed class BangumiClient : IBangumiClient, IDisposable
{
    private const string ApiServerBaseAddress = "https://api.bgm.tv";

    public HttpClient HttpClient { get; }

    public BangumiClient(string userAgent, string? accessToken = null)
    {
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiServerBaseAddress),
        };
        HttpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        if (accessToken is not null)
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

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

    public void Dispose() => HttpClient.Dispose();
}
