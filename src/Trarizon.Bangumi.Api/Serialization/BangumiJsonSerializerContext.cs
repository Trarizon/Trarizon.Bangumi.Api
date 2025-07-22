using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Characters;
using Trarizon.Bangumi.Api.Models.Episodes;
using Trarizon.Bangumi.Api.Models.Indices;
using Trarizon.Bangumi.Api.Models.Persons;
using Trarizon.Bangumi.Api.Models.Revisions;
using Trarizon.Bangumi.Api.Models.Subjects;
using Trarizon.Bangumi.Api.Models.Users;

namespace Trarizon.Bangumi.Api.Serialization;
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(RequestError))]
[JsonSerializable(typeof(RequestDetail))]

[JsonSerializable(typeof(Calendar))]
[JsonSerializable(typeof(CalendarDay[]))]

[JsonSerializable(typeof(SearchSubjectsRequestBody))]
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
[JsonSerializable(typeof(PagedData<PersonActor>))]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(ImmutableArray<PersonRelatedSubject>))]
[JsonSerializable(typeof(ImmutableArray<PersonRelatedCharacter>))]

[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(UserSelf))]

[JsonSerializable(typeof(PagedData<UserCollectionSubject>))]

[JsonSerializable(typeof(UpdateUserCollectionSubjectRequestBody))]

[JsonSerializable(typeof(PagedData<UserCollectionEpisode>))]
[JsonSerializable(typeof(UpdateUserCollectionSubjectEpisodesRequestBody))]
[JsonSerializable(typeof(UpdateUserCollectionEpisodeRequestBody))]

[JsonSerializable(typeof(PagedData<UserCollectionCharacter>))]
[JsonSerializable(typeof(PagedData<UserCollectionPerson>))]

[JsonSerializable(typeof(PagedData<Revision>))]
[JsonSerializable(typeof(PersonRevision))]
[JsonSerializable(typeof(CharacterRevision))]
[JsonSerializable(typeof(SubjectRevision))]
[JsonSerializable(typeof(EpisodeRevision))]

[JsonSerializable(typeof(BangumiIndex))]
[JsonSerializable(typeof(UpdateIndexInfoRequestBody))]
[JsonSerializable(typeof(AddIndexSubjectRequestBody))]
[JsonSerializable(typeof(UpdateIndexSubjectRequestBody))]
// Other 
[JsonSerializable(typeof(bool?))]
internal partial class BangumiJsonSerializerContext : JsonSerializerContext;
