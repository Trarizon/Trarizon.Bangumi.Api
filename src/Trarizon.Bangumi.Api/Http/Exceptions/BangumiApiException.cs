using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Http.Responses;

namespace Trarizon.Bangumi.Api.Http.Exceptions;
public sealed class BangumiApiException(HttpResponseMessage resp, RequestError error) : Exception
{
    public HttpResponseMessage ResponseMessage => resp;

    public HttpStatusCode? StatusCode => resp.StatusCode;

    public RequestError Error => error;

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
    public static void Throw(BangumiApiResult result)
    {
        Debug.Assert(result.IsError);
        throw new BangumiApiException(result.ResponseMessage, result.Error);
    }

    [DoesNotReturn]
    public static void Throw<T>(BangumiApiResult<T> result)
    {
        Debug.Assert(result.IsError);
        throw new BangumiApiException(result.ResponseMessage, result.Error);
    }
}
