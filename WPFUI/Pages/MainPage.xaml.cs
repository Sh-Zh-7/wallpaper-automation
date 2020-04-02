using Microsoft.Win32;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskScheduler;
using Utils;

namespace WallPaperCrontab.Pages
{
    public partial class MainPage : Page
    {
        #region DEPRECATED_USING_WINDOWS_TASKSHEDULER
        //// 创建计划任务的常量
        //private const string kTaskName = "WallPaperChangeTask";
        //private const string kCreatorName = "SHZH";
        //private const string kDescription = "MADE BY CSHARP";
        #endregion

        // 是否是第一次选择
        static bool ImageHasChanged = false;
        //static bool firstSelectTime = true;
        static bool firstSelectStyle = true;

        // 选择图片的路径
        static string selectedFilePath { get; set; }

        public MainPage()
        {
            InitializeComponent();
            // 所有路径都转为绝对路径
            //string absPath = GlobalVariable.lastImagePath[0] == '/' ? 
            //    Environment.CurrentDirectory + GlobalVariable.lastImagePath: GlobalVariable.lastImagePath;
            //Display.Source = new BitmapImage(new Uri(absPath, UriKind.Absolute));

            if (GlobalVariable.imagesAttr.imagesPath.Count == 0)
            {
                string absPath = Environment.CurrentDirectory + @"\Resources\blank.png";
                Display.Source = new BitmapImage(new Uri(absPath, UriKind.Absolute));
            } else
            {
                // 始终显示最后一个图片
                int index = GlobalVariable.imagesAttr.imagesPath.Count - 1;
                Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[index], UriKind.Absolute));
            }
        }

        #region DEPRECATED_USING_WINDOWS_TASKSHEDULER
        //// TODO：添加更为复杂的时间间隔格式
        //// 根据选定的时间间隔，获得计划任务的时间间隔格式
        //private string GetFrequency(int targetInterval, TimeUnit targetTimeUnit)
        //{
        //    switch (targetTimeUnit)
        //    {
        //        case TimeUnit.Minute: return "PT" + targetInterval.ToString() + "M";
        //        case TimeUnit.Hour: return "PT" + targetInterval.ToString() + "H";
        //        case TimeUnit.Day: return "P" + targetInterval.ToString() + "DT";
        //        case TimeUnit.Week: return "P" + targetInterval.ToString() + "WT";
        //    }
        //    return null;
        //}
        #endregion

        // 浏览文件
        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: 如果用户什么都怕没有选
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // 用分号分隔多个文件过滤的格式
                openFileDialog.Filter = "image files (*.png, *.jpg, *.bmp, *.jpeg, *.tif, *.tiff)|*.png; *.jpg; *.bmp; *.jpeg; *.tif; *.tiff|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 获得选取文件的路径
                    filePath = openFileDialog.FileName;
                }
            }
            if (filePath != "")
            {
                // 保存本次选择的路径
                selectedFilePath = filePath;
                // GlobalVariable.lastImagePath = filePath;
                // 根据文件的路径获得图片的内容
                ImageHasChanged = true;
                Display.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            }
        }

        // 把浏览到的文件添加到json类实例的属性中
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ImageHasChanged)
            {
                if (GlobalVariable.imagesAttr == null)
                {
                    MessageBox.Show("空指针异常！", "警告");
                }
                else
                {
                    GlobalVariable.imagesAttr.imagesPath.Add(selectedFilePath);
                    GlobalVariable.imagesAttr.imagesStyle.Add(GlobalVariable.styleSelectIndex);
                    MessageBox.Show("添加成功！", "成功");
                }
            }
            else
            {
                MessageBox.Show("请选择您的图片！", "警告");
            }
        }

        // 获得风格
        private void StyleSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstSelectStyle)
            {
                GlobalVariable.styleSelectIndex = StyleSelect.SelectedIndex;
            }
            else
            {
                firstSelectStyle = false;
            }
        }

        #region DEPRECATED_USING_WINDOWS_TASKSHEDULER
        //// 获得时间模式
        //private void TimeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (!firstSelectTime)
        //    {
        //        GlobalVariable.timeSelectIndex = TimeSelect.SelectedIndex;
        //    }
        //    else
        //    {
        //        firstSelectTime = false;
        //    }
        //}
        #endregion

        #region DEPRECATED_USING_WINDOWS_SERVICE
        //// 利用instsrv.exe和srvany.exe创建服务
        //private void CreateService()
        //{
        //    // 启动cmd
        //    System.Diagnostics.Process p = new System.Diagnostics.Process();
        //    p.StartInfo.FileName = "cmd.exe";
        //    p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
        //    p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        //    p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        //    p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        //    p.StartInfo.CreateNoWindow = true;//不显示程序窗口
        //    p.Start();//启动程序

        //    //向cmd窗口发送输入信息
        //    string prefix = Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User);
        //    string serve_cmd = prefix + @"\Service\instsrv.exe auto_wallpaper " + prefix + @"\Service\srvany.exe";
        //    p.StandardInput.WriteLine(serve_cmd);
        //    //p.WaitForExit();//等待程序执行完退出进程
        //    p.Close();
        //}

        //private void RegisterService()
        //{
        //    // 操作注册表
        //    string prefix = Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User);
        //    // 打开对应的注册表项
        //    RegistryKey key = Registry.LocalMachine;
        //    RegistryKey software = key.OpenSubKey(@"SYSTEM\ControlSet001\Services\auto_wallpaper", true);
        //    RegistryKey parameter = software.CreateSubKey(@"Parameters", true);
        //    // 建立三个字符串值
        //    parameter.SetValue("Application", prefix + @"\TaskSheduler.exe");
        //    parameter.SetValue("AppDirectory", prefix);
        //    parameter.SetValue("AppParameters", "");
        //}
        #endregion

        // 创建计划任务
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            // 判断是否开始
            if (GlobalVariable.imagesAttr.imagesPath.Count != 0)
            {
                if (ConsoleHelper.IsExistProcess("TaskScheduler"))
                {
                    MessageBox.Show("不能同时创建多个计划任务！！", "警告");
                }
                else
                {
                    string parameter = TextBox.Text;
                    bool isValid = CronExpression.IsValidExpression(parameter);

                    if (isValid)
                    {
                        // 给用户注册环境变量，暂停计划任务时删除
                        if (Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User) == null)
                        {
                            Environment.SetEnvironmentVariable("AUTO_WALLPAPER", Environment.CurrentDirectory, EnvironmentVariableTarget.User);
                        }
                        if (Environment.GetEnvironmentVariable("RUN_CHANGE_TEST", EnvironmentVariableTarget.User) == null)
                        {
                            Environment.SetEnvironmentVariable("RUN_CHANGE_TEST", "0", EnvironmentVariableTarget.User);
                        }
                        // 导出成json
                        string imageAttrJson = JsonConvert.SerializeObject(GlobalVariable.imagesAttr);
                        if (Directory.Exists(@".\Config") == false)
                        {
                            Directory.CreateDirectory(@".\Config");
                        }
                        File.WriteAllText(@".\Config\image_attr.json", imageAttrJson);

                        //// 将该进程设置为系统服务
                        //CreateService();
                        //RegisterService();

                        // 命令行开启该服务
                        // 把TextBox中的内容一同送到另一个进程
                        string prefix = Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User);
                        string cmd = prefix + "/TaskScheduler.exe -p \"" + parameter + "\"";
                        ConsoleHelper.ExecuteCmd(cmd);
                    }
                    else
                    {
                        MessageBox.Show("格式错误！！请重新输出格式", "警告");
                    }
                }
            } else
            {
                MessageBox.Show("您还没有选择图片！", "警告");
            }


            # region DEPRECATED_USING_WINDOWS_TASKSHEDULER
            //// 再创建TimeMode对象
            //// 这里暂时赋值为0，表示以秒为单位
            //// TODO: 这里还有很多功能可以添加
            //int targetInterval = -1;
            //TimeUnit targetTimeMode = new TimeUnit();
            //switch (GlobalVariable.timeSelectIndex)
            //{
            //    // TODO：继续修改名称
            //    case 0: // 1分钟
            //        targetInterval = 1;
            //        targetTimeMode = TimeUnit.Minute;
            //        break;
            //    case 1: // 15分钟
            //        targetInterval = 15;
            //        targetTimeMode = TimeUnit.Minute;
            //        break;
            //    case 2: // 1小时
            //        targetInterval = 1;
            //        targetTimeMode = TimeUnit.Hour;
            //        break;
            //    case 3: // 1天
            //        targetInterval = 1;
            //        targetTimeMode = TimeUnit.Day;
            //        break;
            //    case 4: // 1周
            //        targetInterval = 1;
            //        targetTimeMode = TimeUnit.Week;
            //        break;
            //}

            //TimeMode timeMode = new TimeMode(DateTime.Now, (int)targetTimeMode, targetInterval);
            //// 导出成json
            //string timeModeJson = JsonConvert.SerializeObject(timeMode);
            //if (Directory.Exists(@".\Config") == false)
            //{
            //    Directory.CreateDirectory(@".\Config");
            //}
            //File.WriteAllText(@".\Config\time_mode.json", timeModeJson);

            //// 创建计划任务
            //if (!SchTaskExt.IsExists(kTaskName))
            //{
            //    var creator = kCreatorName;
            //    //计划任务名称
            //    var taskName = kTaskName;
            //    //执行的程序路径。
            //    var path = Environment.CurrentDirectory + @"\WallPaperChanger.exe";
            //    //计划任务执行的频率 PT1M一分钟  PT1H30M 90分钟
            //    var interval = GetFrequency(targetInterval, targetTimeMode);
            //    //开始时间 请遵循 yyyy-MM-ddTHH:mm:ss 格式
            //    DateTime time = DateTime.Now;
            //    var startBoundary = time.GetDateTimeFormats('s')[0].ToString();
            //    var description = kDescription;
            //    _TASK_STATE state = SchTaskExt.CreateTaskScheduler(creator, taskName, path, interval, startBoundary, description);
            //}
            //else
            //{
            //    MessageBox.Show("不能同时创建多个计划任务！！");
            //}
#endregion
        }

        // 删除计划任务
        private void End_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ConsoleHelper.IsExistProcess("TaskScheduler"))
            {
                MessageBox.Show("不能删除未创建的进程！！", "警告");
            } else
            {
                // 删除环境变量
                if (Environment.GetEnvironmentVariable("AUTO_WALLPAPER", EnvironmentVariableTarget.User) != null)
                {
                    // 这里设置为空字符串是否能删除环境变量
                    Environment.SetEnvironmentVariable("AUTO_WALLPAPER", "", EnvironmentVariableTarget.User);
                }

                if (Environment.GetEnvironmentVariable("RUN_CHANGE_TEST", EnvironmentVariableTarget.User) != null)
                {
                    // 这里设置为空字符串是否能删除环境变量
                    Environment.SetEnvironmentVariable("RUN_CHANGE_TEST", "", EnvironmentVariableTarget.User);
                }
                // 终止进程
                // ConsoleHelper.CloseProcess("TaskScheduler");
                ConsoleHelper.ExecuteCmd("taskkill /F /IM TaskScheduler.exe");
            }

            #region DEPRECATED_USING_WINDOWS_TASKSHEDULER
            // TODO: 捕获异常
            //if (SchTaskExt.IsExists(kTaskName))
            //{
            //    SchTaskExt.DeleteTask(kTaskName);
            //} else
            //{
            //    MessageBox.Show("不能删除未定义的计划任务！！");
            //}
            #endregion
        }
    }

}
