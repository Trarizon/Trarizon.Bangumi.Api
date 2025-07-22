using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Users;
public sealed class UserSelf : IUser
{
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

    [JsonInclude, JsonPropertyName("user_group")]
    public UserGroup UserGroup { get; internal set; }

    [JsonInclude, JsonPropertyName("avatar")]
    public Avatar Avatar { get; internal set; }

    [JsonInclude, JsonPropertyName("sign")]
    public string Sign { get; internal set; }

    [JsonInclude, JsonPropertyName("email")]
    public string Email { get; internal set; }

    [JsonInclude, JsonPropertyName("reg_time")]
    public DateTime RegisterTime { get; internal set; }

    [JsonInclude, JsonPropertyName("time_offset")]
    public int? TimeZoneOffset { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal UserSelf() { }
#pragma warning restore CS8618
}
