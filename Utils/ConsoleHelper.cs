using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ConsoleHelper
    {
        // 事实证明，再好的win32接口也比不过直接调用cmd命令行
        public static void ExecuteCmd(string cmd)
        {
            // 创建cmd进程
            using (System.Diagnostics.Process p = new System.Diagnostics.Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();
                // 向cmd窗口发送输入信息
                p.StandardInput.WriteLine(cmd);
                // p.WaitForExit();                            //等待程序执行完退出进程
                p.Close();
            }
        }

        // 获取窗口句柄
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 设置窗体的显示与隐藏
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        // 隐藏控制台
        public static void hideConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = string.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
            }
        }

        // 显示控制台
        public static void showConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = String.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
            }
        }

        // 利用Process类执行命令
        public static void StartProcess(string path)
        {
            // 进程的完整信息
            ProcessStartInfo pinfo = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                FileName = path
            };
            // 根据路径名创建进程
            Process p = Process.Start(pinfo);
        }

        // 查看Process类命令是否存在
        public static bool IsExistProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        } 

        // 关闭Process
        public static void CloseProcess(string processName)
        {
            Process targetProcess = Process.GetProcessesByName(processName)[0];
            targetProcess.Dispose();
            targetProcess.Close();
        }
    }
}
