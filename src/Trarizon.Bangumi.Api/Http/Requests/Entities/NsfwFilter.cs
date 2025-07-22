using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Http;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Http.Requests.Entities;
[JsonConverter(typeof(NsfwFilterJsonConverter))]
public enum NsfwFilter : byte
{
    All = default,
    NsfwOnly,
    NoNsfw,
}

public static class NsfwExtensions
{
    extension(NsfwFilter filter)
    {
        public bool? ToRequestJsonValue() => filter switch
        {
            NsfwFilter.All => null,
            NsfwFilter.NsfwOnly => true,
            NsfwFilter.NoNsfw => false,
            _ => Throws.ThrowUnknownEnumValue<bool?>(filter),
        };

        public static NsfwFilter FromRequestJson(bool? value) => value switch
        {
            null => NsfwFilter.All,
            true => NsfwFilter.NsfwOnly,
            false => NsfwFilter.NoNsfw,
        };
    }
}
