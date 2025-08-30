using Trarizon.Bangumi.Api.Models.UserModels;

namespace Trarizon.Bangumi.Api.Models.Abstractions;
/// <summary>
/// 用户
/// </summary>
public interface IUserIdentity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 用户名称
/// </summary>
public interface IUserNamed
{
    /// <summary>
    /// 用户唯一用户名，初始与 UID 相同，可修改一次
    /// </summary>
    string UserName { get; }
    /// <summary>
    /// 用户名
    /// </summary>
    string NickName { get; }
}

/// <summary>
/// 用户
/// </summary>
public interface IUser : IUserIdentity, IUserNamed
{
    /// <summary>
    /// 用户组
    /// </summary>
    UserGroup UserGroup { get; }
}

/// <summary>
/// 用户头像
/// </summary>
public interface IUserAvatarProvider
{
    /// <summary>
    /// 用户头像
    /// </summary>
    Avatar Avatar { get; }
}