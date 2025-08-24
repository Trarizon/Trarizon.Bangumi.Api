namespace Trarizon.Bangumi.Api.Models.UserModels;
/// <summary>
/// 条目收藏类型
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/collections/domain/collection/type.go#L17">
/// SubjectCollection: uint8
/// </see>
/// </remarks>
public enum SubjectCollectionType
{
    // All = 0
    /// <summary>
    /// 想看
    /// </summary>
    Wish = 1,
    /// <summary>
    /// 看过
    /// </summary>
    Collect = 2,
    /// <summary>
    /// 在看
    /// </summary>
    Doing = 3,
    /// <summary>
    /// 搁置
    /// </summary>
    OnHold = 4,
    /// <summary>
    /// 抛弃
    /// </summary>
    Dropped = 5
}

// https://github.com/bangumi/server/blob/master/internal/collections/domain/collection/type.go#L28
/// <summary>
/// 章节收藏类型
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/collections/domain/collection/type.go#L28">
/// EpisodeCollection: uint8
/// </see>
/// </remarks>
public enum EpisodeCollectionType
{
    /// <summary>
    /// 撤销/删除
    /// </summary>
    None = 0,
    // All = 0,
    /// <summary>
    /// 想看
    /// </summary>
    Wish = 1,
    /// <summary>
    /// 看过
    /// </summary>
    Collect = 2,
    /// <summary>
    /// 抛弃
    /// </summary>
    Dropped = 3,
}

internal static class CollectionTypeExtensions
{
    public static int ToQueryValue(this SubjectCollectionType collectionType) => (int)collectionType;
}