using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.UserModels;
/// <summary>
/// 当前用户信息
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/handler/user/me.go#L29">
/// CurrentUser
/// </see>
/// </remarks>
public sealed class UserSelf : IUser, IUserAvatarProvider
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

    /// <inheritdoc cref="User.Signature" />
    [JsonInclude, JsonPropertyName("sign")]
    public string Signature { get; internal set; }

    /// <summary>
    /// 用户绑定的邮箱地址
    /// </summary>
    [JsonInclude, JsonPropertyName("email")]
    public string Email { get; internal set; }

    /// <summary>
    /// 用户注册时间
    /// </summary>
    [JsonInclude, JsonPropertyName("reg_time")]
    public DateTime RegisterTime { get; internal set; }

    // api: 源码非指针，scheme nullable
    /// <summary>
    /// 用户设置的时区偏移，以小时为单位。比如 GMT+8（shanghai/beijing）为 8
    /// </summary>
    [JsonInclude, JsonPropertyName("time_offset")]
    public int TimeZoneOffset { get; internal set; }

#pragma warning disable CS8618
    [JsonConstructor]
    internal UserSelf() { }
#pragma warning restore CS8618
}
