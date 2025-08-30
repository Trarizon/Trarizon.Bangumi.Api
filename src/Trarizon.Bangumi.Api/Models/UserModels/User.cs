using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.UserModels;
/// <summary>
/// 用户
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/user.go#L53">
/// User
/// </see>
/// </remarks>
public sealed class User : IUser, IUserAvatarProvider
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("user_group")]
    public UserGroup UserGroup { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("avatar")]
    public Avatar Avatar { get; internal set; }

    /// <summary>
    /// 用户个人签名
    /// </summary>
    [JsonInclude, JsonPropertyName("sign")]
    public string Signature { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal User() { }
#pragma warning restore CS8618
}
