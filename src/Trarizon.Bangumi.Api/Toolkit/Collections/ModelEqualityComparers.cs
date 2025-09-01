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
    public static IdComparer Id => IdComparer.Instance;

    /// <summary>
    /// 通过UserName进行比较
    /// </summary>
    public static IEqualityComparer<IUserNamed> UserName => UserNameComparer.Instance;

    /// <summary>
    /// 通过Id进行比较
    /// </summary>
    public sealed class IdComparer
        : IEqualityComparer<ICharacterIdentity>
        , IEqualityComparer<IEpisodeIdentity>
        , IEqualityComparer<IIndexIdentity>
        , IEqualityComparer<IPersonIdentity>
        , IEqualityComparer<IRevisionIdentity>
        , IEqualityComparer<ISubjectIdentity>
        , IEqualityComparer<IUserIdentity>
    {
        internal static IdComparer Instance { get; } = new IdComparer();

        private IdComparer() { }

        /// <inheritdoc/>
        public bool Equals(ICharacterIdentity? x, ICharacterIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] ICharacterIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(IEpisodeIdentity? x, IEpisodeIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] IEpisodeIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(IIndexIdentity? x, IIndexIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] IIndexIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(IPersonIdentity? x, IPersonIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] IPersonIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(IRevisionIdentity? x, IRevisionIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] IRevisionIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(ISubjectIdentity? x, ISubjectIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] ISubjectIdentity obj) => obj.Id.GetHashCode();
        /// <inheritdoc/>
        public bool Equals(IUserIdentity? x, IUserIdentity? y) => x?.Id == y?.Id;
        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] IUserIdentity obj) => obj.Id.GetHashCode();
    }

    private sealed class UserNameComparer : IEqualityComparer<IUserNamed>
    {
        public static UserNameComparer Instance { get; } = new UserNameComparer();

        public bool Equals(IUserNamed? x, IUserNamed? y) => x?.UserName == y?.UserName;
        public int GetHashCode([DisallowNull] IUserNamed obj) => obj.UserName.GetHashCode();
    }
}
