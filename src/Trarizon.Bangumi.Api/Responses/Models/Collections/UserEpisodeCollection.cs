using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Serialization.Converters;

namespace Trarizon.Bangumi.Api.Responses.Models.Collections;
/// <summary>
/// 用户的章节收藏
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/handler/user/get_episode_collection.go#L34">
/// ResUserEpisodeCollection
/// </see>
/// </remarks>
public sealed class UserEpisodeCollection
{
    /// <summary>
    /// 章节信息
    /// </summary>
    [JsonInclude, JsonPropertyName("episode")]
    public Episode Episode { get; internal set; }

    /// <summary>
    /// 章节收藏类型
    /// </summary>
    [JsonInclude, JsonPropertyName("type")]
    public EpisodeCollectionType Type { get; internal set; }

    // src: int64，表示unix时间戳
    /// <summary>
    /// 收藏更新时间，未知或未记录时值为<see cref="DateTimeOffset.UnixEpoch"/>
    /// </summary>
    [JsonInclude, JsonPropertyName("updated_at")]
    [JsonConverter(typeof(DateTimeOffsetFromUnixTimeSecondsJsonConverter))]
    public DateTimeOffset UpdateTime { get; internal set; }

#pragma warning disable CS8618          
    [JsonConstructor]
    internal UserEpisodeCollection() { }
#pragma warning restore CS8618          
}
