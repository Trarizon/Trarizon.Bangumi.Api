using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit;
internal delegate Task<PagedData<T>> PageFetchCallback<T>(int? limit, int offset, CancellationToken cancellationToken);
