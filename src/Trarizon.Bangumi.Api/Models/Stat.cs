using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models;
// https://github.com/bangumi/server/blob/master/web/res/common.go#L17
public struct Stat
{
    [JsonInclude, JsonPropertyName("comments")]
    [GoSource<uint>]
    public int CommentCount { get; internal set; }

    [JsonInclude, JsonPropertyName("collects")]
    [GoSource<uint>]
    public int CollectCount { get; internal set; }
}
