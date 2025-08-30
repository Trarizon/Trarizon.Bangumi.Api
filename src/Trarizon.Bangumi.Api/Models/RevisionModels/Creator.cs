using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;

namespace Trarizon.Bangumi.Api.Models.RevisionModels;
/// <summary>
/// 编辑记录的创建者
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/user.go#L63">
/// Creator
/// </see>
/// </remarks>
public sealed class Creator : IUserNamed
{
    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("username")]
    public string UserName { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("nickname")]
    public string NickName { get; internal set; }

#pragma warning disable CS8618      
    [JsonConstructor]
    internal Creator() { }
#pragma warning restore CS8618     
}
