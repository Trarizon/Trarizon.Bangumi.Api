namespace Trarizon.Bangumi.Api.Http;
public sealed class BangumiClientOptions
{
    public string? BaseAddress { get; set; }
    
    public required string UserAgent { get; set; }
    
    /// <summary>
    /// Some apis require authentication
    /// </summary>
    public string? AccessToken { get; set; }

    public TimeSpan? Timeout { get; set; }
}
