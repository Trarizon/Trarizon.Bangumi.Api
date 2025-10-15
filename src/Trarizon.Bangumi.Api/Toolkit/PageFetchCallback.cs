using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;

namespace Trarizon.Bangumi.Api.Toolkit;
internal delegate Task<PagedData<T>> PageFetchCallback<T>(Pagination pagination, CancellationToken cancellationToken);
