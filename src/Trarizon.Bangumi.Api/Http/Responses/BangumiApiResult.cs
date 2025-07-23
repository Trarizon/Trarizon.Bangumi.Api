using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using Trarizon.Bangumi.Api.Http.Exceptions;

namespace Trarizon.Bangumi.Api.Http.Responses;
/// <summary>
/// 封装API返回结果的Result
/// </summary>
public readonly struct BangumiApiResult
{
    private readonly HttpResponseMessage _resp;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求返回的消息
    /// </summary>
    public HttpResponseMessage ResponseMessage => _resp;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _resp.StatusCode;

    /// <summary>
    /// 是否成功
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => _error is null;

    /// <summary>
    /// 是否返回错误
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsError => _error is not null;

    /// <summary>
    /// 错误信息
    /// </summary>
    public RequestError? Error => _error;

    internal BangumiApiResult(HttpResponseMessage resp)
    {
        _resp = resp;
    }

    internal BangumiApiResult(HttpResponseMessage resp, RequestError error)
        => (_resp, _error) = (resp, error);

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    public void ThrowIfError()
    {
        if (IsError)
            BangumiApiException.Throw(this);
    }
}

/// <summary>
/// 封装API返回结果的Result
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct BangumiApiResult<T>
{
    private readonly HttpResponseMessage _resp;
    private readonly T? _value;
    private readonly RequestError? _error;

    /// <summary>
    /// API请求返回的消息
    /// </summary>
    public HttpResponseMessage ResponseMessage => _resp;

    /// <summary>
    /// API请求响应的状态码
    /// </summary>
    public HttpStatusCode StatusCode => _resp.StatusCode;

    /// <summary>
    /// 是否成功
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => _error is null;

    /// <summary>
    /// 是否返回错误
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsError => _error is not null;

    /// <summary>
    /// 成功时返回的数据
    /// </summary>
    public T? Value => _value;

    /// <summary>
    /// 错误信息
    /// </summary>
    public RequestError? Error => _error;

    internal BangumiApiResult(HttpResponseMessage resp, T value)
    {
        _resp = resp;
        _value = value;
    }

    internal BangumiApiResult(HttpResponseMessage resp, RequestError error)
    {
        _resp = resp;
        _error = error;
    }

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    [MemberNotNull(nameof(Value))]
    public void ThrowIfError()
    {
        if (IsError) {
            BangumiApiException.Throw(this);
        }
    }
}

/// <summary>
/// <see cref="BangumiApiResult"/>相关扩展
/// </summary>
public static class BangumiApiResultExtensions
{
    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resultTask"></param>
    /// <returns></returns>
    public static UnwrapAwaitable<T> Unwrap<T>(this Task<BangumiApiResult<T>> resultTask)
        => new(resultTask);

    /// <summary>
    /// 若失败，将错误数据包装进<see cref="BangumiApiException"/>异常抛出
    /// </summary>
    /// <param name="resultTask"></param>
    /// <returns></returns>
    public static UnwrapAwaitable Unwrap(this Task<BangumiApiResult> resultTask)
        => new(resultTask);

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    public readonly struct UnwrapAwaitable
    {
        internal readonly Task<BangumiApiResult> _task;

        internal UnwrapAwaitable(Task<BangumiApiResult> task)
        {
            _task = task;
        }

        public async Task AsTask()
        {
            var result = await _task.ConfigureAwait(false);
            result.ThrowIfError();
        }

        public Awaiter GetAwaiter() => new(this);

        public ConfiguredUnwrapAwaitable ConfigureAwait(bool continueOnCapturedContext) => new(this, continueOnCapturedContext ? ConfigureAwaitOptions.ContinueOnCapturedContext : ConfigureAwaitOptions.None);

        public readonly struct Awaiter : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly TaskAwaiter<BangumiApiResult> _awaiter;

            internal Awaiter(UnwrapAwaitable awaitable)
            {
                _awaiter = awaitable._task.GetAwaiter();
            }

            public bool IsCompleted => _awaiter.IsCompleted;

            public void GetResult()
            {
                var res = _awaiter.GetResult();
                res.ThrowIfError();
            }

            public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
            public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
        }
    }

    public readonly struct ConfiguredUnwrapAwaitable
    {
        private readonly Task<BangumiApiResult> _task;
        private readonly ConfigureAwaitOptions _options;

        internal ConfiguredUnwrapAwaitable(UnwrapAwaitable awaitable, ConfigureAwaitOptions options)
        {
            _task = awaitable._task;
            _options = options;
        }

        public Awaiter GetAwaiter() => new(this);

        public readonly struct Awaiter : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly ConfiguredTaskAwaitable<BangumiApiResult>.ConfiguredTaskAwaiter _awaiter;

            internal Awaiter(ConfiguredUnwrapAwaitable awaitable)
            {
                _awaiter = awaitable._task.ConfigureAwait(awaitable._options).GetAwaiter();
            }

            public bool IsCompleted => _awaiter.IsCompleted;

            public void GetResult()
            {
                var res = _awaiter.GetResult();
                res.ThrowIfError();
            }

            public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
            public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
        }
    }

    public readonly struct UnwrapAwaitable<T>
    {
        internal readonly Task<BangumiApiResult<T>> _task;

        internal UnwrapAwaitable(Task<BangumiApiResult<T>> task)
        {
            _task = task;
        }

        public async Task AsTask()
        {
            var result = await _task.ConfigureAwait(false);
            result.ThrowIfError();
        }

        public Awaiter GetAwaiter() => new(this);

        public ConfiguredUnwrapAwaitable<T> ConfigureAwait(bool continueOnCapturedContext) => new(this, continueOnCapturedContext ? ConfigureAwaitOptions.ContinueOnCapturedContext : ConfigureAwaitOptions.None);

        public readonly struct Awaiter : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly TaskAwaiter<BangumiApiResult<T>> _awaiter;

            internal Awaiter(UnwrapAwaitable<T> awaitable)
            {
                _awaiter = awaitable._task.GetAwaiter();
            }

            public bool IsCompleted => _awaiter.IsCompleted;

            public T GetResult()
            {
                var res = _awaiter.GetResult();
                res.ThrowIfError();
                return res.Value;
            }

            public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
            public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
        }
    }

    public readonly struct ConfiguredUnwrapAwaitable<T>
    {
        private readonly Task<BangumiApiResult<T>> _task;
        private readonly ConfigureAwaitOptions _options;

        internal ConfiguredUnwrapAwaitable(UnwrapAwaitable<T> awaitable, ConfigureAwaitOptions options)
        {
            _task = awaitable._task;
            _options = options;
        }

        public Awaiter GetAwaiter() => new(this);

        public readonly struct Awaiter : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly ConfiguredTaskAwaitable<BangumiApiResult<T>>.ConfiguredTaskAwaiter _awaiter;

            internal Awaiter(ConfiguredUnwrapAwaitable<T> awaitable)
            {
                _awaiter = awaitable._task.ConfigureAwait(awaitable._options).GetAwaiter();
            }

            public bool IsCompleted => _awaiter.IsCompleted;

            public T GetResult()
            {
                var res = _awaiter.GetResult();
                res.ThrowIfError();
                return res.Value;
            }

            public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
            public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
        }
    }

#pragma warning restore CS1591
}
