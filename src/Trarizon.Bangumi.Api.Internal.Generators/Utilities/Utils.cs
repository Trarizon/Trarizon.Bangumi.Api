namespace Trarizon.Bangumi.Api.Internal.Generators.Utilities;
internal static class Utils
{
    public static string ToCamelCase(string pascal)
    {
        if (string.IsNullOrEmpty(pascal) || char.IsLower(pascal[0]))
            return pascal;

        if (pascal.Length == 1)
            return pascal.ToLowerInvariant();

        return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
    }
}
