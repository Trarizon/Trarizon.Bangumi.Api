namespace Trarizon.Bangumi.Api.Models.Revisions;
// https://github.com/bangumi/server/blob/master/internal/model/revision.go#L100
public sealed class EpisodeRevisionDataItem
{
    // TODO: 源码全是string，未实现？
    public string AirDate { get; internal set; }
    public string Description { get; internal set; }
    public string Duration { get; internal set; }
    public string Name { get; internal set; }
    public string ChineseName { get; internal set; }
    public string Sort { get; internal set; }
    public string Type { get; internal set; }
}
