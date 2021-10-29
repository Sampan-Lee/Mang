using System;

namespace Mang.Public.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 一年的第一天
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1).Date;
        }

        /// <summary>
        /// 当前日期所在月份的第一天
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return dateTime.AddDays(-(dateTime.Day) + 1).Date;
        }


        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.AddDays(1 - dateTime.Day).AddMonths(1).AddDays(-1).Date;
        }

        /// <summary>
        /// 与指定日期相差几个月
        /// </summary>
        /// <param name="dateTime">小的日期</param>
        /// <param name="date">大的日期</param>
        /// <returns></returns>
        public static int IntervalMonths(this DateTime dateTime, DateTime date)
        {
            return (date.Year * 12 - (12 - date.Month)) - (dateTime.Year * 12 - (12 - dateTime.Month));
        }

        public static DateTime ToDate(this object obj)
        {
            DateTime reval = DateTime.MinValue;
            if (obj != null && obj != DBNull.Value && DateTime.TryParse(obj.ToString(), out reval))
            {
                reval = Convert.ToDateTime(obj);
            }

            return reval;
        }

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <returns></returns>
        public static string GetWeek(DateTime dateTime = default)
        {
            if (dateTime == default) dateTime = DateTime.Now;
            string week;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                default:
                    week = "N/A";
                    break;
            }

            return week;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTime(this DateTime datetime)
        {
            var ts = (datetime.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            return ts;
        }

        /// <summary>
        /// 获取这个日期的最后一秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToDateLastTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
    }
}