using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WallPaperChanger
{
    // 用来获取两个时间的不同格式
    public sealed class TimeDiff
    {

        // 注意TimeSpan的各种属性
        // 比如1:30, Hour就是1, Minute就是30
        private static TimeSpan GetDiffTime(DateTime startTime, DateTime endTime)
        {
            return endTime.Subtract(startTime);
        }

        private static double GetDiffSeconds(DateTime startTime, DateTime endTime)
        {
            TimeSpan diffTime = GetDiffTime(startTime, endTime);
            return diffTime.TotalSeconds;
        }

        private static double GetDiffMinutes(DateTime startTime, DateTime endTime)
        {
            TimeSpan diffTime = GetDiffTime(startTime, endTime);
            return diffTime.TotalMinutes;
        }

        private static double GetDiffHours(DateTime startTime, DateTime endTime)
        {
            TimeSpan diffTime = GetDiffTime(startTime, endTime);
            return diffTime.TotalHours;
        }

        private static double GetDiffDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan diffTime = GetDiffTime(startTime, endTime);
            return diffTime.TotalDays;
        }
        // 提供给外界的唯一一个函数接口，通过这个接口获得相应日期之差
        public static double GetTargetModeDiff(DateTime startTime, DateTime endTime, TimeUnit timeMode=TimeUnit.Second)
        {
            switch (timeMode)
            {
                case TimeUnit.Second:
                    return GetDiffSeconds(startTime, endTime);
                case TimeUnit.Minute:
                    return GetDiffMinutes(startTime, endTime);
                case TimeUnit.Hour:
                    return GetDiffHours(startTime, endTime);
                case TimeUnit.Day:
                    return GetDiffDays(startTime, endTime);
                default:
                    return -1;
            }
        }
    }
}
