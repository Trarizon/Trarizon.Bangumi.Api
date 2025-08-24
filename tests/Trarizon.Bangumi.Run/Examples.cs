using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Requests.Models;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Collections;

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

        BangumiApiResult<Calendar> calendarResult = await client.GetCalendarAsync(cancellationToken);
        Calendar calendar = await client.GetCalendarAsync(cancellationToken).Unwrap();

        // Subjects

        var searchSubjectsRequestBody = new SearchSubjectsRequestBody
        {
            Keyword = "Summer Pockets",
            Sort = SearchSubjectsSort.Match,
            Filter = new()
            {
                Types = [SubjectType.Game],
                MetaTags = ["Galgame"],
                Tags = ["key"],
                AirDate = [ComparisonFilter.Less(DateOnly.FromDateTime(DateTime.Now))],
                Rating = [ComparisonFilter.GreaterEquals(5)],
                Rank = [ComparisonFilter.GreaterEquals(100)],
                Nsfw = NsfwFilter.All,
            }
        };
        var searchPagedSubjects = await client.SearchPagedSubjectsAsync(searchSubjectsRequestBody, cancellationToken: cancellationToken);
        var subjectsPagedQueries = new GetSubjectsQueries
        {
            Category = new(SubjectGameCategory.Game),
            //Category = SubjectCategory.Game(),
            //Category = SubjectCategory.Game(SubjectGameCategory.Game),
            //Category = SubjectGameCategory.Game
            //Category = SubjectType.Game,
            IsSeries = false,
            GamePlatform = "PC",
            Sort = GetSubjectsSortKind.Date,
            Year = 2018,
            Month = 6,
        };
        var subjectsPaged = await client.GetPagedSubjectsAsync(subjectsPagedQueries, cancellationToken: cancellationToken);
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
        var collectCharacterResult = await client.CollectCharacterAsync(59846, cancellationToken);
        var uncollectCharacterResult = await client.UncollectCharacterAsync(59846, cancellationToken);

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
        var collectPersonResult = await client.CollectPersonAsync(17796, cancellationToken);
        var uncollectPersonResult = await client.UncollectPersonAsync(17796, cancellationToken);

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
        var addUpdateColSubjectResult = await client.AddOrUpdateUserSubjectCollectionAsync(363957, updateColSubjectRequestBody, cancellationToken);
        var updateColSubjectResult = await client.UpdateUserSubjectCollectionAsync(363957, updateColSubjectRequestBody, cancellationToken);

        var colSubjectEpsPaged = await client.GetPagedUserSubjectEpisodeCollectionsAsync(363957, EpisodeType.Normal, cancellationToken: cancellationToken);
        var updateColSubjectEpRequestBody = new UpdateUserSubjectEpisodeCollectionsRequestBody
        {
            Episodes = [1, 2, 3, 4, 5, 6],
            Type = EpisodeCollectionType.Collect,
        };
        var updateColSubjectEpsResult = await client.UpdateUserSubjectEpisodeCollectionsAsync(363957, updateColSubjectEpRequestBody, cancellationToken);

        var colEp = await client.GetUserCollectionEpisodeAsync(1459757, cancellationToken);
        var updateColEpRequestBody = new UpdateUserEpisodeCollectionRequestBody
        {
            Type = EpisodeCollectionType.Collect,
        };
        var updateColEpResult = await client.UpdateUserEpisodeCollectionAsync(1459757, updateColEpRequestBody, cancellationToken);

        var colCharactersPaged = await client.GetPagedUserCharacterCollectionsAsync("Trarizon", cancellationToken: cancellationToken);
        var colCharacter = await client.GetUserCharacterCollectionAsync("Trarizon", 59846, cancellationToken);

        var colPersonsPaged = await client.GetPagedUserPersonCollectionsAsync("Trarizon", cancellationToken: cancellationToken);
        var colPerson = await client.GetUserPersonCollectionAsync("Trarizon", 17796, cancellationToken);

        // Revisions

        var personRevisions = await client.GetPagedPersonRevisionsAsync(17796, cancellationToken: cancellationToken)
            .Unwrap();
        var personRevision = await client.GetPersonRevisionAsync(personRevisions.Datas[0].Id, cancellationToken);
        var characterRevs = await client.GetPagedCharacterRevisionsAsync(59846, cancellationToken: cancellationToken)
            .Unwrap();
        var characterRev = await client.GetCharacterRevisionAsync(characterRevs.Datas[0].Id, cancellationToken);
        var subjectRevs = await client.GetPagedSubjectRevisionsAsync(200763, cancellationToken: cancellationToken)
            .Unwrap();
        var subjectRev = await client.GetSubjectRevisionAsync(subjectRevs.Datas[0].Id, cancellationToken);
        var epRevs = await client.GetPagedEpisodeRevisionsAsync(363957, cancellationToken: cancellationToken)
            .Unwrap();
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
        var updateIdx = await client.UpdateIndexInfoAsync(79713, updateIdxRequestBody, cancellationToken);
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
        var rmvIdxSubject = await client.RemoveSubjectFromIndexAsync(79713, 200763, cancellationToken);
        var collectIdx = await client.CollectIndexAsync(79713, cancellationToken);
        var uncollectIdx = await client.UncollectIndexAsync(79713, cancellationToken);
    }
#pragma warning restore CS0618
#pragma warning restore BgmExprApi

    // Trarizon.Bangumi提供了一些扩展
    public static async Task Ext(IBangumiClient client, CancellationToken cancellationToken)
    {
        // 对于所有返回`Task<BangumiApiResult<PagedData<T>>>`的方法，提供了返回PageCollection<T>的对应方法
        // PageCollection<T>为异步集合，会依次读取每一页，遍历所有结果，可设定单页大小

        var queries = new GetSubjectsQueries() { Category = SubjectType.Game };

        PagedData<Subject> pagedSubjects = await client.GetPagedSubjectsAsync(queries, cancellationToken: cancellationToken).Unwrap();
        foreach (var subject in pagedSubjects.Datas) {
            Console.WriteLine(subject.Name);
        }

        PageCollection<Subject> subjects = client.GetSubjects(queries);
        await foreach (var subject in subjects) {
            Console.WriteLine(subject.Name);
        }
        // 可以通过GetPageAsync()获取单独一页
        var page = await subjects.GetPageAsync(cancellationToken: cancellationToken).Unwrap();
        // 提供了CountAsync()，LongCountAsync()，Take()，Skip()等AsyncLinq优化
        await foreach (var subject in subjects.Take(10)) {
            Console.WriteLine(subject.Name);
        }
    }
}
