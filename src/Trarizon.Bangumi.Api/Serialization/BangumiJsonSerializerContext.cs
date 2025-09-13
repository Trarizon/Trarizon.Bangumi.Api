using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Requests;
using Trarizon.Bangumi.Api.Responses;
using Trarizon.Bangumi.Api.Responses.Models;
using Trarizon.Bangumi.Api.Responses.Models.Collections;
using Trarizon.Bangumi.Api.Responses.Models.Revisions;
using Trarizon.Bangumi.Api.Responses.Models.Users;

namespace Trarizon.Bangumi.Api.Serialization;
#if DEBUG
[JsonSourceGenerationOptions(WriteIndented = true)]
#endif
[JsonSerializable(typeof(RequestError))]
[JsonSerializable(typeof(RequestDetail))]

[JsonSerializable(typeof(Calendar))]
[JsonSerializable(typeof(CalendarDay[]))]

[JsonSerializable(typeof(SearchSubjectsRequestBody))]
[JsonSerializable(typeof(PagedData<SearchResponsedSubject>))]
[JsonSerializable(typeof(PagedData<Subject>))]

[JsonSerializable(typeof(Subject))]
[JsonSerializable(typeof(InfoProperty[]))]
[JsonSerializable(typeof(InfoValue[]))]

[JsonSerializable(typeof(ImmutableArray<SubjectRelatedPerson>))]
[JsonSerializable(typeof(ImmutableArray<SubjectRelatedCharacter>))]
[JsonSerializable(typeof(ImmutableArray<SubjectRelatedSubject>))]

[JsonSerializable(typeof(PagedData<Episode>))]
[JsonSerializable(typeof(Episode))]

[JsonSerializable(typeof(SearchCharactersRequestBody))]
[JsonSerializable(typeof(PagedData<Character>))]
[JsonSerializable(typeof(Character))]
[JsonSerializable(typeof(ImmutableArray<CharacterRelatedSubject>))]
[JsonSerializable(typeof(ImmutableArray<CharacterRelatedPerson>))]

[JsonSerializable(typeof(SearchPersonsRequestBody))]
[JsonSerializable(typeof(PagedData<Person>))]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(ImmutableArray<PersonRelatedSubject>))]
[JsonSerializable(typeof(ImmutableArray<PersonRelatedCharacter>))]

[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(UserSelf))]

[JsonSerializable(typeof(PagedData<UserSubjectCollection>))]

[JsonSerializable(typeof(UpdateUserSubjectCollectionRequestBody))]

[JsonSerializable(typeof(PagedData<UserEpisodeCollection>))]
[JsonSerializable(typeof(UpdateUserSubjectEpisodeCollectionsRequestBody))]
[JsonSerializable(typeof(UpdateUserEpisodeCollectionRequestBody))]

[JsonSerializable(typeof(PagedData<UserCharacterCollection>))]
[JsonSerializable(typeof(PagedData<UserPersonCollection>))]

[JsonSerializable(typeof(PagedData<PersonRevision>))]
[JsonSerializable(typeof(PagedData<CharacterRevision>))]
[JsonSerializable(typeof(PagedData<SubjectRevision>))]
[JsonSerializable(typeof(PagedData<EpisodeRevision>))]

[JsonSerializable(typeof(BangumiIndex))]
[JsonSerializable(typeof(AddIndexRequestBody))]
[JsonSerializable(typeof(UpdateIndexInfoRequestBody))]
[JsonSerializable(typeof(AddIndexSubjectRequestBody))]
[JsonSerializable(typeof(UpdateIndexSubjectRequestBody))]
[JsonSerializable(typeof(PagedData<IndexSubject>))]
// Other 
[JsonSerializable(typeof(bool?))]
internal partial class BangumiJsonSerializerContext : JsonSerializerContext;
