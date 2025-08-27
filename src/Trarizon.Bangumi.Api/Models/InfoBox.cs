using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Models;
/// <summary>
/// 信息列表
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L36">
/// V0wiki: []any
/// </see>
/// </remarks>
[JsonConverter(typeof(InfoBoxJsonConverter))]
[DebuggerDisplay("(Count = {Properties.Length})")]
public readonly struct InfoBox : IReadOnlyCollection<InfoProperty>, ICollection<InfoProperty>
{
    private readonly ImmutableArray<InfoProperty> _properties;

    /// <summary>
    /// 条目信息列表
    /// </summary>
    public ImmutableArray<InfoProperty> Properties => _properties;

    /// <summary>
    /// 属性数量
    /// </summary>
    public int Count => _properties.Length;

    /// <summary>
    /// 获取指定索引的属性键值对
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public InfoProperty this[int index] => _properties[index];

    /// <summary>
    /// 获取Key为指定值的属性值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public InfoValues this[ReadOnlySpan<char> key]
    {
        get {
            if (!TryGetValue(key, out var value))
                Throws.ThrowKeyNotFound(key.ToString(), nameof(InfoBox));
            return value;
        }
    }

    internal InfoBox(ImmutableArray<InfoProperty> properties)
    {
        _properties = properties;
    }

    /// <summary>
    /// 获取Key为指定值的属性值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValue(ReadOnlySpan<char> key, out InfoValues value)
    {
        foreach (var prop in _properties) {
            if (key.SequenceEqual(prop.Key)) {
                value = prop.Values;
                return true;
            }
        }
        value = default;
        return false;
    }

    /// <summary>
    /// 判断是否包含指定Key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey(ReadOnlySpan<char> key) => TryGetValue(key, out _);

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public ImmutableArray<InfoProperty>.Enumerator GetEnumerator() => _properties.GetEnumerator();

    bool ICollection<InfoProperty>.IsReadOnly => true;

    IEnumerator<InfoProperty> IEnumerable<InfoProperty>.GetEnumerator() => ((IEnumerable<InfoProperty>)_properties).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_properties).GetEnumerator();
    void ICollection<InfoProperty>.Add(InfoProperty item) => Throws.ThrowNotSupport();
    void ICollection<InfoProperty>.Clear() => Throws.ThrowNotSupport();
    bool ICollection<InfoProperty>.Contains(InfoProperty item) => _properties.Contains(item);
    void ICollection<InfoProperty>.CopyTo(InfoProperty[] array, int arrayIndex) => _properties.CopyTo(array, arrayIndex);
    bool ICollection<InfoProperty>.Remove(InfoProperty item) => Throws.ThrowNotSupport<bool>();

#pragma warning restore CS1591
}

// src: https://github.com/bangumi/server/blob/master/internal/pkg/compat/wiki.go#L21
/// <summary>
/// 信息键值对
/// </summary>
[DebuggerDisplay("{Key} : {Value}")]
public struct InfoProperty
{
    /// <summary>
    /// 信息的键
    /// </summary>
    [JsonInclude, JsonPropertyName("key")]
    public string Key { get; internal set; }

    /// <summary>
    /// 信息的值
    /// </summary>
    [JsonInclude, JsonPropertyName("value")]
    public InfoValues Values { get; internal set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public readonly void Deconstruct(out string key, out InfoValues value)
    {
        key = Key;
        value = Values;
    }

#pragma warning restore CS1591
}

/// <summary>
/// 信息的值，Union(string, InfoPairValue[])
/// </summary>
/// <remarks>
/// 值的原始形式可能为string或Pair[]，该结构体将其抽象为了统一的Pair列表，可以通过<see cref="IsRawValueString"/>, <see cref="GetRawStringValue"/>, <see cref="GetRawPairsValue"/>获取原始形式
/// <br/>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/pkg/compat/wiki.go#L54">wikiValue: key-value</see> ,
/// <see href="https://github.com/bangumi/server/blob/master/internal/pkg/compat/wiki.go#L59">wikiValues: key-values</see>
/// </remarks>
[JsonConverter(typeof(InfoValuesJsonConverter))]
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public readonly struct InfoValues : ICollection<InfoValue>, IReadOnlyCollection<InfoValue>
{
    private readonly object _obj;

    /// <summary>
    /// 获取键值对
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public InfoValue this[int index]
    {
        get {
            if (_obj is string str) {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, 1);
                return new InfoValue(null, str);
            }
            return Unsafe.As<InfoValue[]>(_obj)[index];
        }
    }

    /// <summary>
    /// 值的数量
    /// </summary>
    public int Count => _obj is string ? 1 : Unsafe.As<InfoValue[]>(_obj).Length;

    /// <summary>
    /// 值，若含多个键值对，返回第一对的值
    /// </summary>
    public string Value => _obj is string str ? str : Unsafe.As<InfoValue[]>(_obj)[0].Value;

    private string DebuggerDisplay => IsRawValueString() ? $"\"{GetRawStringValue()}\"" : $"(Count = {Count})";

    internal InfoValues(string value)
    {
        _obj = value;
    }

    internal InfoValues(ImmutableArray<InfoValue> pairs)
    {
        _obj = ImmutableCollectionsMarshal.AsArray(pairs) ?? [];
    }

    /// <summary>
    /// 判断原始值是否为字符串
    /// </summary>
    /// <returns></returns>
    public bool IsRawValueString() => _obj is string;

    /// <summary>
    /// 原始值为字符串时返回字符串值，否则抛出cast异常
    /// </summary>
    /// <returns></returns>
    public string GetRawStringValue() => (string)_obj;

    /// <summary>
    /// 原始值为集合时返回集合值，否则抛出cast异常
    /// </summary>
    /// <returns></returns>
    public ImmutableArray<InfoValue> GetRawPairsValue() => ImmutableCollectionsMarshal.AsImmutableArray((InfoValue[])_obj);

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public readonly Enumerator GetEnumerator() => new Enumerator(this);

    bool ICollection<InfoValue>.IsReadOnly => true;
    void ICollection<InfoValue>.Add(InfoValue item) => Throws.ThrowNotSupport();
    void ICollection<InfoValue>.Clear() => Throws.ThrowNotSupport();
    bool ICollection<InfoValue>.Contains(InfoValue item)
    {
        if (_obj is string str) {
            return item.Key is null && item.Value == str;
        }
        else {
            return Unsafe.As<InfoValue[]>(_obj).Contains(item);
        }
    }
    void ICollection<InfoValue>.CopyTo(InfoValue[] array, int arrayIndex)
    {
        if (_obj is string str) {
            array[arrayIndex] = new InfoValue(null, str);
        }
        else {
            Unsafe.As<InfoValue[]>(_obj).CopyTo(array, arrayIndex);
        }
    }
    bool ICollection<InfoValue>.Remove(InfoValue item) => Throws.ThrowNotSupport<bool>();
    IEnumerator<InfoValue> IEnumerable<InfoValue>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<InfoValue>
    {
        private readonly object _obj;
        private int _index;
        private InfoValue _current;

        internal Enumerator(InfoValues values)
        {
            _obj = values._obj;
            if (_obj is string)
                _index = -1;
            else
                _index = 0;
        }

        public readonly InfoValue Current => _current;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index == -2) {
                return false;
            }
            if (_index == -1) {
                Debug.Assert(_obj is string);
                _current = new InfoValue(null, Unsafe.As<string>(_obj));
                _index = -2;
                return true;
            }

            Debug.Assert(_obj is InfoValue[]);
            var index = _index;
            var array = Unsafe.As<InfoValue[]>(_obj);
            if (index < array.Length) {
                _current = array[index];
                _index = index + 1;
                return true;
            }
            else {
                _index = -2;
                return false;
            }
        }

        void IDisposable.Dispose() { }
        void IEnumerator.Reset()
        {
            if (_obj is string)
                _index = -1;
            else
                _index = 0;
        }
    }

#pragma warning restore CS1591
}

/// <summary>
/// <see cref="InfoValues"/>为集合时的元素键值对
/// </summary>
[DebuggerDisplay("{Key} : {Value}")]
public struct InfoValue : IEquatable<InfoValue>
{
    /// <summary>
    /// 键
    /// </summary>
    [JsonInclude, JsonPropertyName("k"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Key { get; internal set; }

    /// <summary>
    /// 值
    /// </summary>
    [JsonInclude, JsonPropertyName("v")]
    public string Value { get; internal set; }

    /// <summary>
    /// 构造InfoBox的值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public InfoValue(string? key, string value)
    {
        Key = key;
        Value = value;
    }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public readonly void Deconstruct(out string? key, out string value)
    {
        key = Key;
        value = Value;
    }

    public readonly bool Equals(InfoValue other) => Key == other.Key && Value == other.Value;
    public readonly override bool Equals(object? obj) => obj is InfoValue value && Equals(value);
    public static bool operator ==(InfoValue left, InfoValue right) => left.Equals(right);
    public static bool operator !=(InfoValue left, InfoValue right) => !(left == right);
    public readonly override int GetHashCode() => Key is null ? Value.GetHashCode() : HashCode.Combine(Key, Value);

#pragma warning restore CS1591
}
