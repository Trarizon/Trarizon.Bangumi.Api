using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests.Entities;

namespace Trarizon.Bangumi.Api.Http.Requests;
public sealed class SearchCharactersFilter
{
    [JsonInclude, JsonPropertyName("nsfw")]
    public NsfwFilter Nsfw { get; set; }

    public SearchCharactersFilter Clone() => new()
    {
        Nsfw = Nsfw,
    };
}
