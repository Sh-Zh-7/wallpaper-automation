using Quartz;
using System;

namespace TaskScheduler
{
    /// <summary>
    /// Cron表达式触发器
    /// </summary>
    public class CronTrigger : ITrigger
    {
        CronExpression _cronExpression;

        /// <summary>
        /// Cron表达式触发器
        /// </summary>
        /// <param name="cronExpression">Cron表达式</param>
        public CronTrigger(string cronExpression)
        {
            _cronExpression = new CronExpression(cronExpression);
        }

        /// <summary>
        /// 是否满足触发条件
        /// </summary>
        /// <param name="dateTime">要检测的时间</param>
        /// <returns></returns>
        public bool Satisfied(DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            return _cronExpression.IsSatisfiedBy(dt);
        }

    }
}
