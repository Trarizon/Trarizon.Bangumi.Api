using Trarizon.Bangumi.Api.Models.UserModels;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;

namespace Trarizon.Bangumi.Api.Routes;
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
    public static Task<User> GetUserAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
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
    public static Task<Uri> GetUserAvatarUrlAsync(this IBangumiClient client, string userName, AvatarSize avatarSize, CancellationToken cancellationToken = default)
    {
        return client.GetHeadersLocationWhenStatusFoundOrThrowAsync(
            $"{UsersUrl}/{userName}/avatar?type={avatarSize.ToQueryString()}", 
            cancellationToken)!;
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<UserSelf> GetSelfAsync(this IBangumiClient client, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonWhenSuccessStatusCodeOrThrowAsync(
            MeUrl,
            Json.Default.UserSelf, cancellationToken);
    }
}
