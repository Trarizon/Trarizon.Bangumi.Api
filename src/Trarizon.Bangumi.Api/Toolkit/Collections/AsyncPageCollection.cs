using System.Diagnostics;
using System.Net;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// 按照指定分页参数异步获取页面的集合
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed partial class AsyncPageCollection<T> : IAsyncEnumerable<PagedData<T>>
{
    private readonly PageFetchCallback<T> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    private readonly AsyncPageCollectionOptions _options;

    internal AsyncPageCollection(int? limit, AsyncPageCollectionOptions options, PageFetchCallback<T> pageFetcher)
    {
        _options = options;
        _limit = limit;
        _offset = 0;
        _pageFetcher = pageFetcher;
    }

    /// <summary></summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerator<PagedData<T>> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        int? limit = _limit;
        int offset = _offset;
        while (true) {
            var page = await FetchPageAsync(limit, offset, true, cancellationToken).ConfigureAwait(false);
            if (page?.Datas is null or [])
                yield break;

            limit ??= page.Limit;

            yield return page;

            Debug.Assert(_limit is null || _limit >= page.Datas.Length);
            offset += page.Datas.Length;

            // No more data, we can sure the next fetch result is empty.
            if (page.Datas.Length < _limit)
                yield break;
        }
    }

    private async Task<PagedData<T>?> FetchPageAsync(int? limit, int offset, bool throwIfOutOfRange, CancellationToken cancellationToken)
    {
        try {
            return await _pageFetcher(new(limit, offset), cancellationToken).ConfigureAwait(false);
        }
        catch (BangumiApiException ex) {
            if (!throwIfOutOfRange && ex.HttpStatusCode is HttpStatusCode.BadRequest) {
                // 正常来说，offset为0是一定不会越界的
                var countProvider = await _pageFetcher(new(1, 0), cancellationToken).ConfigureAwait(false);
                var count = countProvider.Total;
                // 如果offset >= count，说明是越界导致的，那么返回null作为替代
                if (offset >= count) {
                    return null;
                }
            }
            throw;
        }
    }

    private Task<PagedData<T>> FetchFromStartAsync(int? limit, CancellationToken cancellationToken)
        => FetchPageAsync(limit, 0, true, cancellationToken)!;
}
