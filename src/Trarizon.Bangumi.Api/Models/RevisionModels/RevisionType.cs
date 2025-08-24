namespace Trarizon.Bangumi.Api.Models.RevisionModels;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 编辑记录类型
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/internal/model/revision.type.go">
/// RevisionType
/// </see>
/// </remarks>
public enum RevisionType
{
    /// <summary>
    /// 条目
    /// </summary>
    Subject = 1,
    /// <summary>
    /// 条目-角色关联
    /// </summary>
    SubjectCharacterRelation = 5,
    /// <summary>
    /// 条目-声优关联
    /// </summary>
    SubjectCastRelation = 6,
    /// <summary>
    /// 条目-人物关联
    /// </summary>
    SubjectPersonRelation = 10,
    /// <summary>
    /// 条目管理
    /// </summary>
    SubjectMerge = 11,
    SubjectErase = 12,
    /// <summary>
    /// 条目关联
    /// </summary>
    SubjectRelation = 17,
    SubjectLock = 103,
    SubjectUnlock = 104,
    /// <summary>
    /// 角色
    /// </summary>
    Character = 2,
    /// <summary>
    /// 角色-条目关联
    /// </summary>
    CharacterSubjectRelation = 4,
    /// <summary>
    /// 角色-声优关联
    /// </summary>
    CharacterCastRelation = 7,
    /// <summary>
    /// 角色管理
    /// </summary>
    CharacterMerge = 13,
    CharacterErase = 14,
    /// <summary>
    /// 人物
    /// </summary>
    Person = 3,
    /// <summary>
    /// 人物-声优关联
    /// </summary>
    PersonCastRelation = 8,
    /// <summary>
    /// 人物-条目关联
    /// </summary>
    PersonSubjectRelation = 9,
    /// <summary>
    /// 人物管理
    /// </summary>
    PersonMerge = 15,
    PersonErase = 16,
    /// <summary>
    /// 章节
    /// </summary>
    Episode = 18,
    /// <summary>
    /// 章节管理
    /// </summary>
    EpisodeMerge = 181,
    EpisodeMove = 182,
    EpisodeLock = 183,
    EpisodeUnlock = 184,
    EpisodeErase = 185,
}

#pragma warning restore CS1591
