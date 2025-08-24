using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Responses;
/// <summary>
/// HTTP 请求详细信息，值可能为string或<see cref="RequestDetail"/>
/// </summary>
[JsonConverter(typeof(RequestDetailJsonConverter))]
public readonly struct RequestDetailUnion
{
    private readonly object? _obj;

    internal RequestDetailUnion(string str)
    {
        _obj = str;
    }

    internal RequestDetailUnion(RequestDetail requestDetail)
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
    public string? GetString() => _obj as string;

    /// <summary>
    /// 获取<see cref="RequestDetail"/>值，如果非<see cref="RequestDetail"/>值，返回null
    /// </summary>
    /// <returns></returns>
    public RequestDetail? GetDetail() => _obj as RequestDetail;

    /// <summary>
    /// 输出格式化信息
    /// </summary>
    /// <returns></returns>
    public string ToDetailString()
    {
        if (_obj is RequestDetail detail)
            return detail.ToDetailString();
        Debug.Assert(_obj is string, "Api doc wrong");
        return _obj.ToString() ?? "";
    }
}
