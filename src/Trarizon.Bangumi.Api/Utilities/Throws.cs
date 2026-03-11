using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Utilities;
internal static class Throws
{
    [DoesNotReturn]
    public static T ThrowNotSupport<T>()
        => throw new NotSupportedException();

    [DoesNotReturn]
    public static void ThrowNotSupport()
        => throw new NotSupportedException();

    [DoesNotReturn]
    public static void ThrowArgumentOutOfRange(string? paramName) => throw new ArgumentOutOfRangeException(paramName);

    [DoesNotReturn]
    public static void ThrowInvalidOperation(string? message = null)
        => throw new InvalidOperationException(message);

    [DoesNotReturn]
    public static T ThrowInvalidOperation<T>(string? message = null)
        => throw new InvalidOperationException(message);

    [DoesNotReturn]
    public static void ThrowKeyNotFound(object key, string collectionName = "collection")
        => throw new KeyNotFoundException($"The {collectionName} doesn't contains key '{key}'.");

    [DoesNotReturn]
    public static T ThrowUnknownEnumValue<T>(Enum value)
        => throw new InvalidOperationException("Unknown enum value.");

    public static T ThrowUnknownEnumCastValue<T>(object obj) where T : struct, Enum
        => throw new InvalidOperationException($"Cannot cast '{obj}' to enum {typeof(T)}.");

    [DoesNotReturn]
    public static void ThrowUnexpectedJsonToken()
        => throw new InvalidOperationException("Unexpected json token type while reading.");

    [DoesNotReturn]
    public static void ThrowUnexpectedJsonToken(JsonTokenType actual, params ReadOnlySpan<JsonTokenType> expecteds)
    {
        ThrowInvalidOperation(expecteds.Length switch
        {
            0 => $"Read unexpected json token type. Actual: {actual}.",
            1 => $"Read unexpected json token type. Actual: {actual}; Expected: {expecteds[0]}.",
            _ => BuildOneOf(actual, expecteds),
        });

        static string BuildOneOf(JsonTokenType actual, ReadOnlySpan<JsonTokenType> expecteds)
        {
            var bfr = (stackalloc char[41 + 21 + expecteds.Length * 20]);
            var builder = new DefaultInterpolatedStringHandler(41 + 21 + 3 - 2 + expecteds.Length * 2, expecteds.Length + 1, null, bfr);
            builder.AppendLiteral("Read unexpected json token type. Actual: ");
            builder.AppendFormatted(actual);
            builder.AppendLiteral("; Expected: One of [");
            foreach (var item in expecteds[..^1]) {
                builder.AppendFormatted(item);
                builder.AppendFormatted(", ");
            }
            builder.AppendFormatted(expecteds[^1]);
            builder.AppendLiteral("].");
            return builder.ToString();
        }
    }

    public static void ThrowIfUnexpectedJsonToken(JsonTokenType actual, JsonTokenType expected)
    {
        if (actual != expected)
            ThrowUnexpectedJsonToken(actual, expected);
    }

    public static void ThrowIfUnexpectedJsonToken(JsonTokenType actual, params ReadOnlySpan<JsonTokenType> expecteds)
    {
        foreach (var item in expecteds) {
            if (actual == item)
                return;
        }
        ThrowUnexpectedJsonToken(actual, expecteds);
    }
}
