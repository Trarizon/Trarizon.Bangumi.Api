using Trarizon.Bangumi.Api.Http.Responses;

namespace Trarizon.Bangumi.Collections;
public sealed class PageCollection<T> : IAsyncEnumerable<T>
{
    private static readonly PageCollection<T> Empty = new(default, default, takeCount: 0, default, default!);

    private readonly Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    private readonly int _takeCount; // -1 for infinite
    private readonly CancellationToken _cancellationToken;

    internal PageCollection(int? limit, CancellationToken cancellationToken, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = 0;
        _takeCount = -1;
        _cancellationToken = cancellationToken;
    }

    private PageCollection(int? limit, int offset, int takeCount, CancellationToken cancellationToken, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = offset;
        _takeCount = takeCount;
        _cancellationToken = cancellationToken;
    }

    public Task<BangumiApiResult<PagedData<T>>> GetPageAsync(int? limit = null, int rawOffset = 0, CancellationToken cancellationToken = default)
    {
        cancellationToken = CombineCancellationToken(_cancellationToken, cancellationToken);
        return _pageFetcher(limit, rawOffset, cancellationToken);
    }

    public async ValueTask<int> CountAsync(CancellationToken cancellationToken = default)
        => checked((int)await LongCountAsync(cancellationToken).ConfigureAwait(false));

    public async ValueTask<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            return 0;

        cancellationToken = CombineCancellationToken(_cancellationToken, cancellationToken);

        var page = await _pageFetcher(1, 0, cancellationToken).Unwrap().ConfigureAwait(false);
        var total = page.Total - _offset;

        if (_takeCount < 0)
            return total;
        return long.Min(total, _takeCount);
    }

    public PageCollection<T> Take(int count)
    {
        if (count == 0)
            return Empty;
        if (_takeCount >= 0)
            count += _takeCount;
        return new PageCollection<T>(_limit, _offset, count, _cancellationToken, _pageFetcher);
    }

    public PageCollection<T> Skip(int count)
    {
        if (count <= 0)
            return this;
        return new PageCollection<T>(_limit, _offset + count, _takeCount, _cancellationToken, _pageFetcher);
    }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            yield break;

        int offset = _offset;
        cancellationToken = CombineCancellationToken(_cancellationToken, cancellationToken);

        while (true) {
            var page = await _pageFetcher(_limit, offset, cancellationToken)
                .Unwrap().ConfigureAwait(false);
            var items = page.Datas;
            if (items.Length == 0)
                yield break;

            if (_takeCount >= 0) {
                var expectedRest = _takeCount + _offset - offset;
                if (expectedRest < items.Length) {
                    for (int i = 0; i < expectedRest; i++) {
                        var data = items[i];
                        yield return data;
                    }
                    yield break;
                }
            }

            foreach (var data in page.Datas) {
                yield return data;
            }
            offset += page.Datas.Length;
        }
    }

    private static CancellationToken CombineCancellationToken(CancellationToken first, CancellationToken second)
    {
        if (first == default)
            return second;
        if (second == default)
            return first;
        if (first == second)
            return first;
        return CancellationTokenSource.CreateLinkedTokenSource(first, second).Token;
    }
}
