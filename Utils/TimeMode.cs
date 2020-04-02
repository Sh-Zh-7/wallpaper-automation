using System;

namespace Utils
{
    // 时间的单位
    public enum TimeUnit : int
    {
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month
    }
    public class TimeMode
    {
        // 格式：MM-dd HH:mm:ss
        public string startTime { get; set; }
        // TimeMode中各个常数所代表的含义
        // Second: 0, Minute: 1
        // Hour: 2, Day: 3
        public int timeMode { get; set; }
        public double interval { get; set; }

        public TimeMode(DateTime dt, int timeMode, double interval)
        {
            this.startTime = dt.ToString("MM-dd HH:mm:ss");
            this.timeMode = timeMode;
            this.interval = interval;      
        }
    }
}
