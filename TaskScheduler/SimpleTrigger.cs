using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskScheduler
{
    /// <summary>
    /// 简单的触发器，可指定间隔秒数和执行次数
    /// </summary>
    public class SimpleTrigger : ITrigger
    {
        /// <summary>
        /// second
        /// </summary>
        private long _intervalSeconds = 0L;
        private int _repeatCount = 0;
        private int _triggerCount = 0;

        /// <summary>
        /// 简单触发器
        /// </summary>
        /// <param name="intervalSeconds">间隔秒数</param>
        /// <param name="repeatCount">重复次数，0为不断重复</param>
        public SimpleTrigger(long intervalSeconds, int repeatCount)
        {
            _intervalSeconds = intervalSeconds;
            _repeatCount = repeatCount;
        }

        /// <summary>
        /// 是否满足触发条件
        /// </summary>
        /// <param name="dateTime">要检测的时间</param>
        /// <returns></returns>
        public bool Satisfied(DateTime dateTime)
        {
            if (_repeatCount > 0 && _triggerCount >= _repeatCount)
            {
                return false;
            }
            var satisfied = ((long)(dateTime - DateTime.MinValue).TotalSeconds) % _intervalSeconds == 0;
            if (satisfied)
            {
                _triggerCount++;
            }
            return satisfied;
        }
    }
}
