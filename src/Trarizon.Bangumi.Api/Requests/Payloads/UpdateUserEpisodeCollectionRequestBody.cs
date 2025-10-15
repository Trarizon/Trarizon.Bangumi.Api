using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Responses.Models.Collections;

namespace Trarizon.Bangumi.Api.Requests.Payloads;
/// <summary>
/// 
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/req/collection.go#L87">
/// UpdateUserEpisodeCollection 
/// </see>
/// </remarks>
public sealed class UpdateUserEpisodeCollectionRequestBody
{
    /// <inheritdoc cref="UserEpisodeCollection.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public required EpisodeCollectionType Type { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public UpdateUserEpisodeCollectionRequestBody Clone() => new()
    {
        Type = Type,
    };

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
