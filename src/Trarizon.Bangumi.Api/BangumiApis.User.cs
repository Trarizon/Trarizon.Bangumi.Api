using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Users;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    private const string UsersUrl = V0Url + "/users";
    private const string MeUrl = V0Url + "/me";

    public static Task<BangumiApiResult<User>> GetUserAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}", Json.Default.User, cancellationToken);
    }

    public static Task<BangumiApiResult<Uri>> GetUserAvatarUrlAsync(this IBangumiClient client, string userName, AvatarSize avatarSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/avatar?type={avatarSize.ToUrlQueryString()}", 
            cancellationToken)!;
    }

    public static Task<BangumiApiResult<UserSelf>> GetSelfAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            MeUrl,
            Json.Default.UserSelf, cancellationToken);
    }
}
