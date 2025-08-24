using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Http;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Requests.Models;
/// <summary>
/// R18内容筛选条件
/// </summary>
[JsonConverter(typeof(NsfwFilterJsonConverter))]
public enum NsfwFilter : byte
{
    /// <summary>
    /// 不筛选
    /// </summary>
    All = default,
    /// <summary>
    /// 仅R18内容
    /// </summary>
    NsfwOnly,
    /// <summary>
    /// 不含R18内容
    /// </summary>
    NoNsfw,
}

internal static class NsfwExtensions
{
    extension(NsfwFilter filter)
    {
        internal bool? ToRequestJsonValue() => filter switch
        {
            NsfwFilter.All => null,
            NsfwFilter.NsfwOnly => true,
            NsfwFilter.NoNsfw => false,
            _ => Throws.ThrowUnknownEnumValue<bool?>(filter),
        };

        internal static NsfwFilter FromRequestJson(bool? value) => value switch
        {
            null => NsfwFilter.All,
            true => NsfwFilter.NsfwOnly,
            false => NsfwFilter.NoNsfw,
        };
    }
}
