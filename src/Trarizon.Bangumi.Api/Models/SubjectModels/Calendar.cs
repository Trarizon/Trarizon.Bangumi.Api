using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Trarizon.Bangumi.Api.Serialization.Converters.Model;

namespace Trarizon.Bangumi.Api.Models.SubjectModels;
/// <summary>
/// 每日放送
/// </summary>
[JsonConverter(typeof(CalendarJsonConverter))]
public sealed class Calendar
{
    private readonly ImmutableArray<CalendarDay> _days;

    /// <summary>
    /// 单日放送信息
    /// </summary>
    public ImmutableArray<CalendarDay> Days => _days;

    internal Calendar(ImmutableArray<CalendarDay> days)
    {
        Debug.Assert(!days.IsDefault);
        _days = days;
    }
}
