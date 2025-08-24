using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Models.Abstractions;
using Trarizon.Bangumi.Api.Models.SubjectModels;

namespace Trarizon.Bangumi.Api.Models.IndexModels;
/// <summary>
/// 目录内条目
/// </summary>
/// <remarks>
/// src: <see href="https://github.com/bangumi/server/blob/master/web/res/subject.go#L292">
/// IndexSubjectV0
/// </see>
/// </remarks>
public sealed class IndexSubject : ISubject
{
    /// <summary>
    /// 条目添加时间
    /// </summary>
    [JsonInclude, JsonPropertyName("added_at")]
    public DateTimeOffset AddedTime { get; internal set; }

    /// <inheritdoc cref="Subject.Date"/>
    [JsonInclude, JsonPropertyName("date")]
    public string? Date { get; internal set; }

    /// <inheritdoc cref="Subject.Images"/>
    [JsonInclude, JsonPropertyName("images")]
    public SubjectImageSet Images { get; internal set; }

    /// <inheritdoc />
    [JsonInclude, JsonPropertyName("id")]
    public uint Id { get; internal set; }

    /// <inheritdoc cref="Subject.Name" />
    [JsonInclude, JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <inheritdoc cref="Subject.ChineseName" />
    [JsonInclude, JsonPropertyName("name_cn")]
    public string ChineseName { get; internal set; }

    /// <summary>
    /// 目录内条目评价
    /// </summary>
    [JsonInclude, JsonPropertyName("comment")]
    public string Comment { get; internal set; }

    /// <inheritdoc cref="Subject.InfoBox"/>
    [JsonInclude, JsonPropertyName("infobox")]
    public InfoBox InfoBox { get; internal set; }

    /// <inheritdoc cref="Subject.Type"/>
    [JsonInclude, JsonPropertyName("type")]
    public SubjectType Type { get; internal set; }

#pragma warning disable CS8618 
    [JsonConstructor]
    internal IndexSubject() { }
#pragma warning restore CS8618 
}
