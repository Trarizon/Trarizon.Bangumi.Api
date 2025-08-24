using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using SubjectModel = Trarizon.Bangumi.Api.Models.SubjectModels.Subject;

namespace Trarizon.Bangumi.Api.Models.UserModels;
// UserSubjectCollection
// https://github.com/bangumi/server/blob/master/web/res/collection.go#L25
/// <summary>
/// 用户的条目收藏
/// </summary>
public sealed class UserSubjectCollection
{
    /// <inheritdoc cref="ISubject.Id" />
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    /// <inheritdoc cref="SubjectModel.Type" />
    [JsonInclude, JsonPropertyName("subject_type")]
    public SubjectType SubjectType { get; internal set; }

    // src: uint8
    /// <summary>
    /// 用户打分
    /// </summary>
    [JsonInclude, JsonPropertyName("rate")]
    public int Rate { get; internal set; }

    /// <summary>
    /// 收藏类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectCollectionType Type { get; internal set; }

    /// <summary>
    /// 用户评论
    /// </summary>
    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; internal set; }

    /// <summary>
    /// 用户标记标签
    /// </summary>
    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<string> Tags { get; internal set; }

    // src: uint32
    /// <summary>
    /// 章节观看进度
    /// </summary>
    [JsonInclude, JsonPropertyName("ep_status")]
    public int EpisodeStatus { get; internal set; }

    // src: uint32
    /// <summary>
    /// 卷观看进度
    /// </summary>
    [JsonInclude, JsonPropertyName("vol_status")]
    public int VolumeStatus { get; internal set; }

    /// <summary>
    /// 收藏信息更新时间
    /// </summary>
    /// <remarks>
    /// 本时间并不代表条目的收藏时间。修改评分，评价，章节观看状态等收藏信息时未更新此时间是一个 bug。请不要依赖此特性
    /// </remarks>
    [JsonInclude, JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedTime { get; internal set; }

    /// <summary>
    /// 收藏是否仅自己可见
    /// </summary>
    [JsonInclude, JsonPropertyName("private")]
    public bool IsPrivate { get; internal set; }

    // api: 源码非指针但scheme nullable
    /// <summary>
    /// 条目信息
    /// </summary>
    [JsonInclude, JsonPropertyName("subject")]
    public SlimSubject Subject { get; internal set; } 

#pragma warning disable CS8618          
    [JsonConstructor]
    internal UserSubjectCollection() { }
#pragma warning restore CS8618          
}
