using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Serialization;

namespace Trarizon.Bangumi.Api.Routes;

internal static class BangumiHttpExtensions
{
    private static readonly MediaTypeHeaderValue _jsonHeaderValue = new("application/json");

    private static JsonContent CreateJsonContent<T>(T value, JsonTypeInfo<T> jsonTypeInfo)
        => JsonContent.Create(value, jsonTypeInfo, _jsonHeaderValue);

    // Client

    private static async Task<BangumiApiResult<Uri>> SendFromHeadersLocationAsyncInternal(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<Uri> rtn = resp.StatusCode == HttpStatusCode.Found
            ? new(resp.StatusCode, resp.Headers.Location!)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        return rtn;
    }

    private static async Task<Uri> SendFromHeadersLocationOrThrowAsync(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<Uri?> rtn = resp.StatusCode == HttpStatusCode.Found
            ? new(resp.StatusCode, resp.Headers.Location)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
        return rtn.Value!;
    }

    private static async Task<BangumiApiResult<T>> SendFromJsonAsyncInternal<T>(this IBangumiClient client, HttpRequestMessage request, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<T> rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false))!)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        return rtn;
    }

    private static async Task<T> SendFromJsonOrThrowAsyncInternal<T>(this IBangumiClient client, HttpRequestMessage request, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<T> rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false))!)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
        return rtn.Value;
    }

    private static async Task<BangumiApiResult> SendAsyncInternal(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        return rtn;
    }


    private static async Task SendOrThrowAsyncInternal(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
    }

    #region Get

    public static Task<Uri> GetHeadersLocationOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendFromHeadersLocationOrThrowAsync(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);

    public static Task<BangumiApiResult<Uri>> GetHeadersLocationAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendFromHeadersLocationAsyncInternal(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);

    public static Task<T> GetFromJsonOrThrowAsync<T>(this IBangumiClient client, string uri, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Get, uri), jsonTypeInfo, cancellationToken);

    public static Task<BangumiApiResult<T>> GetFromJsonAsync<T>(this IBangumiClient client, string uri, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonAsyncInternal(new HttpRequestMessage(HttpMethod.Get, uri), jsonTypeInfo, cancellationToken);

    #endregion

    #region Post

    public static Task<TResponse> PostFromJsonOrThrowAsync<TRequest, TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, responseJsonTypeInfo, cancellationToken);

    public static Task<BangumiApiResult<TResponse>> PostFromJsonAsync<TRequest, TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, responseJsonTypeInfo, cancellationToken);

    public static Task PostOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri), cancellationToken);

    public static Task<BangumiApiResult> PostAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri), cancellationToken);

    public static Task PostOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    public static Task<BangumiApiResult> PostAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendAsyncInternal(new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    #endregion

    #region Delete Patch Put

    public static Task DeleteOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Delete, uri), cancellationToken);

    public static Task<BangumiApiResult> DeleteAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
        => client.SendAsyncInternal(new HttpRequestMessage(HttpMethod.Delete, uri), cancellationToken);

    public static Task PatchOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Patch, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    public static Task<BangumiApiResult> PatchAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendAsyncInternal(new HttpRequestMessage(HttpMethod.Patch, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    public static Task PutOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    public static Task<BangumiApiResult> PutAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendAsyncInternal(new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, cancellationToken);

    public static Task<TResponse> PutFromJsonOrThrowAsync<TRequest, TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonOrThrowAsyncInternal(new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, responseJsonTypeInfo, cancellationToken);

    public static Task<BangumiApiResult<TResponse>> PutFromJsonAsync<TRequest, TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
        => client.SendFromJsonAsyncInternal(new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) }, responseJsonTypeInfo, cancellationToken);

    #endregion
}
