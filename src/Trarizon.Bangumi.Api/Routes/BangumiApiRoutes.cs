namespace Trarizon.Bangumi.Api.Routes;

internal static class BangumiApiRoutes
{
    private const string V0Url = "/v0";
    private const string V0SearchUrl = V0Url + "/search";

    public const string CharactersUrl = V0Url + "/characters";
    public const string SearchCharactersUrl = V0SearchUrl + "/characters";

    public const string UserCollectionsUrl = UsersUrl + "/-/collections";

    public const string EpisodesUrl = V0Url + "/episodes";

    public const string IndicesUrl = V0Url + "/indices";

    public const string PersonsUrl = V0Url + "/persons";
    public const string SearchPersonsUrl = V0SearchUrl + "/persons";

    public const string RevisionsUrl = V0Url + "/revisions";

    public const string CalendarUrl = "/calendar";
    public const string SubjectsUrl = V0Url + "/subjects";
    public const string SearchSubjectsUrl = V0SearchUrl + "/subjects";

    public const string UsersUrl = V0Url + "/users";
    public const string MeUrl = V0Url + "/me";
}
