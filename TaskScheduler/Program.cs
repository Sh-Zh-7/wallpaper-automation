using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.Threading;

namespace TaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            // 隐藏控制台窗口
            ConsoleHelper.hideConsole();
            Scheduler scheduler = new Scheduler();
            CronTrigger cronTrigger = new CronTrigger(args[1]);
            // 进程的完整信息
            System.Diagnostics.ProcessStartInfo pinfo = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            // 获取完整路径的命令
            string prefix = Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User);
            string fileName = prefix + "\\WallPaperChanger.exe";
            pinfo.FileName = fileName;
            scheduler.AddJob("Auto_WallPaper", cronTrigger, (sch, trigger) =>
            {
                //启动进程
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(pinfo);
                p.WaitForExit();
                p.Dispose();
                p.Close();
            });
            scheduler.Start();

            // 常驻内存成为进程
            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
