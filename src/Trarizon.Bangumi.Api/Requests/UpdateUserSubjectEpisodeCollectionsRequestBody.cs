using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.UserModels;

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
    public required List<int> Episodes { get; set; }

    /// <inheritdoc cref="UserEpisodeCollection.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public required EpisodeCollectionType Type { get; set; }
}
