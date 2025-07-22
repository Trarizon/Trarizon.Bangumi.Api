using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests.Entities;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Http;
internal abstract class ComparisonFilterJsonConverter<T> : JsonConverter<ComparisonFilter<T>>
{
    private readonly string? _format;
    private readonly IFormatProvider? _formatProvider;

    public ComparisonFilterJsonConverter(string? format, IFormatProvider? formatProvider)
    {
        _format = format;
        _formatProvider = formatProvider;
    }

    public ComparisonFilterJsonConverter() { }

    protected abstract ComparisonFilter<T> Parse(ReadOnlySpan<byte> utf8, IFormatProvider? formatProvider);

    public override ComparisonFilter<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.String);
        return Parse(reader.ValueSpan, _formatProvider);
    }

    public override void Write(Utf8JsonWriter writer, ComparisonFilter<T> value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToRequestJsonString());
    }
}

internal class ComparisonFilterUtf8JsonConverter<T> : ComparisonFilterJsonConverter<T>
    , IConstructable<ComparisonFilterUtf8JsonConverter<T>>
    where T : IUtf8SpanParsable<T>
{
    static ComparisonFilterUtf8JsonConverter<T> IConstructable<ComparisonFilterUtf8JsonConverter<T>>.Construct() => new();

    protected override ComparisonFilter<T> Parse(ReadOnlySpan<byte> utf8, IFormatProvider? formatProvider)
        => ComparisonFilter<T>.Parse(utf8, formatProvider);
}

internal class ComparisonFilterUtf16JsonConverter<T>(string? format, IFormatProvider? formatProvider) : ComparisonFilterJsonConverter<T>(format, formatProvider)
    where T : ISpanParsable<T>
{
    protected override ComparisonFilter<T> Parse(ReadOnlySpan<byte> utf8, IFormatProvider? formatProvider)
    {
        var charLength = Encoding.UTF8.GetMaxCharCount(utf8.Length);
        char[]? rentedArray = null;
        var chars = charLength < 256
            ? stackalloc char[charLength]
            : (rentedArray = ArrayPool<char>.Shared.Rent(charLength));

        try {
            return ComparisonFilter<T>.Parse(chars, formatProvider);
        }
        finally {
            if (rentedArray is not null) {
                ArrayPool<char>.Shared.Return(rentedArray);
            }
        }
    }
}

internal sealed class ComparisonFilterDateOnlyJsonConverter()
    : ComparisonFilterUtf16JsonConverter<DateOnly>("YYYY-MM-DD", null)
    , IConstructable<ComparisonFilterDateOnlyJsonConverter>
{
    static ComparisonFilterDateOnlyJsonConverter IConstructable<ComparisonFilterDateOnlyJsonConverter>.Construct() => new();
}