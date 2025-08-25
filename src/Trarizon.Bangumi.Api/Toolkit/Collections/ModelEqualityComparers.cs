using System.Diagnostics.CodeAnalysis;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// 提供了一系列Model类型的比较器
/// </summary>
public static class ModelEqualityComparers
{
    /// <summary>
    /// 通过Id进行比较
    /// </summary>
    public static IEqualityComparer<ICharacter> Id { get; }

    private sealed class IdComparer : IEqualityComparer<ICharacter>
    {
        public bool Equals(ICharacter? x, ICharacter? y) => x?.Id == y?.Id;
        public int GetHashCode([DisallowNull] ICharacter obj) => obj.Id.GetHashCode();
    }
}
