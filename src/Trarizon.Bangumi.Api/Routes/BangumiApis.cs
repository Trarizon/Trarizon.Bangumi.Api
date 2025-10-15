using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Serialization;

namespace Trarizon.Bangumi.Api.Routes;
/// <summary>
/// Bangumi API的入口实现
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/routes.go">
/// AddRouters
/// </see>
/// </remarks>
public static partial class BangumiApis
{
    internal const string ExperimentalApiDiagnosticId = "BgmExprApi";

    private static readonly MediaTypeHeaderValue _jsonHeaderValue = new("application/json");

    private static JsonContent CreateJsonContent<T>(T value, JsonTypeInfo<T> jsonTypeInfo)
        => JsonContent.Create(value, jsonTypeInfo, _jsonHeaderValue);

    // Client

    private static async Task<Uri> SendAndGetHeadersLocationWhenStatusFoundOrThrowAsync(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<Uri?> rtn = resp.StatusCode == HttpStatusCode.Found
            ? new(resp.StatusCode, resp.Headers.Location)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
        return rtn.Value!;
    }

    private static Task<Uri> GetHeadersLocationWhenStatusFoundOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
    {
        return client.SendAndGetHeadersLocationWhenStatusFoundOrThrowAsync(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);
    }

    private static async Task<T> SendAndFromJsonWhenSuccessStatusCodeOrThrowAsync<T>(this IBangumiClient client, HttpRequestMessage request, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult<T> rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false))!)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
        return rtn.Value;
    }

    private static async Task SendAndEnsureSuccessStatusCodeOrThrowAsync(this IBangumiClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var resp = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        BangumiApiResult rtn = resp.IsSuccessStatusCode
            ? new(resp.StatusCode)
            : new(resp.StatusCode, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
        rtn.ThrowIfError();
    }

    private static Task<T> GetFromJsonWhenSuccessStatusCodeOrThrowAsync<T>(this IBangumiClient client, string uri, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndFromJsonWhenSuccessStatusCodeOrThrowAsync(new HttpRequestMessage(HttpMethod.Get, uri), jsonTypeInfo, cancellationToken);
    }

    private static Task<TResponse> PostAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync<TRequest, TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) },
            responseJsonTypeInfo, cancellationToken);
    }

    private static Task PostEnsureSuccessStatusCodeOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
    {
        return client.SendAndEnsureSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Post, uri), cancellationToken);
    }

    private static Task PostAsJsonEnsureSuccessStatusCodeOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndEnsureSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Post, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) },
            cancellationToken);
    }

    private static Task DeleteEnsureSuccessStatusCodeOrThrowAsync(this IBangumiClient client, string uri, CancellationToken cancellationToken)
    {
        return client.SendAndEnsureSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Delete, uri), cancellationToken);
    }

    private static Task PatchAsJsonEnsureSuccessStatusCodeOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndEnsureSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Patch, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) },
            cancellationToken);
    }

    private static Task PutAsJsonEnsureSuccessStatusCodeOrThrowAsync<TRequest>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndEnsureSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) },
            cancellationToken);
    }

    private static Task<TResponse> PutAsJsonAndFromJsonWhenSuccessStatusCodeOrThrowAsync<TRequest,TResponse>(this IBangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
    {
        return client.SendAndFromJsonWhenSuccessStatusCodeOrThrowAsync(
            new HttpRequestMessage(HttpMethod.Put, uri) { Content = CreateJsonContent(requestBody, requestJsonTypeInfo) },
            responseJsonTypeInfo, cancellationToken);
    }
}
