using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Exceptions;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// 封装API返回结果的Result
/// </summary>
internal readonly struct BangumiApiResult
{
    private readonly HttpStatusCode _httpStatusCode;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _httpStatusCode;

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

    internal BangumiApiResult(HttpStatusCode httpStatusCode)
    {
        _httpStatusCode = httpStatusCode;
    }

    internal BangumiApiResult(HttpStatusCode httpStatusCode, RequestError error)
        => (_httpStatusCode, _error) = (httpStatusCode, error);

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
internal readonly struct BangumiApiResult<T>
{
    private readonly HttpStatusCode _statusCode;
    private readonly T? _value;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _statusCode;

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

    internal BangumiApiResult(HttpStatusCode httpStatusCode, T value)
    {
        _statusCode = httpStatusCode;
        _value = value;
    }

    internal BangumiApiResult(HttpStatusCode httpStatusCode, RequestError error)
    {
        _statusCode = httpStatusCode;
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
