namespace Trarizon.Bangumi.Api.Models.Subjects;
// src: uint8 https://github.com/bangumi/server/blob/master/internal/model/subject_type.go#L17
/// <summary>
/// 条目类型
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/model/subject_type.go#L17">
/// SubjectType uint8
/// </see>
/// </remarks>
public enum SubjectType
{
    // All = 0
    /// <summary>
    /// 书籍
    /// </summary>
    Book = 1,
    /// <summary>
    /// 动画
    /// </summary>
    Anime = 2,
    /// <summary>
    /// 音乐
    /// </summary>
    Music = 3,
    /// <summary>
    /// 游戏
    /// </summary>
    Game = 4,
    /// <summary>
    /// 三次元
    /// </summary>
    Real = 6,
}

internal static class SubjectTypeExtensions
{
    public static int ToQueryValue(this SubjectType subjectType) => (int)subjectType;
}