using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
partial class AsyncPageCollection<T>
{
    /// <summary>
    /// 获取总页数
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        PagedData<T> page;
        int limit;
        if (_limit is null) {
            page = await FetchFromStartAsync(null, cancellationToken).ConfigureAwait(false);
            limit = page.Limit;
        }
        else {
            page = await FetchFromStartAsync(1, cancellationToken).ConfigureAwait(false);
            limit = _limit.Value;
        }
        var total = page.Total - _offset;
        return checked((int)(total / limit));
    }

    /// <summary>
    /// 获取总页数
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        PagedData<T> page;
        int limit;
        if (_limit is null) {
            page = await FetchFromStartAsync(null, cancellationToken).ConfigureAwait(false);
            limit = page.Limit;
        }
        else {
            page = await FetchFromStartAsync(1, cancellationToken).ConfigureAwait(false);
            limit = _limit.Value;
        }
        var total = page.Total - _offset;
        return total / limit;
    }
}
