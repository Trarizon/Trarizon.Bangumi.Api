using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Api.Http.Requests;
// 
/// <summary>
/// 
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/req/collection.go#L35">
/// SubjectEpisodeCollectionPatch 
/// </see>
/// </remarks>
public sealed class UpdateUserCollectionSubjectRequestBody
{
    /// <inheritdoc cref="UserSubjectCollection.Type"/>
    [JsonInclude, JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SubjectCollectionType? Type { get; set; }

    // src: uint8
    /// <inheritdoc cref="UserSubjectCollection.Rate"/>
    /// <remarks>
    /// [0, 10]，越界自动clamp, 0表示删除评分
    /// </remarks>
    [JsonInclude, JsonPropertyName("rate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Rate { get; set => field = value is { } num ? int.Clamp(num, 0, 10) : null; }

    // src: uint32
    /// <inheritdoc cref="UserSubjectCollection.EpisodeStatus"/>
    [JsonInclude, JsonPropertyName("ep_status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EpisodeStatus { get; set => field = value is { } num ? int.Max(num, 0) : null; }

    // src: uint32
    /// <inheritdoc cref="UserSubjectCollection.VolumeStatus"/>
    [JsonInclude, JsonPropertyName("vol_status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? VolumeStatus { get; set => field = value is { } num ? int.Max(num, 0) : null; }

    /// <inheritdoc cref="UserSubjectCollection.Comment"/>
    [JsonInclude, JsonPropertyName("comment"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Comment { get; set; }

    /// <inheritdoc cref="UserSubjectCollection.IsPrivate"/>
    [JsonInclude, JsonPropertyName("private"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsPrivate { get; set; }

    /// <inheritdoc cref="UserSubjectCollection.Tags"/>
    /// <remarks>
    /// 传 null 会被忽略，传[] 则会删除所有 tag。
    /// <br/>
    /// 值不能包含空格
    /// </remarks>
    [JsonInclude, JsonPropertyName("tags"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Tags { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public UpdateUserCollectionSubjectRequestBody Clone() => new()
    {
        Type = Type,
        Rate = Rate,
        EpisodeStatus = EpisodeStatus,
        VolumeStatus = VolumeStatus,
        Comment = Comment,
        Tags = Tags?.ToList(),
    };

#pragma warning restore CS1591
}
