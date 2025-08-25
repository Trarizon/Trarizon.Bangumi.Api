using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Collections;
public sealed class AsyncPageCollection<T> : IAsyncEnumerable<T>
{
    private static readonly AsyncPageCollection<T> Empty = new(default, default, takeCount: 0, default!, pageFetcher: default!);

    private readonly Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    private readonly int _takeCount; // -1 for infinite
    private readonly PageCollectionOptions _options;

    internal AsyncPageCollection(int? limit, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = 0;
        _takeCount = -1;
        _options = PageCollectionOptions.Default;
    }

    private AsyncPageCollection(int? limit, int offset, int takeCount, PageCollectionOptions options, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = offset;
        _takeCount = takeCount;
        _options = options;
    }

    /// <summary>
    /// Clone the collection with new page limit
    /// </summary>
    /// <param name="limit"></param>
    /// <returns>A new collection with given page limit</returns>
    public AsyncPageCollection<T> WithPageLimit(int? limit)
        => new AsyncPageCollection<T>(limit, _offset, _takeCount, _options, _pageFetcher);

    public Task<BangumiApiResult<PagedData<T>>> GetPageAsync(int? limit = null, int rawOffset = 0, CancellationToken cancellationToken = default)
    {
        return _pageFetcher(limit, rawOffset, cancellationToken);
    }

    public async ValueTask<int> CountAsync(CancellationToken cancellationToken = default)
        => checked((int)await LongCountAsync(cancellationToken).ConfigureAwait(false));

    public async ValueTask<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            return 0;

        var page = await _pageFetcher(1, 0, cancellationToken).Unwrap().ConfigureAwait(false);
        var total = page.Total - _offset;

        if (_takeCount < 0)
            return total;
        return long.Min(total, _takeCount);
    }

    public AsyncPageCollection<T> Take(int count)
    {
        if (count >= _takeCount) {
            return this;
        }
        if (count <= 0) {
            return Empty;
        }
        return new AsyncPageCollection<T>(_limit, _offset, count, _options, _pageFetcher);
    }

    public AsyncPageCollection<T> Skip(int count)
    {
        if (count >= _takeCount) {
            return Empty;
        }
        if (count <= 0)
            return this;
        return new AsyncPageCollection<T>(_limit, _offset + count, _takeCount - count, _options, _pageFetcher);
    }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            yield break;

        int offset = _offset;

        while (true) {
            var page = await _pageFetcher(_limit, offset, cancellationToken)
                .Unwrap().ConfigureAwait(false);
            var delayTask = Task.Delay(_options.RequestInterval, cancellationToken);

            var items = page.Datas;
            if (items.Length == 0)
                yield break;

            if (_takeCount >= 0) {
                var expectedRest = _takeCount + _offset - offset;
                if (expectedRest < items.Length) {
                    for (int i = 0; i < expectedRest; i++) {
                        cancellationToken.ThrowIfCancellationRequested();
                        var data = items[i];
                        yield return data;
                    }
                    yield break;
                }
            }

            foreach (var data in page.Datas) {
                cancellationToken.ThrowIfCancellationRequested();
                yield return data;
            }
            offset += page.Datas.Length;

            await delayTask.ConfigureAwait(false);
        }
    }
}
