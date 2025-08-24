using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Exceptions;
/// <summary>
/// Bangumi API未返回成功值的异常
/// </summary>
/// <param name="resp"></param>
/// <param name="error"></param>
public sealed class BangumiApiException(HttpResponseMessage resp, RequestError error) : Exception
{
    /// <summary>
    /// 响应消息
    /// </summary>
    public HttpResponseMessage ResponseMessage => resp;

    /// <summary>
    /// Http状态码
    /// </summary>
    public HttpStatusCode? StatusCode => resp.StatusCode;

    /// <summary>
    /// Bangumi API返回的错误信息
    /// </summary>
    public RequestError Error => error;

    /// <inheritdoc />
    public override string Message
    {
        get {
            DefaultInterpolatedStringHandler builder = $"Api responsed with Error '{error.Title}': {error.Description}";
            var code = resp.StatusCode;
            if (code is { } c) {
                builder.AppendLiteral("\nStatusCode: ");
                builder.AppendFormatted((int)c);
                builder.AppendLiteral(" ");
                builder.AppendFormatted(code);
            }
            return builder.ToStringAndClear();
        }
    }

    [DoesNotReturn]
    internal static void Throw(BangumiApiResult result)
    {
        Debug.Assert(result.IsError);
        throw new BangumiApiException(result.ResponseMessage, result.Error);
    }

    [DoesNotReturn]
    internal static void Throw<T>(BangumiApiResult<T> result)
    {
        Debug.Assert(result.IsError);
        throw new BangumiApiException(result.ResponseMessage, result.Error);
    }
}
