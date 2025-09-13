using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models.Collections;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// 
/// </summary>
public sealed class UpdateUserSubjectEpisodeCollectionsRequestBody
{
    /// <summary>
    /// 更新的章节ID
    /// </summary>
    [JsonInclude, JsonPropertyName("episode_id")]
    public required List<uint> EpisodeIds { get; set; }

    /// <inheritdoc cref="UserEpisodeCollection.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public required EpisodeCollectionType Type { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public UpdateUserSubjectEpisodeCollectionsRequestBody Clone() => new()
    {
        EpisodeIds = EpisodeIds.ToList(),
        Type = Type
    };

#pragma warning restore CS1591
}
