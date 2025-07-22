using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Utilities;
using Comparison = Trarizon.Bangumi.Api.Http.Requests.Entities.ComparisonFilterComparison;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
public readonly struct ComparisonFilter<T>(Comparison comparison, T value)
{
    public readonly Comparison Comparison = comparison;
    public readonly T Value = value;

    public static implicit operator ComparisonFilter<T>(T value) => new(Comparison.Equals, value);
}

public enum ComparisonFilterComparison
{
    Equals,
    Greater,
    GreaterEquals,
    Less,
    LessEquals,
}

public static class ComparisonFilter
{
    public static ComparisonFilter<T> Equals<T>(T value) => new(Comparison.Equals, value);
    public static ComparisonFilter<T> Greater<T>(T value) => new(Comparison.Greater, value);
    public static ComparisonFilter<T> GreaterEquals<T>(T value) => new(Comparison.GreaterEquals, value);
    public static ComparisonFilter<T> Less<T>(T value) => new(Comparison.Less, value);
    public static ComparisonFilter<T> LessEquals<T>(T value) => new(Comparison.LessEquals, value);

    public static string ToRequestJsonString(this Comparison comparison) => comparison switch
    {
        ComparisonFilterComparison.Equals => "=",
        ComparisonFilterComparison.Greater => ">",
        ComparisonFilterComparison.GreaterEquals => ">=",
        ComparisonFilterComparison.Less => "<",
        ComparisonFilterComparison.LessEquals => "<=",
        _ => Throws.ThrowUnknownEnumValue<string>(comparison),
    };

    internal static string ToRequestJsonString<T>(this ComparisonFilter<T> filter, string? format = null, IFormatProvider? formatProvider = null)
    {
        // DateOnly: YYYY-MM-DD, 10 length
        // int: -2147483648, 11 length
        var sb = new DefaultInterpolatedStringHandler(0, 0, formatProvider, stackalloc char[2 + 11]);
        sb.AppendLiteral(filter.Comparison.ToRequestJsonString());
        sb.AppendFormatted(filter.Value, format);
        return sb.ToStringAndClear();
    }

    extension<T>(ComparisonFilter<T>) where T : IUtf8SpanParsable<T>
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8, IFormatProvider? provider, out ComparisonFilter<T> filter)
        {
            (int len, Comparison comp) = utf8 switch
            {
                [(byte)'>', (byte)'=', ..] => (2, Comparison.GreaterEquals),
                [(byte)'>', ..] => (1, Comparison.Greater),
                [(byte)'<', (byte)'=', ..] => (2, Comparison.LessEquals),
                [(byte)'<', ..] => (1, Comparison.Less),
                _ => (-1, default),
            };

            if (len == -1) {
                filter = default;
                return false;
            }

            while (len < utf8.Length && char.IsWhiteSpace((char)utf8[len])) {
                len++;
            }
            var rest = utf8[len..];

            if (T.TryParse(rest, provider, out var val)) {
                filter = new ComparisonFilter<T>(comp, val);
                return true;
            }
            filter = default;
            return false;
        }

        public static ComparisonFilter<T> Parse(ReadOnlySpan<byte> utf8, IFormatProvider? provider)
        {
            if (!ComparisonFilter<T>.TryParse(utf8, provider, out var filter)) {
                Throws.ThrowInvalidOperation<(int, Comparison)>("Invalid format of comparison filter string.");
            }
            return filter;
        }
    }

    extension<T>(ComparisonFilter<T>) where T : ISpanParsable<T>
    {
        public static bool TryParse(ReadOnlySpan<char> text, IFormatProvider? provider, out ComparisonFilter<T> filter)
        {
            (int len, Comparison comp) = text switch
            {
                ['>', '=', ..] => (2, Comparison.GreaterEquals),
                ['>', ..] => (1, Comparison.Greater),
                ['<', '=', ..] => (2, Comparison.LessEquals),
                ['<', ..] => (1, Comparison.Less),
                _ => (-1, default),
            };

            if (len == -1) {
                filter = default;
                return false;
            }

            while (len < text.Length && char.IsWhiteSpace(text[len])) {
                len++;
            }
            var rest = text[len..];

            if (T.TryParse(rest, provider, out var val)) {
                filter = new ComparisonFilter<T>(comp, val);
                return true;
            }
            filter = default;
            return false;
        }

        public static ComparisonFilter<T> Parse(ReadOnlySpan<char> text, IFormatProvider? provider)
        {
            if (!ComparisonFilter<T>.TryParse(text, provider, out var filter)) {
                Throws.ThrowInvalidOperation<(int, Comparison)>("Invalid format of comparison filter string.");
            }
            return filter;
        }
    }
}
