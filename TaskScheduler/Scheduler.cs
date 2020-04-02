using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TaskScheduler
{
    /// <summary>
    /// 任务调度器
    /// </summary>
    public class Scheduler : IDisposable
    {
        private Dictionary<ITrigger, List<Action<Scheduler, ITrigger>>> _taskDict;
        private Dictionary<string, Action<Scheduler, ITrigger>> _nameDict;
        private object lockObject = new object();

        System.Timers.Timer timer;

        /// <summary>
        /// 任务调度器
        /// </summary>
        public Scheduler()
        {
            _taskDict = new Dictionary<ITrigger, List<Action<Scheduler, ITrigger>>>();
            _nameDict = new Dictionary<string, Action<Scheduler, ITrigger>>();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var task in _taskDict)
            {
                if (task.Key.Satisfied(e.SignalTime))
                {
                    foreach (var job in task.Value)
                    {
                        new Task(() =>
                        {
                            job(this, task.Key);
                        }).Start();
                    }
                }
            }
        }

        /// <summary>
        /// 添加要作业
        /// </summary>
        /// <param name="jobName">工作唯一名称</param>
        /// <param name="trigger">触发器，对象可复用</param>
        /// <param name="job">作业，对象不可重用</param>
        public void AddJob(string jobName, ITrigger trigger, Action<Scheduler, ITrigger> job)
        {
            lock (lockObject)
            {
                List<Action<Scheduler, ITrigger>> jobList;
                if (_taskDict.TryGetValue(trigger, out jobList))
                {
                    jobList.Add(job);
                }
                else
                {
                    jobList = new List<Action<Scheduler, ITrigger>>() { job };
                    _taskDict.Add(trigger, jobList);
                }
                _nameDict.Add(jobName, job);
            }
        }

        /// <summary>
        /// 异常指定名称的任务
        /// </summary>
        /// <param name="jobName"></param>
        public void RemoveJob(string jobName)
        {
            lock (lockObject)
            {
                Action<Scheduler, ITrigger> jobObj;
                if (_nameDict.TryGetValue(jobName, out jobObj))
                {
                    foreach (var task in _taskDict)
                    {
                        foreach (var job in task.Value)
                        {
                            if (ReferenceEquals(job, jobObj))
                            {
                                task.Value.Remove(job);
                                if (task.Value.Count == 0)
                                {
                                    _taskDict.Remove(task.Key);
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 开始调度任务
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        /// <summary>
        /// 停止调度任务
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }



        #region dispose
        private bool disposed = false;
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                //清理托管资源
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
            //清理自己的非托管资源，这里好像没有
            disposed = true;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Scheduler()
        {
            Dispose(false);
        }
        #endregion

    }
}
