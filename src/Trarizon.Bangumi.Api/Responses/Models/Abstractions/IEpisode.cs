namespace Trarizon.Bangumi.Api.Responses.Models.Abstractions;

/// <summary>
/// 章节
/// </summary>
public interface IEpisodeIdentity
{
    /// <summary>
    /// 章节ID
    /// </summary>
    uint Id { get; }
}

// 章节model目前只有一个，暂时不写具体接口
