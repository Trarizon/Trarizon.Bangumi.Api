using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models.Users;
// https://github.com/bangumi/server/blob/master/internal/collections/domain/collection/type.go#L17
[GoSource<byte>]
public enum CollectionType
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
[GoSource<byte>]
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
    public static int ToQueryValue(this CollectionType collectionType) => (int)collectionType;
}