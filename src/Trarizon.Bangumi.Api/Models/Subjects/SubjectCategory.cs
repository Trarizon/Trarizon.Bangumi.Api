using Trarizon.Bangumi.Api.Attributes;

namespace Trarizon.Bangumi.Api.Models.Subjects;
[GoSource<ushort>("https://github.com/bangumi/server/blob/master/web/handler/subject/browse.go#L88")]
public readonly struct SubjectCategory
{
    private readonly ushort _value;

    internal SubjectCategoryKind Kind { get; }

    private SubjectCategory(SubjectCategoryKind kind, ushort value)
    {
        Kind = kind;
        _value = value;
    }

    public SubjectCategory(SubjectBookCategory category) : this(SubjectCategoryKind.Book, (ushort)category) { }
    public SubjectCategory(SubjectAnimeCategory category) : this(SubjectCategoryKind.Anime, (ushort)category) { }
    public SubjectCategory(SubjectGameCategory category) : this(SubjectCategoryKind.Game, (ushort)category) { }
    public SubjectCategory(SubjectRealCategory category) : this(SubjectCategoryKind.Real, (ushort)category) { }

    public static implicit operator SubjectCategory(SubjectBookCategory category) => new(category);
    public static implicit operator SubjectCategory(SubjectAnimeCategory category) => new(category);
    public static implicit operator SubjectCategory(SubjectGameCategory category) => new(category);
    public static implicit operator SubjectCategory(SubjectRealCategory category) => new(category);

    internal ushort ToQueryValue() => _value;
}

internal enum SubjectCategoryKind : ushort
{
    Book,
    Anime,
    Game,
    Real,
}

public enum SubjectBookCategory : ushort
{
    Other = 0,
    Comic = 1001,
    Novel = 1002,
    Artbook = 1003,
}

public enum SubjectAnimeCategory : ushort
{
    Other = 0,
    TV = 1,
    OVA = 2,
    Move = 3,
    WEB = 5,
}

public enum SubjectGameCategory : ushort
{
    Other = 0,
    Game = 4001,
    Software = 4002,
    DLC = 4003,
    BoardGame = 4005,
}

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