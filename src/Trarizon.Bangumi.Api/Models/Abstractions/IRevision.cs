using Trarizon.Bangumi.Api.Models.RevisionModels;

namespace Trarizon.Bangumi.Api.Models.Abstractions;
/// <summary>
/// 编辑历史记录
/// </summary>
public interface IRevisionIdentity
{
    /// <summary>
    /// 编辑历史记录ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 编辑历史记录
/// </summary>
public interface IRevision : IRevisionIdentity
{
    /// <summary>
    /// 编辑记录类型
    /// </summary>
    RevisionType Type { get; }
    /// <summary>
    /// 编辑记录创建者
    /// </summary>
    Creator Creator { get; }
    /// <summary>
    /// 编辑记录摘要
    /// </summary>
    string Summary { get; }
    /// <summary>
    /// 编辑记录创建时间
    /// </summary>
    DateTimeOffset CreatedTime { get; }
}