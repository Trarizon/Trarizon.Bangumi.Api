using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Responses.Models.Abstractions;
/// <summary>
/// 角色
/// </summary>
public interface ICharacterIdentity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 角色
/// </summary>
public interface ICharacter : ICharacterIdentity
{
    /// <summary>
    /// 角色名称
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 角色类型
    /// </summary>
    CharacterType Type { get; }
}

/// <summary>
/// 角色图片
/// </summary>
public interface ICharacterImagesProvider
{
    /// <summary>
    /// 角色图片
    /// </summary>
    PersonImageSet Images { get; }
}