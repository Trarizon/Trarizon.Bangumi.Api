using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Users;
// https://github.com/bangumi/server/blob/master/web/res/user.go#L22
public struct Avatar
{
    [JsonInclude, JsonPropertyName("large")]
    public string Large { get; internal set; }

    [JsonInclude, JsonPropertyName("medium")]
    public string Medium { get; internal set; }

    [JsonInclude, JsonPropertyName("small")]
    public string Small { get; internal set; }
}