using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler;

namespace WallPaperCrontab
{
    class TaskScheduler
    {
        public class SchTaskExt
        {
            public static void DeleteTask(string taskName)
            {
                TaskSchedulerClass ts = new TaskSchedulerClass();
                ts.Connect(null, null, null, null);
                ITaskFolder folder = ts.GetFolder("\\");
                folder.DeleteTask(taskName, 0);
            }
            public static IRegisteredTaskCollection GetAllTasks()
            {
                TaskSchedulerClass ts = new TaskSchedulerClass();
                ts.Connect(null, null, null, null);
                ITaskFolder folder = ts.GetFolder("\\");
                IRegisteredTaskCollection tasks_exists = folder.GetTasks(1);
                return tasks_exists;
            }
            public static bool IsExists(string taskName)
            {
                var isExists = false;
                IRegisteredTaskCollection tasks_exists = GetAllTasks();
                for (int i = 1; i <= tasks_exists.Count; i++)
                {
                    IRegisteredTask t = tasks_exists[i];
                    if (t.Name.Equals(taskName))
                    {
                        isExists = true;
                        break;
                    }
                }
                return isExists;
            }

            public static _TASK_STATE CreateTaskScheduler(string creator, string taskName, string path, string interval, string startBoundary, string description)
            {
                try
                {
                    if (IsExists(taskName))
                    {
                        DeleteTask(taskName);
                    }

                    // new scheduler
                    TaskSchedulerClass scheduler = new TaskSchedulerClass();
                    // pc-name/ip,username,domain,password
                    scheduler.Connect(null, null, null, null);
                    // get scheduler folder
                    ITaskFolder folder = scheduler.GetFolder("\\");


                    // set base attr 
                    ITaskDefinition task = scheduler.NewTask(0);
                    task.RegistrationInfo.Author = creator;
                    task.RegistrationInfo.Description = description;

                    // set trigger  (IDailyTrigger ITimeTrigger)
                    // 这里可以修改我们的任务到底是什么时候执行
                    ITimeTrigger tt = (ITimeTrigger)task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
                    // 这里我直接改成Duration
                    tt.Repetition.Interval = interval;// format PT1H1M==1小时1分钟 设置的值最终都会转成分钟加入到触发器
                    tt.StartBoundary = startBoundary;//start time

                    // set action
                    IExecAction action = (IExecAction)task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
                    action.Path = path;//计划任务调用的程序路径

                    task.Settings.ExecutionTimeLimit = "PT0S"; //运行任务时间超时停止任务吗? PTOS 不开启超时
                    task.Settings.DisallowStartIfOnBatteries = false;//只有在交流电源下才执行
                    task.Settings.RunOnlyIfIdle = false;//仅当计算机空闲下才执行

                    IRegisteredTask regTask = folder.RegisterTaskDefinition(
                        taskName, task,
                        (int)_TASK_CREATION.TASK_CREATE, null, //user
                        null, // password
                        _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN,
                    "");
                    IRunningTask runTask = regTask.Run(null);
                    return runTask.State;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
