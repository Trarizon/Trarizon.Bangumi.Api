namespace Trarizon.Bangumi.Api.Utilities;
internal interface IStringEnumeration<TSelf>
{
    /// <summary>
    /// 字符串值
    /// </summary>
    string? StringValue { get; }
    static abstract TSelf Create(string? value);
}
