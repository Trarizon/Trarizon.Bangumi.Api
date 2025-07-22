using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class UpdateIndexInfoRequestBody
{
    [JsonInclude, JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonInclude, JsonPropertyName("description")]
    public string? Description { get; set; }

    public UpdateIndexInfoRequestBody Clone() => new()
    {
        Title = Title,
        Description = Description,
    };
}
