using System.Runtime.CompilerServices;

namespace Trarizon.Bangumi.Api.Utilities;
internal static class StringExtensions
{
    extension(ref DefaultInterpolatedStringHandler handler)
    {
        public ReadOnlySpan<char> Text => GetTextSpan(ref handler);
    }


    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_Text")]
    public static extern ReadOnlySpan<char> GetTextSpan(ref DefaultInterpolatedStringHandler handler);
}
