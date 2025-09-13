using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters;

namespace Trarizon.Bangumi.Api.Responses.Models;
/// <summary>
/// 单日放送信息
/// </summary>
[DebuggerDisplay("{WeekDay} {Items.Length} subjects")]
public sealed class CalendarDay
{
    /// <summary>
    /// 放送星期
    /// </summary>
    [JsonInclude, JsonPropertyName("weekday")]
    [JsonConverter(typeof(DayOfWeekByNameTupleIdJsonConverter))]
    public DayOfWeek WeekDay { get; internal set; }

    /// <summary>
    /// 放送条目
    /// </summary>
    [JsonInclude, JsonPropertyName("items")]
    public ImmutableArray<CalendarSubject> Items { get; internal set; }

    [JsonConstructor]
    internal CalendarDay() { }
}
