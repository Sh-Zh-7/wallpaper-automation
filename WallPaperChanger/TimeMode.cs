using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPaperChanger
{
    class TimeMode
    {
        // 格式：MM-dd HH:mm:ss
        public string start_time { get; set; }
        // Second 0
        // Minute 1
        // Hour   2
        // Day    3
        public int time_mode { get; set; }
        public double interval { get; set; }

        public TimeMode(DateTime dt, int timeMode, double interval)
        {
            start_time = dt.ToString("MM-dd HH:mm:ss");
            time_mode = timeMode;
            this.interval = interval;       // C#里面没有指针
        }

        //public string StartTime
        //{
        //    get
        //    {
        //        return start_time;
        //    }
        //    set
        //    {
        //        // 这里的参数直接是字符串类型的了。。
        //        start_time = value.ToString("MM-dd HH:mm:ss");
        //    }
        //}
    }
}
