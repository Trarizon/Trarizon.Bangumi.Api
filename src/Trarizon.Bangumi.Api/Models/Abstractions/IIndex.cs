namespace Trarizon.Bangumi.Api.Models.Abstractions;

/// <summary>
/// 目录
/// </summary>
public interface IIndexIdentity
{
    /// <summary>
    /// 目录ID
    /// </summary>
    public uint Id { get;  }
}

// 目录model目前只有一个，暂时不写具体接口
