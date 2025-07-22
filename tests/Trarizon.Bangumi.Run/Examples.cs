using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Requests.Entities;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Episodes;
using Trarizon.Bangumi.Api.Models.Persons;
using Trarizon.Bangumi.Api.Models.Subjects;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Run;
public static class Examples
{
#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时
    public static async Task Api(IBangumiClient client, CancellationToken cancellationToken)
    {
        // All apis returns `Task<BangumiApiResult>` or `Task<BangumiApiResult<T>>`
        // Which provides a `ThrowIfError` to convert it to a `Task` or `Task<T>`,
        // and error request will throw as `BangumiApiException`

        // Subjects

        var calendar = await client.GetCalendarAsync(cancellationToken)
            .ThrowIfError();

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
            Category = new(SubjectGameCategory.Game), // SubjectBook/Anime/Game/RealCategory, can implicit cast
            IsSeries = false,
            GamePlatform = "PC",
            Sort = GetSubjectsSortKind.Date,
            Year = 2018,
            Month = 6,
        };
        var subjectsPaged = await client.GetPagedSubjectsAsync(SubjectType.Anime, subjectsPagedQueries, cancellationToken: cancellationToken);
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

        var colSubjectsPaged = await client.GetPagedUserCollectionSubjectsAsync("Trarizon", SubjectType.Game, CollectionType.Collect);
        var colSubject = await client.GetUserCollectionSubjectAsync("Trarizon", 200763, cancellationToken);

        var updateColSubjectRequestBody = new UpdateUserCollectionSubjectRequestBody
        {
            Type = CollectionType.Collect,
            Rate = 0,
            EpisodeStatus = 26,
            VolumeStatus = 1,
            Comment = "Comment",
            IsPrivate = false,
            Tags = ["gal改"],
        };
        var addUpdateColSubjectResult = await client.AddOrUpdateUserCollectionSubjectAsync(363957, updateColSubjectRequestBody, cancellationToken);
        var updateColSubjectResult = await client.UpdateUserCollectionSubjectAsync(363957, updateColSubjectRequestBody, cancellationToken);

        var colSubjectEpsPaged = await client.GetPagedUserCollectionSubjectEpisodesAsync(363957, EpisodeType.Normal, cancellationToken: cancellationToken);
        var updateColSubjectEpRequestBody = new UpdateUserCollectionSubjectEpisodesRequestBody
        {
            Episodes = [1, 2, 3, 4, 5, 6],
            Type = EpisodeCollectionType.Collect,
        };
        var updateColSubjectEpsResult = await client.UpdateUserCollectionSubjectEpisodesAsync(363957, updateColSubjectEpRequestBody, cancellationToken);

        var colEp = await client.GetUserCollectionEpisodeAsync(1459757, cancellationToken);
        var updateColEpRequestBody = new UpdateUserCollectionEpisodeRequestBody
        {
            Type = EpisodeCollectionType.Collect,
        };
        var updateColEpResult = await client.UpdateUserCollectionEpisodeAsync(1459757, updateColEpRequestBody, cancellationToken);

        var colCharactersPaged = await client.GetPagedUserCollectionCharactersAsync("Trarizon", cancellationToken);
        var colCharacter = await client.GetUserCollectionCharacterAsync("Trarizon", 59846, cancellationToken);

        var colPersonsPaged = await client.GetPagedUserCollectionPersonsAsync("Trarizon", cancellationToken);
        var colPerson = await client.GetUserCollectionPersonAsync("Trarizon", 17796, cancellationToken);

        // Revisions

        var personRevisions = await client.GetPagedPersonRevisionsAsync(17796, cancellationToken: cancellationToken)
            .ThrowIfError();
        var personRevision = await client.GetPersonRevisionAsync(personRevisions.Datas[0].Id, cancellationToken);
        var characterRevs = await client.GetPagedCharacterRevisionsAsync(59846, cancellationToken: cancellationToken)
            .ThrowIfError();
        var characterRev = await client.GetCharacterRevisionAsync(characterRevs.Datas[0].Id, cancellationToken);
        var subjectRevs = await client.GetPagedSubjectRevisionsAsync(200763, cancellationToken: cancellationToken)
            .ThrowIfError();
        var subjectRev = await client.GetSubjectRevisionAsync(subjectRevs.Datas[0].Id, cancellationToken);
        var epRevs = await client.GetPagedEpisodeRevisionsAsync(363957, cancellationToken: cancellationToken)
            .ThrowIfError();
        var epRev = await client.GetEpisodeRevisionAsync(epRevs.Datas[0].Id, cancellationToken);

        // Index

        var updateIdxRequestBody = new UpdateIndexInfoRequestBody
        {
            Title = "Title",
            Description = "Description",
        };
        var createIdx = await client.CreateIndexAsync(updateIdxRequestBody, cancellationToken);
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

    public static void Ext(IBangumiClient client,CancellationToken cancellationToken)
    {
        // `SearchPagedXXXAsync` and `GetPagedXXXAsync` returns `PagedResult<T>`
        // Related `SearchXXX` and `GetXXX` will return a async collection
        // You can directly async iterate the collection or use LinqAsync
        //var searchSubjects = client.SearchSubjects(searchSubjectsRequestBody, cancellationToken: cancellationToken);
    }
}
