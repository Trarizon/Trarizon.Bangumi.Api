using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.Users;
/// <summary>
/// 用户
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/user.go#L53">
/// User
/// </see>
/// </remarks>
public sealed class User : IUser
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <summary>
    /// 用户唯一用户名，初始与 UID 相同，可修改一次
    /// </summary>
    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

    /// <summary>
    /// 用户组
    /// </summary>
    [JsonInclude, JsonPropertyName("user_group")]
    public UserGroup UserGroup { get; internal set; }

    /// <summary>
    /// 用户头像
    /// </summary>
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
