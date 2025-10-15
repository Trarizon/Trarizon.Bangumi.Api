using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// HTTP 请求详细信息，值可能为string或<see cref="RequestDetails"/>
/// </summary>
[JsonConverter(typeof(RequestDetailJsonConverter))]
public readonly struct RequestDetailsData
{
    private readonly object? _obj;

    internal RequestDetailsData(string str)
    {
        _obj = str;
    }

    internal RequestDetailsData(RequestDetails requestDetail)
    {
        _obj = requestDetail;
    }

    /// <summary>
    /// 值为null
    /// </summary>
    public bool IsNull => _obj is null;

    /// <summary>
    /// 获取string值，如果非string值，返回null
    /// </summary>
    /// <returns></returns>
    public string? GetRawString() => _obj as string;

    /// <summary>
    /// 获取<see cref="RequestDetails"/>值，如果非<see cref="RequestDetails"/>值，返回null
    /// </summary>
    /// <returns></returns>
    public RequestDetails? GetRawDetail() => _obj as RequestDetails;

    /// <summary>
    /// 输出格式化信息
    /// </summary>
    /// <returns></returns>
    public string ToDetailString()
    {
        if (_obj is RequestDetails detail)
            return detail.ToDetailString();
        Debug.Assert(_obj is string, "Api doc wrong");
        return _obj.ToString() ?? "";
    }
}
