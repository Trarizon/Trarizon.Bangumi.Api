using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Routes;
using Trarizon.Bangumi.Api.Toolkit;
using Trarizon.Bangumi.Api.Toolkit.Collections;

namespace Trarizon.Bangumi.Run;
public static class Examples
{
#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时
    public static async Task Api(IBangumiClient client, CancellationToken cancellationToken)
    {
        // 所有API对应方法返回`Task<BangumiApiResult>` 或 `Task<BangumiApiResult<T>>`,
        // 这些返回值都可以使用`Unwrap`方法转换成可直接await或ConfigureAwait的`UnwrapAwaitable`或`UnwrapAwaitable<T>`
        // Unwrap后，API请求返回的错误会被包装为`BangumiApiException`抛出

        // http-api返回的错误会被包装为`BangumiApiException`抛出

        try {
            Calendar calendar = await client.GetCalendarAsync(cancellationToken);
        }
        catch (BangumiApiException ex) {
            Console.WriteLine(ex.HttpStatusCode);
            Console.WriteLine(ex.Error.Title);
            Console.WriteLine(ex.Error.Description);
            throw;
        }

        // Subjects

        // 搜索
        var searchSubjectsRequestBody = new SearchSubjectsRequestBody
        {
            Keyword = "Summer Pockets",
            Sort = SearchSubjectsSort.Match,
            Filter = new()
            {
                Types = [SubjectType.Game],
                MetaTags = ["Galgame"],
                Tags = ["key"],
                // ComparisonFilter<T>可通过ComparisonFilter类创建，直接new也行
                AirDate = [ComparisonFilter.Less(DateOnly.FromDateTime(DateTime.Now))],
                Rating = [ComparisonFilter.GreaterEquals(5)],
                // ISpanParsable<T>和IParsable<T>提供了Parse和TryParse方法
                // C#14也可以使用ComparisonFilter<T>.Parse()调用
                Rank = [ComparisonFilter.Parse<int>(">100")],
                Nsfw = NsfwFilter.All,
            }
        };
        var searchPagedSubjects = await client.SearchPagedSubjectsAsync(searchSubjectsRequestBody, cancellationToken: cancellationToken);

        var subjectsPagedQuery = new GetSubjectsQuery
        {
            // SubjectCategory提供了一系列静态方法创建值
            // - SubjectCategory.Game() : 构造游戏类型条目分类，无具体分类
            // - SubjectCategory.Game(SubjectGameCategory.Game) : 构造游戏类型条目分类，指定具体分类
            // - 从SubjectGame/Anime/Book/RealCategory中隐式转换
            // - 从SubjectType中隐式转换，构造无具体分类的条目分类
            Category = new(SubjectGameCategory.Game),
            IsSeries = false,
            GamePlatform = "PC",
            Sort = GetSubjectsSortKind.Date,
            Year = 2018,
            Month = 6,
        };
        var subjectsPaged = await client.GetPagedSubjectsAsync(subjectsPagedQuery, cancellationToken: cancellationToken);

        var subject = await client.GetSubjectAsync(200763u, cancellationToken);
        var subjectImageUrl = await client.GetSubjectImageUrlAsync(200763u, SubjectImageSize.Common, cancellationToken);
        var subjectPersons = await client.GetSubjectRelatedPersonsAsync(200763u, cancellationToken);
        var subjectCharacters = await client.GetSubjectRelatedCharactersAsync(200763u, cancellationToken);
        var subjectSubjects = await client.GetSubjectRelatedSubjectsAsync(200763u, cancellationToken);

        // Episodes

        var epsPaged = await client.GetPagedEpisodesAsync(363957u, EpisodeType.Normal, cancellationToken: cancellationToken);
        var ep = await client.GetEpisodeAsync(363957u, cancellationToken);

        // Characters

        var searchCharactersRequestBody = new SearchCharactersRequestBody
        {
            Keyword = "しろは",
            Filter = new()
            {
                Nsfw = NsfwFilter.All,
            }
        };
        var searchCharacterPaged = await client.SearchPagedCharactersAsync(searchCharactersRequestBody, cancellationToken: cancellationToken);
        var character = await client.GetCharacterAsync(59846, cancellationToken);
        var characterImageUrl = await client.GetCharacterImageUrlAsync(59846, PersonImageSize.Small, cancellationToken);
        var characterSubjects = await client.GetCharacterRelatedSubjectAsync(59846, cancellationToken);
        var characterPersons = await client.GetCharacterRelatedPersonAsync(59846, cancellationToken);
        await client.CollectCharacterAsync(59846, cancellationToken);
        await client.UncollectCharacterAsync(59846, cancellationToken);

        // Persons

        var searchPersonRequestBody = new SearchPersonsRequestBody
        {
            Keyword = "しらたま",
            Filter = new()
            {
                Careers = [PersonCareer.Seiyu],
            }
        };
        var searchPersonsPaged = await client.SearchPagedPersonsAsync(searchPersonRequestBody, cancellationToken: cancellationToken);
        var person = await client.GetPersonAsync(17796, cancellationToken);
        var personImageUrl = await client.GetPersonImageUrlAsync(17796, PersonImageSize.Small, cancellationToken);
        var personSubjects = await client.GetPersonRelatedSubjectsAsync(17796, cancellationToken);
        var personCharacters = await client.GetPersonRelatedSubjectsAsync(17796, cancellationToken);
        await client.CollectPersonAsync(17796, cancellationToken);
        await client.UncollectPersonAsync(17796, cancellationToken);

        // User

        var user = await client.GetUserAsync("Trarizon", cancellationToken);
        var userAvatarUrl = await client.GetUserAvatarUrlAsync("Trarizon", AvatarSize.Small, cancellationToken);
        var me = await client.GetSelfAsync(cancellationToken);

        // Collections

        var colSubjectsPaged = await client.GetPagedUserSubjectCollectionsAsync("Trarizon", SubjectType.Game, SubjectCollectionType.Collect);
        var colSubject = await client.GetUserSubjectCollectionAsync("Trarizon", 200763, cancellationToken);

        var updateColSubjectRequestBody = new UpdateUserSubjectCollectionRequestBody
        {
            Type = SubjectCollectionType.Collect,
            Rate = 0,
            EpisodeStatus = 26,
            VolumeStatus = 1,
            Comment = "Comment",
            IsPrivate = false,
            Tags = ["gal改"],
        };
        await client.AddOrUpdateUserSubjectCollectionAsync(363957, updateColSubjectRequestBody, cancellationToken);
        await client.UpdateUserSubjectCollectionAsync(363957, updateColSubjectRequestBody, cancellationToken);

        var colSubjectEpsPaged = await client.GetPagedUserSubjectEpisodeCollectionsAsync(363957, EpisodeType.Normal, cancellationToken: cancellationToken);
        var updateColSubjectEpRequestBody = new UpdateUserSubjectEpisodeCollectionsRequestBody
        {
            EpisodeIds = [1, 2, 3, 4, 5, 6],
            Type = EpisodeCollectionType.Collect,
        };
        await client.UpdateUserSubjectEpisodeCollectionsAsync(363957, updateColSubjectEpRequestBody, cancellationToken);

        var colEp = await client.GetUserCollectionEpisodeAsync(1459757, cancellationToken);
        var updateColEpRequestBody = new UpdateUserEpisodeCollectionRequestBody
        {
            Type = EpisodeCollectionType.Collect,
        };
        await client.UpdateUserEpisodeCollectionAsync(1459757, updateColEpRequestBody, cancellationToken);

        var colCharactersPaged = await client.GetPagedUserCharacterCollectionsAsync("Trarizon", cancellationToken: cancellationToken);
        var colCharacter = await client.GetUserCharacterCollectionAsync("Trarizon", 59846, cancellationToken);

        var colPersonsPaged = await client.GetPagedUserPersonCollectionsAsync("Trarizon", cancellationToken: cancellationToken);
        var colPerson = await client.GetUserPersonCollectionAsync("Trarizon", 17796, cancellationToken);

        // Revisions

        var personRevisions = await client.GetPagedPersonRevisionsAsync(17796, cancellationToken: cancellationToken);
        var personRevision = await client.GetPersonRevisionAsync(personRevisions.Datas[0].Id, cancellationToken);
        var characterRevs = await client.GetPagedCharacterRevisionsAsync(59846, cancellationToken: cancellationToken);
        var characterRev = await client.GetCharacterRevisionAsync(characterRevs.Datas[0].Id, cancellationToken);
        var subjectRevs = await client.GetPagedSubjectRevisionsAsync(200763, cancellationToken: cancellationToken);
        var subjectRev = await client.GetSubjectRevisionAsync(subjectRevs.Datas[0].Id, cancellationToken);
        var epRevs = await client.GetPagedEpisodeRevisionsAsync(363957, cancellationToken: cancellationToken);
        var epRev = await client.GetEpisodeRevisionAsync(epRevs.Datas[0].Id, cancellationToken);

        // Index

        var addIdxRequestBody = new AddIndexRequestBody
        {
            Title = "Title",
            Description = "Description",
        };
        var updateIdxRequestBody = addIdxRequestBody.ToUpdateIndexInfoRequestBody();
        var createIdx = await client.CreateIndexAsync(addIdxRequestBody, cancellationToken);
        var index = await client.GetIndexAsync(79713, cancellationToken);
        await client.UpdateIndexInfoAsync(79713, updateIdxRequestBody, cancellationToken);
        var idxSubject = await client.GetPagedIndexSubjectsAsync(79713, SubjectType.Game, cancellationToken: cancellationToken);
        var addIdxSubjectRequestBody = new AddIndexSubjectRequestBody
        {
            SubjectId = 200763,
            Sort = 0,
            Comment = "Comment",
        };
        var addIdxSubject = await client.AddSubjectToIndexAsync(79713, addIdxSubjectRequestBody, cancellationToken);
        var updIdxSubjectRequestBody = new UpdateIndexSubjectRequestBody
        {
            Sort = 0,
            Comment = "Comment",
        };
        var updateIdxSubject = await client.UpdateIndexSubjectAsync(79713, 200763, updIdxSubjectRequestBody, cancellationToken);
        await client.RemoveSubjectFromIndexAsync(79713, 200763, cancellationToken);
        await client.CollectIndexAsync(79713, cancellationToken);
        await client.UncollectIndexAsync(79713, cancellationToken);
    }
#pragma warning restore CS0618
#pragma warning restore BgmExprApi

    // 关于分页数据
    public static async Task PagedDataExt(IBangumiClient client, CancellationToken cancellationToken)
    {
        // GetPagedXXXAsync返回PagedData<XXX>，数据结构等同API页的Paged[XXX]

        var queries = new GetSubjectsQuery() { Category = SubjectType.Game };

        PagedData<Subject> pagedSubjects = await client.GetPagedSubjectsAsync(queries, cancellationToken: cancellationToken);
        foreach (var subject in pagedSubjects.Datas) {
            Console.WriteLine(subject.Name);
        }

        // 在Trarizon.Bangumi.Api.Toolkit中，
        // 所有GetPagedXXXAsync有对应的GetXXX方法，返回AsyncPagedCollection<XXX>
        // 该集合实现了IAsyncEnumerable<XXX>，提供了异步遍历所有搜索结果的功能。

        AsyncPagedDataCollection<Subject> subjects = client.GetSubjects(queries);
        // 搜索结果可能有很多数据，这种情况不推荐遍历所有数据，请自行break或使用Linq筛选
        await foreach (var subject in subjects) {
            Console.WriteLine(subject.Name);
        }
        // 提供了CountAsync()，LongCountAsync()，Take()，Skip()类Linq方法
        await foreach (var subject in subjects.Take(10)) {
            Console.WriteLine(subject.Name);
        }
    }

    // 关于InfoBox
    public static void AboutInfoBox(Subject subject)
    {
        // InfoBox是个涉及union的较复杂结构，该库中将其统一为了一个Dictionary<string, (string, string)[]>
        InfoBox infoBox = subject.InfoBox;

        // InfoBox是个底层为InfoProperty[]的字典，提供了几种基本获取数据段方式
        // InfoProperty是(string, InfoValues)的键值对
        InfoProperty firstProp = infoBox[0];
        InfoValues firstVals = firstProp.Values;
        InfoValues vals = infoBox["key"];
        infoBox.TryGetValue("key", out var tryVals);

        // InfoValues为InfoValue[]，其底层string和InfoValue[]的union
        InfoValue infoValue = vals[0];
        Console.WriteLine($"{infoValue.Key ?? null} {infoValue.Value}");
        // 可以通过GetRawXXX()获取其实际存储的表示
        if (vals.IsRawValueString())
            _ = vals.GetRawStringValue();
        else
            _ = vals.GetRawPairsValue();
    }
}
