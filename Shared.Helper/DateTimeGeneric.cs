using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.Helper
{
    public static class DateTimeGeneric
    {
        /// <summary>
        /// Вычисляем возраст от даты
        /// </summary>
        /// <param name="needPostfix">Нужна ли вставка "назад"</param>
        public static string AgeString(this DateTime birthDateTime, bool needPostfix)
        {
            TimeSpan lifeTime = DateTime.UtcNow - birthDateTime;
            string ageString = string.Empty;

            int totalDays = lifeTime.TotalDays.ToInt32();
            if (needPostfix && totalDays <= 1)
            {
                var hoursAgo = Math.Round(lifeTime.TotalHours);
                if (hoursAgo == 0)
                    ageString = $"{lifeTime.TotalMinutes.ToInt32()} мин. назад";
                else
                    ageString = $"{hoursAgo} ч. назад";
            }
            else if (needPostfix && totalDays >= 2 && totalDays <= 31)
            {
                ageString = string.Format("{0} д. назад", totalDays);
            }
            else if (needPostfix && totalDays > 31 )
            {
                ageString = string.Format("{0} мес. назад", (totalDays / 30).ToInt32());
            }
            else if (!needPostfix && totalDays <= 30)
            {
                ageString = string.Format("{0} д.", totalDays);
            }
            else if (!needPostfix && totalDays > 30 && totalDays < 1095)
            {
                ageString = string.Format("{0} мес.", (totalDays / 30).ToInt32());
            }
            else if (!needPostfix)
            {
                ageString = string.Format("{0} г.", (totalDays / 365).ToInt32());
            }

            return ageString;
        }
        public static int TimeExpire(this TimeSpan timeExpire)
        {
            if (DateTime.UtcNow.TimeOfDay < timeExpire)
            {
                return (timeExpire.Add(new TimeSpan(0,0,1)) - DateTime.UtcNow.TimeOfDay).TotalSeconds.ToInt32();
            }

            return (DateTime.UtcNow.AddDays(1).Date + timeExpire.Add(new TimeSpan(0, 0, 1)) - DateTime.UtcNow).TotalSeconds.ToInt32();
        }
        public static DateTime? ToSmallDateTime(this DateTime? dateTime)
        {
            return dateTime.HasValue && dateTime.Value.Year > 2000 ? dateTime : null;
        }
        public static DateTime ToSmallDateTime(this DateTime dateTime)
        {
            return dateTime.Year > 2000 ? dateTime : new DateTime(2000, 1, 1);
        }
        public static DateTime LocalTime(this DateTime t, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(t, timeZone);
        }
    }
    /// <summary>
    /// Json конвертер, который переводит datetime в минуты
    /// </summary>
    public class MinutesEpochConverter : JsonConverter<DateTime>
    {
        DateTime from = new DateTime(2003, 1, 1);
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                from.AddMinutes(reader.GetString().ToInt32());

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue((dateTimeValue - from).TotalMinutes.ToInt32().ToString());
    }
}
