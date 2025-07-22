using System.Diagnostics;

#pragma warning disable CS9113 // 参数未读。

namespace Trarizon.Bangumi.Api.Attributes;
/// <summary>
/// 用来标记go源码，该Attribute在发布时不会存在
/// </summary>
[AttributeUsage(AttributeTargets.All)]
[Conditional("GO_SOURCE_MARK")]
internal sealed class GoSourceAttribute(string? message) : Attribute
{ 
    public string? Url { get; set; }
}

/// <summary>
/// 用来标记go源码中的类型，该属性在发布时不会存在
/// </summary>
[AttributeUsage(AttributeTargets.All)]
[Conditional("GO_SOURCE_MARK")]
internal sealed class GoSourceAttribute<T>(string? name = null) : Attribute
{ 
    public string? Url { get; set; }
}