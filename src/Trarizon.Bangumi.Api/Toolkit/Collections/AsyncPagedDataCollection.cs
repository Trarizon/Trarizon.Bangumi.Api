using System.Diagnostics;
using System.Net;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
/// <summary>
/// 按照指定分页参数异步获取数据的集合
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed partial class AsyncPagedDataCollection<T> : IAsyncEnumerable<T>
{
    // Note: 如果为外部提供了调用_pageFetcher的方法，那么我们是不能使用Empty作为空集合实例的
    // 简单起见删除了GetPageAsync(lmt, ofs)方法
    private static readonly AsyncPagedDataCollection<T> Empty = new(default, default, takeCount: 0, default!, pageFetcher: default!);

    /// <remarks>
    /// Do not directly invoke this, use <see cref="FetchPageAsync(int?, int, bool, CancellationToken)"/> instead
    /// </remarks>
    private readonly PageFetchCallback<T> _pageFetcher;
    private readonly int? _limit;
    private readonly int _offset;
    /// <summary>
    /// If -1, means infinite
    /// </summary>
    private readonly int _takeCount; // -1 for infinite
    private readonly AsyncPageCollectionOptions _options;

    internal AsyncPagedDataCollection(int? limit, AsyncPageCollectionOptions options, PageFetchCallback<T> pageFetcher)
    {
        _pageFetcher = pageFetcher;
        _limit = limit;
        _offset = 0;
        _takeCount = -1;
        _options = options;
    }

    internal AsyncPagedDataCollection(int? limit, int offset, int takeCount, AsyncPageCollectionOptions options, PageFetchCallback<T> pageFetcher)
    {
        Debug.Assert(_takeCount >= -1);
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
    public AsyncPagedDataCollection<T> WithPageLimit(int? limit)
        => new AsyncPagedDataCollection<T>(limit, _offset, _takeCount, _options, _pageFetcher);

    /// <summary></summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        Debug.Assert(_takeCount >= -1);
        if (_takeCount == 0)
            yield break;

        int offset = _offset;

        while (true) {
            bool isFirst = offset == _offset;
            PagedData<T>? page = await FetchPageAsync(_limit, offset, isFirst, cancellationToken).ConfigureAwait(false);
            if (page?.Datas is null or [])
                yield break;

            var items = page.Datas;
            // Finite
            if (_takeCount >= 0) {
                var endOffset = _offset + _takeCount;
                int chunkCount = int.Min(items.Length, endOffset - offset);
                for (int i = 0; i < chunkCount; i++) {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return items[i];
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
