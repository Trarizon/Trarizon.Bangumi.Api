using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.Abstractions;
/// <summary>
/// 条目
/// </summary>
public interface ISubject
{
    /// <summary>
    /// 条目ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 条目基本信息
/// </summary>
public interface ISubjectBasicInfo
{
    /// <inheritdoc cref="Subject.ChineseName"/>
    string ChineseName { get; }

    /// <inheritdoc cref="Subject.Name"/>
    string Name { get; }

    /// <inheritdoc cref="Subject.Type"/>
    SubjectType Type { get; }
}
