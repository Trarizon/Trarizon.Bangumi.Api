using System.Collections.Immutable;
using Trarizon.Bangumi.Api.Responses.Models;

namespace Trarizon.Bangumi.Api.Responses.Models.Abstractions;
/// <summary>
/// 人物
/// </summary>
public interface IPersonIdentity
{
    /// <summary>
    /// 人物ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 人物
/// </summary>
public interface IPerson : IPersonIdentity
{
    /// <summary>
    /// 人物名称
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 人物类型
    /// </summary>
    PersonType Type { get; }
}

/// <summary>
/// 人物图片
/// </summary>
public interface IPersonImagesProvider
{
    /// <summary>
    /// 人物图片
    /// </summary>
    PersonImageSet Images { get; }
}

/// <summary>
/// 人物职业
/// </summary>
public interface IPersonCareersProvider
{
    /// <summary>
    /// 人物职业
    /// </summary>
    ImmutableArray<PersonCareer> Careers { get; }
}