using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models.Episodes;
// https://github.com/bangumi/server/blob/master/internal/episode/model.go#L21
[GoSource<byte>]
public enum EpisodeType
{
    /// <summary>
    /// 本篇
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 特别篇
    /// </summary>
    Special = 1,
    /// <summary>
    /// OP
    /// </summary>
    OpeningSong = 2,
    /// <summary>
    /// ED
    /// </summary>
    EndingSong = 3,
    /// <summary>
    /// 预告/宣传/广告
    /// </summary>
    Trailer = 4, // 源码这里是MAD，没有Trailer，没有5
    /// <summary>
    /// MAD
    /// </summary>
    MAD = 5,
    /// <summary>
    /// 其他
    /// </summary>
    Other = 6,
}

internal static class EpisodeTypeExtensions
{
    internal static int ToQueryValue(this EpisodeType episodeType) => (int)episodeType;
}