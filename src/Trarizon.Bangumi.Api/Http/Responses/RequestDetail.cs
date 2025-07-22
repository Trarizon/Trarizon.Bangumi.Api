using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Responses;
public sealed class RequestDetail
{
    [JsonInclude, JsonPropertyName("path")]
    public string? Path { get; internal set; }

    [JsonInclude, JsonPropertyName("method")]
    public string? Method { get; internal set; }

    [JsonConstructor]
    internal RequestDetail() { }

    public string ToDetailString() => $"{{ Path: '{Path}', Method: '{Method}' }}";
}
