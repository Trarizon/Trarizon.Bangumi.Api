using System.Diagnostics;
using Trarizon.Bangumi.Api.Utilities;

namespace Trarizon.Bangumi.Api.Toolkit.Collections;
partial class AsyncPagedDataCollection<T>
{
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
            var page = await _pageFetcher(1, 0, cancellationToken).ConfigureAwait(false);
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
            var page = await _pageFetcher(1, 0, cancellationToken).ConfigureAwait(false);
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
    public AsyncPagedDataCollection<T> Take(int count)
    {
        if (count >= _takeCount) {
            return this;
        }
        if (count <= 0) {
            return Empty;
        }
        return new AsyncPagedDataCollection<T>(_limit, _offset, count, _options, _pageFetcher);
    }

    /// <summary>
    /// 跳过指定数量的数据，返回剩余数据
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public AsyncPagedDataCollection<T> Skip(int count)
    {
        if (count >= _takeCount) {
            return Empty;
        }
        if (count <= 0)
            return this;
        return new AsyncPagedDataCollection<T>(_limit, _offset + count, _takeCount - count, _options, _pageFetcher);
    }

    /// <summary>
    /// 获取指定索引的数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> ElementAtAsync(int index, CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsyncInternal(index, throwIfNotFound: true, cancellationToken)!;

    /// <summary>
    /// 获取指定索引的数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> ElementAtAsync(Index index, CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsyncInternal(index, throwIfNotFound: true, cancellationToken)!;

    /// <summary>
    /// 获取指定索引的数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> ElementAtOrDefaultAsync(int index, CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsyncInternal(index, throwIfNotFound: false, cancellationToken);

    /// <summary>
    /// 获取指定索引的数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> ElementAtOrDefaultAsync(Index index, CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsyncInternal(index, throwIfNotFound: false, cancellationToken);

    private Task<T?> ElementAtOrDefaultAsyncInternal(Index index, bool throwIfNotFound, CancellationToken cancellationToken)
    {
        if (_takeCount == 0) {
            goto Error;
        }

        if (index.IsFromEnd) {
            var indexFromEnd = index.Value;
            if (indexFromEnd <= 0)
                goto Error;
            if (_takeCount >= 0 && indexFromEnd > _takeCount)
                goto Error;
        }
        else {
            var indexValue = index.Value;
            if (indexValue < 0)
                goto Error;
            if (_takeCount >= 0 && indexValue >= _takeCount)
                goto Error;
        }

        return Core(index, throwIfNotFound, cancellationToken);

    Error:
        if (throwIfNotFound)
            Throws.ThrowArgumentOutOfRange(nameof(index));
        return Task.FromResult<T?>(default);

        async Task<T?> Core(Index index, bool throwIfNotFound, CancellationToken cancellationToken)
        {
            int indexValue;
            if (index.IsFromEnd) {
                var count = await CountAsync(cancellationToken).ConfigureAwait(false);
                indexValue = index.GetOffset(count);
            }
            else {
                indexValue = index.Value;
            }

            Debug.Assert(indexValue >= 0);
            Debug.Assert(_takeCount < 0 || indexValue < _takeCount);

            var page = await _pageFetcher(1, indexValue + _offset, cancellationToken).ConfigureAwait(false);
            if (page.Datas.Length == 0) {
                if (throwIfNotFound)
                    Throws.ThrowArgumentOutOfRange(nameof(index));
                return default;
            }
            else {
                return page.Datas[0];
            }
        }
    }

    /// <summary>
    /// 获取第一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> FirstAsync(CancellationToken cancellationToken = default)
        => ElementAtAsync(0, cancellationToken);

    /// <summary>
    /// 获取第一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsync(0, cancellationToken);

    /// <summary>
    /// 获取最后一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> LastAsync(CancellationToken cancellationToken = default)
        => ElementAtAsync(^1, cancellationToken);

    /// <summary>
    /// 获取最后一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> LastOrDefaultAsync(CancellationToken cancellationToken = default)
        => ElementAtOrDefaultAsync(^1, cancellationToken);
}
