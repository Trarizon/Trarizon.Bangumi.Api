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
            var page = await FetchFromStartWithRetryAsync(1, cancellationToken).ConfigureAwait(false);
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
            var page = await FetchFromStartWithRetryAsync(1, cancellationToken).ConfigureAwait(false);
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
        if (count <= 0) {
            return Empty;
        }
        if (_takeCount >= 0 && count >= _takeCount) {
            return this;
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
        if (count <= 0) {
            return this;
        }
        if (_takeCount < 0) {
            return new AsyncPagedDataCollection<T>(_limit, _offset + count, -1, _options, _pageFetcher);
        }
        if (count >= _takeCount) {
            return Empty;
        }
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
    {
        if (index.IsFromEnd)
            return ElementAtOrDefaultFromEndAsyncInternal(index.Value, throwIfNotFound: true, cancellationToken)!;
        else
            return ElementAtOrDefaultAsyncInternal(index.Value, throwIfNotFound: true, cancellationToken)!;
    }

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
    {
        if (index.IsFromEnd)
            return ElementAtOrDefaultFromEndAsyncInternal(index.Value, throwIfNotFound: false, cancellationToken);
        else
            return ElementAtOrDefaultAsyncInternal(index.Value, throwIfNotFound: false, cancellationToken);
    }

    private Task<T?> ElementAtOrDefaultAsyncInternal(int index, bool throwIfNotFound, CancellationToken cancellationToken)
    {
        if (_takeCount == 0)
            goto Error;

        if (index < 0)
            goto Error;
        if (_takeCount >= 0 && index >= _takeCount)
            goto Error;

        return Core(index, throwIfNotFound, cancellationToken);

    Error:
        if (throwIfNotFound)
            Throws.ThrowArgumentOutOfRange(nameof(index));
        return Task.FromResult<T?>(default);

        async Task<T?> Core(int index, bool throwIfNotFound, CancellationToken cancellationToken)
        {
            var page = await FetchPageWithRetryAsync(1, _offset + index, false, cancellationToken).ConfigureAwait(false);
            if (page?.Datas is null or []) {
                if (throwIfNotFound)
                    Throws.ThrowArgumentOutOfRange(nameof(index));
                return default;
            }

            return page.Datas[0];
        }
    }

    private Task<T?> ElementAtOrDefaultFromEndAsyncInternal(int indexFromEnd, bool throwIfNotFound, CancellationToken cancellationToken)
    {
        if (_takeCount == 0)
            goto Error;

        if (indexFromEnd <= 0)
            goto Error;
        if (_takeCount >= 0 && indexFromEnd > _takeCount)
            goto Error;

        return Core(indexFromEnd, throwIfNotFound, cancellationToken);

    Error:
        if (throwIfNotFound)
            Throws.ThrowArgumentOutOfRange("index");
        return Task.FromResult<T?>(default);

        async Task<T?> Core(int indexFromEnd, bool throwIfNotFound, CancellationToken cancellationToken)
        {
            Debug.Assert(indexFromEnd > 0);
            Debug.Assert(_takeCount == -1 || indexFromEnd <= _takeCount);

            var count = await CountAsync(cancellationToken).ConfigureAwait(false);
            int index = count - indexFromEnd;

            if (index < 0)
                goto CoreError;

            var page = await FetchPageWithRetryAsync(1, index, false, cancellationToken).ConfigureAwait(false);
            Debug.Assert(page is not null);

            if (page.Datas is [])
                goto CoreError;

            return page.Datas[0];

        CoreError:
            if (throwIfNotFound)
                Throws.ThrowArgumentOutOfRange("index");
            return default;
        }
    }

    /// <summary>
    /// 获取第一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> FirstAsync(CancellationToken cancellationToken = default)
        => FirstOrDefaultAsyncInternal(throwIfNotFound: true, cancellationToken)!;

    /// <summary>
    /// 获取第一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => FirstOrDefaultAsyncInternal(throwIfNotFound: false, cancellationToken);

    private Task<T?> FirstOrDefaultAsyncInternal(bool throwIfNotFound, CancellationToken cancellationToken)
    {
        if (_takeCount == 0) {
            if (throwIfNotFound)
                Throws.ThrowInvalidOperation("Collection has no element");
            return Task.FromResult<T?>(default);
        }
        return Core(throwIfNotFound, cancellationToken);

        async Task<T?> Core(bool throwIfNotFound, CancellationToken cancellationToken)
        {
            var page = await FetchFromStartWithRetryAsync(1, cancellationToken).ConfigureAwait(false);
            if (page.Datas is []) {
                if (throwIfNotFound)
                    Throws.ThrowInvalidOperation("Collection has no element");
                return default;
            }
            return page.Datas[0];
        }
    }

    /// <summary>
    /// 获取最后一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> LastAsync(CancellationToken cancellationToken = default)
        => LastOrDefaultAsyncInternal(throwIfNotFound: true, cancellationToken)!;

    /// <summary>
    /// 获取最后一个数据
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> LastOrDefaultAsync(CancellationToken cancellationToken = default)
        => LastOrDefaultAsyncInternal(throwIfNotFound: false, cancellationToken);

    private Task<T?> LastOrDefaultAsyncInternal(bool throwIfNotFound, CancellationToken cancellationToken)
    {
        if (_takeCount == 0) {
            if (throwIfNotFound)
                Throws.ThrowInvalidOperation("Collection has no element");
            return Task.FromResult<T?>(default);
        }
        return Core(throwIfNotFound, cancellationToken);

        async Task<T?> Core(bool throwIfNotFound, CancellationToken cancellationToken)
        {
            var first = await FetchFromStartWithRetryAsync(1, cancellationToken).ConfigureAwait(false);
            if (first.Datas is []) {
                if (throwIfNotFound)
                    Throws.ThrowInvalidOperation("Collection has no element");
                return default;
            }

            var total = checked((int)first.Total);
            var last = _takeCount < 0 ? total : int.Min(total, _offset + _takeCount);

            var page = await FetchPageWithRetryAsync(1, last - 1, false, cancellationToken).ConfigureAwait(false);
            Debug.Assert(page is not null && page.Datas is not []);
            return page.Datas[0];
        }
    }
}
