using System.Text.Json.Serialization;

namespace Trarizon.Bangumi.Api.Models.Revisions;
/// <summary>
/// 编辑记录的创建者
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/user.go#L63">
/// Creator
/// </see>
/// </remarks>
public sealed class Creator
{
    /// <inheritdoc cref="Users.User.UserName"/>
    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    /// <inheritdoc cref="Users.User.NickName"/>
    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal Creator() { }
#pragma warning restore CS8618     
}
