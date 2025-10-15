using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Trarizon.Bangumi.Api.Utilities;

internal ref struct QueryBuilder
{
    private DefaultInterpolatedStringHandler _handler;
    private bool _and;

    public QueryBuilder(DefaultInterpolatedStringHandler handler)
    {
        _handler = handler;
    }

    public QueryBuilder(string initText)
    {
        _handler = $"{initText}";
    }

    public QueryBuilder(IFormatProvider? formatProvider, Span<char> initBuffer,
        string initText)
    {
        _handler = new DefaultInterpolatedStringHandler(0, 0, formatProvider, initBuffer);
        _handler.AppendLiteral(initText);
    }

    public void AppendQuery<T>(string key, T value)
    {
        AppendKeyPart(key);
        _handler.AppendFormatted(value);
    }

    public void AppendQuery(string key, string value)
    {
        AppendKeyPart(key);
        _handler.AppendFormatted(value);
    }

    public void AppendQuery(string key, ReadOnlySpan<char> value)
    {
        AppendKeyPart(key);
        _handler.AppendFormatted(value);
    }

    public void TryAppendQuery<T>(string key, T? nullableValue) where T : struct
    {
        if (nullableValue is { } value) {
            AppendQuery(key, value);
        }
    }

    public void TryAppendQuery<T>(string key, T? value) where T : class
    {
        if (value is not null) {
            AppendQuery(key, value);
        }
    }

    public void TryAppendQuery(string key, string? value)
    {
        if (value is not null) {
            AppendQuery(key, value);
        }
    }

    private void AppendKeyPart(string key)
    {
        if (_and) {
            _handler.AppendLiteral("&");
        }
        else {
            _handler.AppendLiteral("?");
            _and = true;
        }
        _handler.AppendLiteral(key);
        _handler.AppendLiteral("=");
    }

    public string Build() => _handler.ToStringAndClear();
}
