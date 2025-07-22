using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Api.Http.Requests;
// https://github.com/bangumi/server/blob/master/web/req/collection.go#L35
public sealed class UpdateUserCollectionSubjectRequestBody
{
    [JsonInclude, JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CollectionType? Type { get; set; }

    /// <summary>
    /// [0, 10], 0表示删除评分
    /// </summary>
    [JsonInclude, JsonPropertyName("rate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [GoSource<byte>]
    public int? Rate { get; set => field = value is { } num ? int.Clamp(num, 0, 10) : null; }

    [JsonInclude, JsonPropertyName("ep_status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [GoSource<uint>]
    public int? EpisodeStatus { get; set => field = value is { } num ? int.Max(num, 0) : null; }

    [JsonInclude, JsonPropertyName("vol_status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [GoSource<uint>]
    public int? VolumeStatus { get; set => field = value is { } num ? int.Max(num, 0) : null; }

    [JsonInclude, JsonPropertyName("comment"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Comment { get; set; }

    [JsonInclude, JsonPropertyName("private"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsPrivate { get; set; }

    /// <summary>
    /// 传 null 会被忽略，传 [] 则会删除所有 tag。
    /// <br/>
    /// 值不能包含空格
    /// </summary>
    [JsonInclude, JsonPropertyName("tags"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Tags { get; set; }

    public UpdateUserCollectionSubjectRequestBody Clone() => new()
    {
        Type = Type,
        Rate = Rate,
        EpisodeStatus = EpisodeStatus,
        VolumeStatus = VolumeStatus,
        Comment = Comment,
        Tags = Tags?.ToList(),
    };
}
