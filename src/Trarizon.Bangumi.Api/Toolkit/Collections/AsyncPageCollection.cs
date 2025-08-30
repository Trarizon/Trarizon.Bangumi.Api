using System.Diagnostics;
using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// 按照指定分页参数异步获取数据的集合
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed partial class AsyncPageCollection<T> : IAsyncEnumerable<T>
{
    // Note: 如果为外部提供了调用_pageFetcher的方法，那么我们是不能使用Empty作为空集合实例的
    // 简单起见删除了GetPageAsync(lmt, ofs)方法
    private static readonly AsyncPageCollection<T> Empty = new(default, default, takeCount: 0, default!, pageFetcher: default!);

    private readonly Func<int?, int, CancellationToken, Task<PagedData<T>>> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    private readonly int _takeCount; // -1 for infinite
    private readonly AsyncPageCollectionOptions _options;

    internal AsyncPageCollection(int? limit, AsyncPageCollectionOptions options, Func<int?, int, CancellationToken, Task<PagedData<T>>> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = 0;
        _takeCount = -1;
        _options = options;
    }

    private AsyncPageCollection(int? limit, int offset, int takeCount, AsyncPageCollectionOptions options, Func<int?, int, CancellationToken, Task<PagedData<T>>> pageFetcher)
    {
        Debug.Assert(_takeCount >= 0);
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

    /// <summary></summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        Debug.Assert(_takeCount >= 0);
        if (_takeCount == 0)
            yield break;

        int offset = _offset;

        while (true) {
            var page = await _pageFetcher(_limit, offset, cancellationToken).ConfigureAwait(false);
            var delayTask = Task.Delay(_options.RequestInterval, cancellationToken);

            var items = page.Datas;
            if (items.Length == 0)
                yield break;

            // Finite
            if (_takeCount >= 0) {
                var endOffset = _offset + _takeCount;
                int chunkCount = int.Min(items.Length, endOffset - offset);
                for (int i = 0; i < chunkCount; i++) {
                    cancellationToken.ThrowIfCancellationRequested();
                    var data = items[i];
                    yield return data;
                }
                offset += chunkCount;
                if (offset >= endOffset)
                    yield break;
            }
            // Infinite
            else {
                foreach (var item in items) {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }
                offset += items.Length;
            }

            // No more data, we can sure the next fetch result is empty.
            if (items.Length < _limit)
                yield break;

            await delayTask.ConfigureAwait(false);
        }
    }
}
