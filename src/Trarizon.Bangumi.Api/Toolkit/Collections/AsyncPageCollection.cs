using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// 按照指定分页参数异步获取数据的集合
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class AsyncPageCollection<T> : IAsyncEnumerable<T>
{
    // Note: 如果为外部提供了调用_pageFetcher的方法，那么我们是不能使用Empty作为空集合实例的
    // 简单起见删除了GetPageAsync(lmt, ofs)方法
    private static readonly AsyncPageCollection<T> Empty = new(default, default, takeCount: 0, default!, pageFetcher: default!);

    private readonly Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    private readonly int _takeCount; // -1 for infinite
    private readonly AsyncPageCollectionOptions _options;

    internal AsyncPageCollection(int? limit, AsyncPageCollectionOptions options, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = 0;
        _takeCount = -1;
        _options = options;
    }

    private AsyncPageCollection(int? limit, int offset, int takeCount, AsyncPageCollectionOptions options, Func<int?, int, CancellationToken, Task<BangumiApiResult<PagedData<T>>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = offset;
        _takeCount = takeCount;
        _options = options;
    }

    /// <summary>
    /// 重新设置单页数量显示，返回新的集合
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    public AsyncPageCollection<T> WithPageLimit(int? limit)
        => new AsyncPageCollection<T>(limit, _offset, _takeCount, _options, _pageFetcher);

    /// <summary>
    /// 获取数据的总数
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            return Task.FromResult(0);
        return Core(cancellationToken);

        async Task<int> Core(CancellationToken cancellationToken)
        {
            var page = await _pageFetcher(1, 0, cancellationToken).Unwrap().ConfigureAwait(false);
            var total = page.Total - _offset;

            if (_takeCount < 0)
                return checked((int)total);
            return checked((int)long.Min(total, _takeCount));
        }
    }

    /// <summary>
    /// 获取数据的总数
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        if (_takeCount == 0)
            return Task.FromResult(0L);
        return Core(cancellationToken);

        async Task<long> Core(CancellationToken cancellationToken)
        {
            var page = await _pageFetcher(1, 0, cancellationToken).Unwrap().ConfigureAwait(false);
            var total = page.Total - _offset;

            if (_takeCount < 0)
                return total;
            return long.Min(total, _takeCount);
        }
    }

    /// <summary>
    /// 获取指定数量的数据
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 跳过指定数量的数据，返回剩余数据
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public AsyncPageCollection<T> Skip(int count)
    {
        if (count >= _takeCount) {
            return Empty;
        }
        if (count <= 0)
            return this;
        return new AsyncPageCollection<T>(_limit, _offset + count, _takeCount - count, _options, _pageFetcher);
    }

    /// <summary></summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
