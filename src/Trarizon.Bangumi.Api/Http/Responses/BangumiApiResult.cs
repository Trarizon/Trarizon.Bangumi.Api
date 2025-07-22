using System.Diagnostics.CodeAnalysis;
using System.Net;
using Trarizon.Bangumi.Api.Http.Exceptions;

namespace Trarizon.Bangumi.Api.Http.Responses;
/// <summary>
/// 封装API返回结果的Result
/// </summary>
public readonly struct BangumiApiResult
{
    private readonly HttpResponseMessage _resp;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求返回的消息
    /// </summary>
    public HttpResponseMessage ResponseMessage => _resp;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _resp.StatusCode;

    /// <summary>
    /// 是否成功
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => _error is null;

    /// <summary>
    /// 是否返回错误
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsError => _error is not null;

    /// <summary>
    /// 错误信息
    /// </summary>
    public RequestError? Error => _error;

    internal BangumiApiResult(HttpResponseMessage resp)
    {
        _resp = resp;
    }

    internal BangumiApiResult(HttpResponseMessage resp, RequestError error)
        => (_resp, _error) = (resp, error);

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    public void ThrowIfError()
    {
        if (IsError)
            BangumiApiException.Throw(this);
    }
}

/// <summary>
/// 封装API返回结果的Result
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct BangumiApiResult<T>
{
    private readonly HttpResponseMessage _resp;
    private readonly T? _value;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求返回的消息
    /// </summary>
    public HttpResponseMessage ResponseMessage => _resp;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _resp.StatusCode;

    /// <summary>
    /// 是否成功
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => _error is null;

    /// <summary>
    /// 是否返回错误
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsError => _error is not null;

    /// <summary>
    /// 成功时返回的数据
    /// </summary>
    public T? Value => _value;

    /// <summary>
    /// 错误信息
    /// </summary>
    public RequestError? Error => _error;

    internal BangumiApiResult(HttpResponseMessage resp, T value)
    {
        _resp = resp;
        _value = value;
    }

    internal BangumiApiResult(HttpResponseMessage resp, RequestError error)
    {
        _resp = resp;
        _error = error;
    }

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    [MemberNotNull(nameof(Value))]
    public void ThrowIfError()
    {
        if (IsError) {
            BangumiApiException.Throw(this);
        }
    }
}

/// <summary>
/// <see cref="BangumiApiResult"/>相关扩展
/// </summary>
public static class BangumiApiResultExtensions
{
    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resultTask"></param>
    /// <returns></returns>
    public static async Task<T> ThrowIfError<T>(this Task<BangumiApiResult<T>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        result.ThrowIfError();
        return result.Value;
    }

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    /// <param name="resultTask"></param>
    /// <returns></returns>
    public static async Task ThrowIfError(this Task<BangumiApiResult> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        result.ThrowIfError();
    }
}
