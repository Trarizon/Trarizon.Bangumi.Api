namespace Trarizon.Bangumi.Api.Utilities;
internal static class EnumExtensions
{
    extension(DayOfWeek dayOfWeek)
    {
        public static DayOfWeek FromBangumiWeekdayId(int id) => id switch
        {
            1 => DayOfWeek.Monday,
            2 => DayOfWeek.Tuesday,
            3 => DayOfWeek.Wednesday,
            4 => DayOfWeek.Thursday,
            5 => DayOfWeek.Friday,
            6 => DayOfWeek.Saturday,
            7 => DayOfWeek.Sunday,
            _ => Throws.ThrowInvalidOperation<DayOfWeek>($"Invalid weekday id '{id}'."),
        };

        public int ToBangumiWeekdayId() => dayOfWeek switch
        {
            DayOfWeek.Sunday => 7,
            DayOfWeek.Monday => 1,
            DayOfWeek.Tuesday => 2,
            DayOfWeek.Wednesday => 3,
            DayOfWeek.Thursday => 4,
            DayOfWeek.Friday => 5,
            DayOfWeek.Saturday => 6,
            _ => Throws.ThrowUnknownEnumValue<int>(dayOfWeek),
        };

        public string ToEnglishShortName() => dayOfWeek switch
        {
            DayOfWeek.Sunday => "Sun",
            DayOfWeek.Monday => "Mon",
            DayOfWeek.Tuesday => "Tue",
            DayOfWeek.Wednesday => "Wed",
            DayOfWeek.Thursday => "Thu",
            DayOfWeek.Friday => "Fri",
            DayOfWeek.Saturday => "Sat",
            _ => Throws.ThrowUnknownEnumValue<string>(dayOfWeek),
        };

        public string ToChineseName() => dayOfWeek switch
        {
            DayOfWeek.Sunday => "星期日",
            DayOfWeek.Monday => "星期一",
            DayOfWeek.Tuesday => "星期二",
            DayOfWeek.Wednesday => "星期三",
            DayOfWeek.Thursday => "星期四",
            DayOfWeek.Friday => "星期五",
            DayOfWeek.Saturday => "星期六",
            _ => Throws.ThrowUnknownEnumValue<string>(dayOfWeek),
        };

        public string ToJapaneseName() => dayOfWeek switch
        {
            DayOfWeek.Sunday => "日曜日",
            DayOfWeek.Monday => "月曜日",
            DayOfWeek.Tuesday => "火曜日",
            DayOfWeek.Wednesday => "水曜日",
            DayOfWeek.Thursday => "木曜日",
            DayOfWeek.Friday => "金曜日",
            DayOfWeek.Saturday => "土曜日",
            _ => Throws.ThrowUnknownEnumValue<string>(dayOfWeek),
        };
    }
}
