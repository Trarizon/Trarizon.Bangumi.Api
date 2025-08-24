using System.Text.Json;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Serialization.Converters;
internal class ListJsonConverter<T, TConverter> : JsonConverter<List<T>>
    where TConverter : JsonConverter<T>, IConstructable<TConverter>
{
    private readonly TConverter _converter = TConverter.Construct();

    public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Throws.ThrowIfUnexpectedJsonToken(reader.TokenType, JsonTokenType.StartArray);

        var list = new List<T>();

        while (reader.Read()) {
            var tokenType = reader.TokenType;
            if (tokenType is JsonTokenType.EndArray)
                break;
            var item = _converter.Read(ref reader, typeToConvert, options);
            list.Add(item!);
        }
        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value) {
            _converter.Write(writer, item, options);
        }
        writer.WriteEndArray();
    }
}
