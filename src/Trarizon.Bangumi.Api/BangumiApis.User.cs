using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.Users;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api;
partial class BangumiApis
{
    // src: https://github.com/bangumi/server/blob/master/web/handler/user
    
    private const string UsersUrl = V0Url + "/users";
    private const string MeUrl = V0Url + "/me";

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<User>> GetUserAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}", Json.Default.User, cancellationToken);
    }

    /// <summary>
    /// 获取用户头像
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="avatarSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<Uri>> GetUserAvatarUrlAsync(this IBangumiClient client, string userName, AvatarSize avatarSize, CancellationToken cancellationToken = default)
    {
        return client.GetRequestUriWhenSuccessStatusCodeAsync(
            $"{UsersUrl}/{userName}/avatar?type={avatarSize.ToUrlQueryString()}", 
            cancellationToken)!;
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<BangumiApiResult<UserSelf>> GetSelfAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeAsync(
            MeUrl,
            Json.Default.UserSelf, cancellationToken);
    }
}
