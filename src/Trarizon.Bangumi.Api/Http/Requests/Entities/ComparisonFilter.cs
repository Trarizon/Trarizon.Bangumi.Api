using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Utilities;
using Comparison = Trarizon.Bangumi.Api.Http.Requests.Entities.ComparisonFilterComparison;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
/// <summary>
/// 表示一个通过比较值过滤的过滤器
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="comparison"></param>
/// <param name="value"></param>
public readonly struct ComparisonFilter<T>(Comparison comparison, T value)
{
    /// <summary>
    /// 比较类型
    /// </summary>
    public readonly Comparison Comparison = comparison;
    /// <summary>
    /// 比较值
    /// </summary>
    public readonly T Value = value;

    /// <summary>
    /// 创建一个比较值相等的过滤器
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator ComparisonFilter<T>(T value) => new(Comparison.Equals, value);
}

/// <summary>
/// 比较类型
/// </summary>
public enum ComparisonFilterComparison
{
    /// <summary>
    /// 与指定值相等
    /// </summary>
    Equals,
    /// <summary>
    /// 大于指定值
    /// </summary>
    Greater,
    /// <summary>
    /// 大于等于指定值
    /// </summary>
    GreaterEquals,
    /// <summary>
    /// 小于指定值
    /// </summary>
    Less,
    /// <summary>
    /// 小于等于指定值
    /// </summary>
    LessEquals,
}

/// <summary>
/// 用于构造<see cref="ComparisonFilter{T}"/>的静态方法集合
/// </summary>
public static class ComparisonFilter
{
    /// <summary>
    /// 构造要求与指定值相等的过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ComparisonFilter<T> Equals<T>(T value) => new(Comparison.Equals, value);

    /// <summary>
    /// 构造要求大于指定值的过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ComparisonFilter<T> Greater<T>(T value) => new(Comparison.Greater, value);

    /// <summary>
    /// 构造要求大于等于指定值的过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ComparisonFilter<T> GreaterEquals<T>(T value) => new(Comparison.GreaterEquals, value);

    /// <summary>
    /// 构造要求小于指定值的过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ComparisonFilter<T> Less<T>(T value) => new(Comparison.Less, value);

    /// <summary>
    /// 构造要求小于等于指定值的过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ComparisonFilter<T> LessEquals<T>(T value) => new(Comparison.LessEquals, value);

    internal static string ToRequestJsonString(this Comparison comparison) => comparison switch
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

    /// <summary>
    /// 为IUtf8SpanParsable类型扩展从UTF8字符串解析过滤器的方法
    /// </summary>
    extension<T>(ComparisonFilter<T>) where T : IUtf8SpanParsable<T>
    {
        /// <summary>
        /// 从UTF8字符串解析
        /// </summary>
        /// <param name="utf8"></param>
        /// <param name="provider"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 从UTF8字符串解析
        /// </summary>
        /// <param name="utf8"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ComparisonFilter<T> Parse(ReadOnlySpan<byte> utf8, IFormatProvider? provider)
        {
            if (!ComparisonFilter<T>.TryParse(utf8, provider, out var filter)) {
                Throws.ThrowInvalidOperation<(int, Comparison)>("Invalid format of comparison filter string.");
            }
            return filter;
        }
    }

    /// <summary>
    /// 为ISpanParsable类型扩展从UTF8字符串解析过滤器的方法
    /// </summary>
    extension<T>(ComparisonFilter<T>) where T : ISpanParsable<T>
    {
        /// <summary>
        /// 从字符串解析
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 从字符串解析
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ComparisonFilter<T> Parse(ReadOnlySpan<char> text, IFormatProvider? provider)
        {
            if (!ComparisonFilter<T>.TryParse(text, provider, out var filter)) {
                Throws.ThrowInvalidOperation<(int, Comparison)>("Invalid format of comparison filter string.");
            }
            return filter;
        }
    }
}
