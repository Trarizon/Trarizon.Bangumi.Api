using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Trarizon.Bangumi.Api.Internal.Generators.Utilities.Collections;
internal static class SequenceEquatableImmutableArray
{
    public static SequenceEquatableImmutableArray<T> ToSequenceEquatableImmutableArray<T>(this IEnumerable<T> source) 
        => new SequenceEquatableImmutableArray<T>(source.ToImmutableArray());
}
internal readonly struct SequenceEquatableImmutableArray<T>(ImmutableArray<T> arr) : IEquatable<SequenceEquatableImmutableArray<T>>
{
    public ImmutableArray<T> Array { get; } = arr;

    public bool Equals(SequenceEquatableImmutableArray<T> other)
    {
        if (Array.Length != other.Array.Length)
            return false;

        for (int i = 0; i < Array.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(Array[i], other.Array[i]))
                return false;
        }
        return true;
    }

    public override bool Equals(object? obj) => obj is SequenceEquatableImmutableArray<T> other && Equals(other);

    public override int GetHashCode()
    {
        unchecked {
            int hash = 17;
            foreach (var item in Array) {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }

    public static bool operator ==(SequenceEquatableImmutableArray<T> left, SequenceEquatableImmutableArray<T> right)
        => left.Equals(right);

    public static bool operator !=(SequenceEquatableImmutableArray<T> left, SequenceEquatableImmutableArray<T> right)
        => !left.Equals(right);

    public static implicit operator SequenceEquatableImmutableArray<T>(ImmutableArray<T> arr) => new SequenceEquatableImmutableArray<T>(arr);
}
