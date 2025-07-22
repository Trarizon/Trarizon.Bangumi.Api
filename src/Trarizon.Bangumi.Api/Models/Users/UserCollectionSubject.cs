using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Attributes;
using Trarizon.Bangumi.Api.Models.Subjects;

namespace Trarizon.Bangumi.Api.Models.Users;
// UserSubjectCollection
// https://github.com/bangumi/server/blob/master/web/res/collection.go#L25
public sealed class UserCollectionSubject
{
    [JsonInclude, JsonPropertyName("subject_id")]
    public uint SubjectId { get; internal set; }

    [JsonInclude, JsonPropertyName("subject_type")]
    public SubjectType SubjectType { get; internal set; }

    [JsonInclude, JsonPropertyName("rate")]
    [GoSource<byte>]
    public int Rate { get; internal set; }

    [JsonInclude, JsonPropertyName("type")]
    public CollectionType Type { get; internal set; }

    [JsonInclude, JsonPropertyName("comment")]
    public string? Comment { get; internal set; }

    [JsonInclude, JsonPropertyName("tags")]
    public ImmutableArray<string> Tags { get; internal set; }

    [JsonInclude, JsonPropertyName("ep_status")]
    [GoSource<uint>]
    public int EpisodeStatus { get; internal set; }

    [JsonInclude, JsonPropertyName("vol_status")]
    [GoSource<uint>]
    public int VolumeStatus { get; internal set; }

    [JsonInclude, JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedTime { get; internal set; }

    [JsonInclude, JsonPropertyName("private")]
    public bool IsPrivate { get; internal set; }

    // 源码非指针但scheme nullable，我看不懂go，有问题再说吧
    [JsonInclude, JsonPropertyName("subject")]
    public SubjectSlim Subject { get; internal set; } 

#pragma warning disable CS8618          
    [JsonConstructor]
    internal UserCollectionSubject() { }
#pragma warning restore CS8618          
}
