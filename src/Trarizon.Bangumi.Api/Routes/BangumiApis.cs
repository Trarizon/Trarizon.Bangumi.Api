using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Serialization;

namespace Trarizon.Bangumi.Api.Routes;
/// <summary>
/// Bangumi API的入口实现
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/routes.go">
/// AddRouters
/// </see>
/// </remarks>
public static partial class BangumiApis
{
    internal const string ExperimentalApiDiagnosticId = "BgmExprApi";
}
