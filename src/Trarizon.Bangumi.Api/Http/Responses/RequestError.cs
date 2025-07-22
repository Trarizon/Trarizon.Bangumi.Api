using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Http.Responses;
public sealed class RequestError
{
    [JsonInclude, JsonPropertyName("title")]
    public string Title { get; internal set; }

    [JsonInclude, JsonPropertyName("details")]
    public RequestDetailUnion Details { get; internal set; }
    
    //public string RequestId { get; internal set; }
    
    [JsonInclude, JsonPropertyName("description")]
    public string Description { get; internal set; }

#pragma warning disable CS8618 
    [JsonConstructor]
    internal RequestError() { }
#pragma warning restore CS8618 
}
