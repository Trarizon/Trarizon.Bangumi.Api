using System.Diagnostics;

namespace Trarizon.Bangumi.Api.Http.Responses;
public readonly struct RequestDetailUnion
{
    private readonly object? _obj;

    public bool IsNull => _obj is null;

    public string? GetString() => _obj as string;

    public RequestDetail? GetDetail() => _obj as RequestDetail;

    public string ToDetailString()
    {
        if (_obj is RequestDetail detail)
            return detail.ToDetailString();
        Debug.Assert(_obj is string, "Api doc wrong");
        return _obj.ToString() ?? "";
    }
}
