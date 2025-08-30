using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.Abstractions;
/// <summary>
/// 条目
/// </summary>
public interface ISubjectIdentity
{
    /// <summary>
    /// 条目ID
    /// </summary>
    uint Id { get; }
}

/// <summary>
/// 条目基本信息
/// </summary>
public interface ISubject : ISubjectIdentity
{
    /// <summary>
    /// 条目中文名称
    /// </summary>
    string ChineseName { get; }

    /// <summary>
    /// 条目名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 条目类型
    /// </summary>
    SubjectType Type { get; }
}

/// <summary>
/// 条目默认图像
/// </summary>
public interface ISubjectImageUrlProvider
{
    /// <summary>
    /// 条目默认图片URL
    /// </summary>
    string ImageUrl { get; }
}

/// <summary>
/// 条目图像
/// </summary>
public interface ISubjectImagesProvider : ISubjectImageUrlProvider
{
    /// <summary>
    /// 条目图片
    /// </summary>
    SubjectImageSet Images { get; }

    string ISubjectImageUrlProvider.ImageUrl => Images.Large;
}
