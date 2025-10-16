using System.Net;
using System.Net.Http.Headers;

namespace Trarizon.Bangumi.Api;

/// <summary>
/// Bangumi API客户端
/// </summary>
public sealed class BangumiHttpClient : IBangumiClient, IDisposable
{
    private const string ApiServerBaseAddress = "https://api.bgm.tv";

    private readonly HttpClientHandler _httpClientHandler;
    private readonly BangumiHttpClientOptions _options;

    private readonly Timer? _sendTimer;
    private readonly SemaphoreSlim _sendSemaphore = default!;

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <summary>
    /// 使用User-Agent和AccessToken创建默认BangumiClient
    /// </summary>
    /// <param name="userAgent"></param>
    /// <param name="accessToken">为null或空时，不设置AccessToken</param>
    public BangumiHttpClient(string userAgent, string? accessToken = null) : this(new BangumiHttpClientOptions
    {
        UserAgent = userAgent,
        AccessToken = accessToken,
    })
    { }

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
        _options = options;
        HttpClient = new HttpClient(_httpClientHandler)
        {
            BaseAddress = new Uri(options.BaseAddress ?? ApiServerBaseAddress),
        };
        HttpClient.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
        if (options.AccessToken is not null)
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken);
        if (options.Timeout is { } timeout)
            HttpClient.Timeout = timeout;
        if (options.RequestInterval is { } interval) {
            _sendSemaphore = new SemaphoreSlim(1, 1);
            _sendTimer = new Timer(obj => { _sendSemaphore.Release(); }, null, Timeout.Infinite, Timeout.Infinite);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        HttpClient.Dispose();
        _httpClientHandler.Dispose();
        _sendTimer?.Dispose();
        _sendSemaphore?.Dispose();
    }

    /// <inheritdoc/>
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
#if DEBUG
        Console.WriteLine($"Client requesting on {DateTime.Now} : {request.RequestUri}");
#endif

        if (_options.MaxRetryCount <= 0)
            return SendWithIntervalAsync(request, cancellationToken);

        return WithRetry();

        async Task<HttpResponseMessage> WithRetry()
        {
            int retryCount = -1;

        Retry:
            retryCount++;

            HttpResponseMessage resp;
            try {
                resp = await SendWithIntervalAsync(request, cancellationToken).ConfigureAwait(false);
                
                if (resp.StatusCode is HttpStatusCode.TooManyRequests) {
                    // Force wait to if status code is TooManyRequests and not set RequestInterval
                    if (_options.RequestInterval is null) {
                        await Task.Delay(500, cancellationToken).ConfigureAwait(false);
                    }
                    goto Retry;
                }
                if (resp.StatusCode >= HttpStatusCode.InternalServerError) {
                    goto Retry;
                }
                return resp;
            }
            catch (OperationCanceledException) { throw; }
        }
    }

    private Task<HttpResponseMessage> SendWithIntervalAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        if (_sendTimer is null) {
            return HttpClient.SendAsync(request, cancellationToken);
        }

        return WithInterval();

        async Task<HttpResponseMessage> WithInterval()
        {
            await _sendSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            var resp = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            _sendTimer.Change(_options.RequestInterval!.Value, Timeout.InfiniteTimeSpan);
            return resp;
        }
    }
}
