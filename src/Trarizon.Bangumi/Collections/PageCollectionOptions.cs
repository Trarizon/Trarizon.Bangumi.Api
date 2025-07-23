namespace Trarizon.Bangumi.Collections;
public sealed class PageCollectionOptions
{
    internal static PageCollectionOptions Default { get; } = new()
    {
        RequestInterval = TimeSpan.FromMilliseconds(500),
    };

    public TimeSpan RequestInterval { get; set; }

    public PageCollectionOptions Clone() => new()
    {
        RequestInterval = RequestInterval
    };
}
