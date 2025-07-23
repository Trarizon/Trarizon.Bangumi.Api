using System.Diagnostics;

namespace Trarizon.Bangumi.Api.Models.Subjects;
/// <summary>
/// 条目分类
/// </summary>
/// <remarks>
/// 该类型提供了`Subject.XXX()`，`new(XXX)`，隐式转换三种构造方式
/// <br/>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/handler/subject/browse.go#L88">
/// uint16
/// </see>
/// </remarks>
public readonly struct SubjectCategory
{
    /// <summary>
    /// 构造书籍类型条目分类，无具体分类
    /// </summary>
    /// <returns></returns>
    public static SubjectCategory Book() => new(SubjectType.Book);

    /// <summary>
    /// 构造书籍类型条目分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static SubjectCategory Book(SubjectBookCategory category) => new(SubjectType.Book, (ushort)category);

    /// <summary>
    /// 构造动画类型条目分类，无具体分类
    /// </summary>
    /// <returns></returns>
    public static SubjectCategory Anime() => new(SubjectType.Anime);

    /// <summary>
    /// 构造动画类型条目分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static SubjectCategory Anime(SubjectAnimeCategory category) => new(SubjectType.Anime, (ushort)category);

    /// <summary>
    /// 构造游戏类型条目分类，无具体分类
    /// </summary>
    /// <returns></returns>
    public static SubjectCategory Game() => new(SubjectType.Game);

    /// <summary>
    /// 构造游戏类型条目分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static SubjectCategory Game(SubjectGameCategory category) => new(SubjectType.Game, (ushort)category);

    /// <summary>
    /// 构造音乐类型条目分类，无具体分类
    /// </summary>
    /// <returns></returns>
    public static SubjectCategory Music() => new(SubjectType.Music);

    /// <summary>
    /// 构造三次元类型条目分类，无具体分类
    /// </summary>
    /// <returns></returns>
    public static SubjectCategory Real() => new(SubjectType.Real);

    /// <summary>
    /// 构造三次元类型条目分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static SubjectCategory Real(SubjectRealCategory category) => new(SubjectType.Real, (ushort)category);

    private readonly ushort _value;
    private readonly bool _hasCategory;
    private readonly byte _type;

    private SubjectCategory(SubjectType kind, ushort value)
    {
        Debug.Assert((int)kind is >= 0 and <= 255);
        _type = (byte)kind;
        _hasCategory = true;
        _value = value;
    }

    /// <summary>
    /// 从SubjectType创建，
    /// </summary>
    /// <param name="subjectType"></param>
    public SubjectCategory(SubjectType subjectType)
    {
        Debug.Assert((int)subjectType is >= 0 and <= 255);
        _type = (byte)subjectType;
    }

    /// <summary>
    /// 创建书籍类别
    /// </summary>
    /// <param name="category"></param>
    public SubjectCategory(SubjectBookCategory category) : this(SubjectType.Book, (ushort)category) { }
    /// <summary>
    /// 创建动画类别
    /// </summary>
    /// <param name="category"></param>
    public SubjectCategory(SubjectAnimeCategory category) : this(SubjectType.Anime, (ushort)category) { }
    /// <summary>
    /// 创建游戏类别
    /// </summary>
    /// <param name="category"></param>
    public SubjectCategory(SubjectGameCategory category) : this(SubjectType.Game, (ushort)category) { }
    /// <summary>
    /// 创建三次元类别
    /// </summary>
    /// <param name="category"></param>
    public SubjectCategory(SubjectRealCategory category) : this(SubjectType.Real, (ushort)category) { }

    internal SubjectType SubjectType => (SubjectType)_type;

    internal ushort? ToQueryValue() => _hasCategory ? _value : null;

    /// <summary>
    /// 隐式转换创建书籍类别
    /// </summary>
    /// <param name="category"></param>
    public static implicit operator SubjectCategory(SubjectBookCategory category) => new(category);
    /// <summary>
    /// 隐式转换创建动画类别
    /// </summary>
    /// <param name="category"></param>
    public static implicit operator SubjectCategory(SubjectAnimeCategory category) => new(category);
    /// <summary>
    /// 隐式转换创建游戏类别
    /// </summary>
    /// <param name="category"></param>
    public static implicit operator SubjectCategory(SubjectGameCategory category) => new(category);
    /// <summary>
    /// 隐式转换创建三次元类别
    /// </summary>
    /// <param name="category"></param>
    public static implicit operator SubjectCategory(SubjectRealCategory category) => new(category);
}

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

/// <summary>
/// 书籍条目类别
/// </summary>
public enum SubjectBookCategory : ushort
{
    Other = 0,
    Comic = 1001,
    Novel = 1002,
    Artbook = 1003,
}

/// <summary>
/// 动画条目类别
/// </summary>
public enum SubjectAnimeCategory : ushort
{
    Other = 0,
    TV = 1,
    OVA = 2,
    Move = 3,
    WEB = 5,
}

/// <summary>
/// 游戏条目类别
/// </summary>
public enum SubjectGameCategory : ushort
{
    Other = 0,
    Game = 4001,
    Software = 4002,
    DLC = 4003,
    BoardGame = 4005,
}

/// <summary>
/// 三次元条目类别
/// </summary>
public enum SubjectRealCategory : ushort
{
    /// <summary>
    /// 其他
    /// </summary>
    Other = 0,
    /// <summary>
    /// 日剧
    /// </summary>
    JapaneseDrama = 1,
    /// <summary>
    /// 欧美剧
    /// </summary>
    EuroAmericanDrama = 2,
    /// <summary>
    /// 华语剧
    /// </summary>
    ChineseDrama = 3,
    /// <summary>
    /// 电视剧
    /// </summary>
    TVSeries = 6001,
    /// <summary>
    /// 电影
    /// </summary>
    Movie = 6002,
    /// <summary>
    /// 演出
    /// </summary>
    Show = 6003,
    /// <summary>
    /// 综艺
    /// </summary>
    VarietyShow = 6004,
}