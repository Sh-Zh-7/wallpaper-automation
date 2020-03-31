using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WallPaperChanger
{
    public class Program
    {

        // 把json文件转化为字符串，方便paser解析
        public static string Json2Str(string path)
        {
            string result = "";
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader fileStream = new StreamReader(fs))
                {
                    string line;
                    while ((line = fileStream.ReadLine()) != null)
                    {
                        result += line;
                    }
                }
            }
            return result;
        }

        // 根据给定的时间和间隔获得图片的索引
        public static int GetIndex(DateTime start, TimeUnit timeUnit, double interval)
        {
            DateTime now = DateTime.Now;
            double diffTime = TimeDiff.GetTargetModeDiff(start, now, timeUnit);
            return (int)(diffTime / interval);
        }

        public static void SetWallPaper(
            string jsonPath,
            DateTime startTime, 
            TimeUnit timeUnit,
            double interval
            )
        {
            // 先获取从路径中得到的json文件
            string jsonStr = Json2Str(jsonPath);
            // 反序列化该json文件，得到各个属性（图片路径的List，壁纸的展示格式）
            ImagesAttr imagesAttr = JsonConvert.DeserializeObject<ImagesAttr>(jsonStr);
            // 计算相应日期，该显示哪一个图片
            int index = GetIndex(startTime, timeUnit, interval);
            // 使用LINQ的方式
            int length = imagesAttr.imagesPath.Count;
            string targetPath = imagesAttr.imagesPath[index % length];
            Style targetStyle = (Style)imagesAttr.imagesStyle[index % length];
            // 根据计算出来的图片和显示格式切换桌面
            WallpaperSetter.SetWallPaper(targetPath, targetStyle);
        }

        static public void Main(string[] args)
        {
            // 隐藏控制台窗口
            ConsoleHelper.hideConsole();
            // 从环境变量中获得路径名称
            string project_path = Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User);
            Console.WriteLine(project_path);
            // 解析对应的json文件
            // 先获取从路径中得到的json文件
            string jsonStr = Json2Str(project_path + "/Config/time_mode.json");
            // 反序列化
            TimeMode timeMode = JsonConvert.DeserializeObject<TimeMode>(jsonStr);
            DateTime startTime = DateTime.ParseExact(timeMode.startTime, "MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            TimeUnit mode = (TimeUnit)timeMode.timeMode;
            double interval = timeMode.interval;
            // 根据图片的具体属性修改壁纸
            SetWallPaper(project_path + "/Config/image_attr.json", startTime, mode, interval);
        }
    }
}
