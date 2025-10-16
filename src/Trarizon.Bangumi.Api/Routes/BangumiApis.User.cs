using Trarizon.Bangumi.Api.Responses.Models.Users;
using Json = Trarizon.Bangumi.Api.Serialization.BangumiJsonSerializerContext;
using ApiRoutes = Trarizon.Bangumi.Api.Routes.BangumiApiRoutes;

namespace Trarizon.Bangumi.Api.Routes;

// src: https://github.com/bangumi/server/blob/master/web/handler/user
partial class BangumiApis
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<User> GetUserAsync(this IBangumiClient client, string userName, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonOrThrowAsync(
            $"{ApiRoutes.UsersUrl}/{userName}", Json.Default.User, cancellationToken);
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
        return client.GetHeadersLocationOrThrowAsync(
            $"{ApiRoutes.UsersUrl}/{userName}/avatar?type={avatarSize.ToQueryString()}",
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
        return client.GetFromJsonOrThrowAsync(
            ApiRoutes.MeUrl,
            Json.Default.UserSelf, cancellationToken);
    }
}
