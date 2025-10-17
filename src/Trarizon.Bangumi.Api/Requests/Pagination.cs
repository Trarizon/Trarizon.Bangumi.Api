using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Requests;
/// <summary>
/// API请求的分页参数
/// </summary>
/// <param name="pageLimit">单页最大条目数</param>
/// <param name="pageOffset">单页偏移</param>
public readonly struct Pagination(int? pageLimit = null, int? pageOffset = null)
{
    /// <summary>
    /// 单页最大条目数量，该值必须大于0，过大会被API限制在maxLimit内，取决于http API
    /// </summary>
    public int? Limit { get; } = pageLimit;
    /// <summary>
    /// 页面偏移的条目数量
    /// </summary>
    public int? Offset { get; } = pageOffset;
}

internal static class PaginationExtensions
{
    /// <param name="builder"></param>
    /// <param name="pagination"></param>
    /// <param name="limitQueryName"></param>
    /// <param name="offsetQueryName"></param>
    internal static void AppendPagination(this ref QueryBuilder builder, Pagination pagination, string limitQueryName = "limit", string offsetQueryName = "offset")
    {
        if (pagination.Limit is { } limit)
            builder.AppendQuery(limitQueryName, limit);
        if (pagination.Offset is { } offset)
            builder.AppendQuery(offsetQueryName, offset);
    }
}