namespace Trarizon.Bangumi.Api.Utilities;
internal interface IStringEnumeration<TSelf>
{
    string? StringValue { get; }
    static abstract TSelf Create(string? value);
}
