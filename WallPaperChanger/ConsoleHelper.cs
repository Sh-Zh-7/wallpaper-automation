using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WallPaperChanger
{
    class ConsoleHelper
    {    
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
    }
}
