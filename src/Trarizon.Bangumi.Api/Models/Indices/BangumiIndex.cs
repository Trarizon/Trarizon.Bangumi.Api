using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.Revisions;

namespace Trarizon.Bangumi.Api.Models.Indices;
// 'Index' is widely used in BCL, so we name it as 'BangumiIndex'.
// But for other types, we still use `Index` as base name.
// https://github.com/bangumi/server/blob/master/web/res/index.go
public sealed class BangumiIndex : IIndex
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("title")]
    public string Title { get; internal set; }

    [JsonInclude, JsonPropertyName("desc")]
    public string Description { get; internal set; }

    [JsonInclude, JsonPropertyName("total")]
    public int SubjectCount { get; internal set; }

    [JsonInclude, JsonPropertyName("stat")]
    public Stat Stat { get; internal set; }

    [JsonInclude, JsonPropertyName("created_at")]
    public DateTimeOffset CreatedTime { get; internal set; }

    [JsonInclude, JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedTime { get; internal set; }

    [JsonInclude, JsonPropertyName("creator")]
    public Creator Creator { get; internal set; }

    [JsonInclude, JsonPropertyName("nsfw")]
    public bool IsNsfw { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal BangumiIndex() { }
#pragma warning restore CS8618
}
