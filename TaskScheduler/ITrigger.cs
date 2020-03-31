using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskScheduler
{
    /// <summary>
    /// 任务触发
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// 是否满足触发条件
        /// </summary>
        /// <param name="dateTime">要检测的时间</param>
        /// <returns></returns>
        bool Satisfied(DateTime dateTime);
    }
}
