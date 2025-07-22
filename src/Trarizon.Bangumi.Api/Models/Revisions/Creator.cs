using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
public sealed class Creator
{
    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal Creator() { }
#pragma warning restore CS8618     
}
