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
    private const string ExperimentalApiDiagnosticId = "BgmExprApi";

    private const string V0Url = "/v0";

    private static readonly MediaTypeHeaderValue _jsonHeaderValue = new("application/json");

    private static JsonContent CreateJsonContent<T>(T value, JsonTypeInfo<T> jsonTypeInfo)
        => JsonContent.Create(value, jsonTypeInfo, _jsonHeaderValue);

    private static async ValueTask<BangumiApiResult<Uri?>> GetRequestUriWhenSuccessStatusCodeAsync(this HttpResponseMessage resp, CancellationToken cancellationToken)
    {
        if (resp.StatusCode is HttpStatusCode.Found)
            return new(resp, resp.Headers.Location);
        else
            return new(resp, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
    }


    private static async Task<BangumiApiResult<T>> ReadFromJsonWhenSuccessStatusCodeAsync<T>(this HttpResponseMessage resp, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        if (resp.IsSuccessStatusCode) {
            return new(resp, (await resp.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false))!);
        }
        else {
            return new(resp, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken: cancellationToken).ConfigureAwait(false))!);
        }
    }

    private static async ValueTask<BangumiApiResult> GetResultCheckSuccessStatusCodeAsync(this HttpResponseMessage resp, CancellationToken cancellationToken)
    {
        if (resp.IsSuccessStatusCode)
            return new(resp);
        else
            return new(resp, (await resp.Content.ReadFromJsonAsync(BangumiJsonSerializerContext.Default.RequestError, cancellationToken).ConfigureAwait(false))!);
    }

    private static async Task<BangumiApiResult<Uri?>> GetRequestUriWhenSuccessStatusCodeAsync(this BangumiClient client, string uri, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        return await resp.GetRequestUriWhenSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult<T>> GetFromJsonWhenSuccessStatusCodeAsync<T>(this BangumiClient client, string uri, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        return await resp.ReadFromJsonWhenSuccessStatusCodeAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult<T>> PostFromJsonWhenSuccessStatusCodeAsync<T>(this BangumiClient client, string uri, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PostAsync(uri, null, cancellationToken).ConfigureAwait(false);
        return await resp.ReadFromJsonWhenSuccessStatusCodeAsync(jsonTypeInfo, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult<TResponse>> PostAsJsonAndFromJsonWhenSuccessStatusCodeAsync<TRequest, TResponse>(this BangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken) where TResponse : class
    {
        var resp = await client.HttpClient.PostAsync(uri, CreateJsonContent(requestBody, requestJsonTypeInfo), cancellationToken).ConfigureAwait(false);
        return await resp.ReadFromJsonWhenSuccessStatusCodeAsync(responseJsonTypeInfo, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult> PostAsJsonEnsureSuccessStatusCodeAsync<TRequest>(this BangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PostAsync(uri, CreateJsonContent(requestBody, requestJsonTypeInfo), cancellationToken).ConfigureAwait(false);
        return await resp.GetResultCheckSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult> PostEnsureSuccessStatusCodeAsync(this BangumiClient client, string uri, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PostAsync(uri, null, cancellationToken).ConfigureAwait(false);
        return await resp.GetResultCheckSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult> PatchAsJsonEnsureSuccessStatusCodeAsync<TRequest>(this BangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PatchAsync(uri, CreateJsonContent(requestBody, requestJsonTypeInfo), cancellationToken).ConfigureAwait(false);
        return await resp.GetResultCheckSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult<TResponse>> PutAsJsonAndFromJsonWhenSuccessStatusCodeAsync<TRequest, TResponse>(this BangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, JsonTypeInfo<TResponse> responseJsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PutAsync(uri, CreateJsonContent(requestBody, requestJsonTypeInfo), cancellationToken).ConfigureAwait(false);
        return await resp.ReadFromJsonWhenSuccessStatusCodeAsync(responseJsonTypeInfo, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult> PutAsJsonEnsureSuccessStatusCodeAsync<TRequest>(this BangumiClient client, string uri, TRequest requestBody, JsonTypeInfo<TRequest> requestJsonTypeInfo, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.PutAsync(uri, CreateJsonContent(requestBody, requestJsonTypeInfo), cancellationToken).ConfigureAwait(false);
        return await resp.GetResultCheckSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task<BangumiApiResult> DeleteEnsureSuccessStatusCodeAsync(this BangumiClient client, string uri, CancellationToken cancellationToken)
    {
        var resp = await client.HttpClient.DeleteAsync(uri, cancellationToken).ConfigureAwait(false);
        return await resp.GetResultCheckSuccessStatusCodeAsync(cancellationToken).ConfigureAwait(false);
    }
}
