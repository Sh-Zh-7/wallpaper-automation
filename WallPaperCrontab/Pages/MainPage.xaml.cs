using Newtonsoft.Json;
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
using static WallPaperCrontab.TaskScheduler;

namespace WallPaperCrontab.Pages
{
    public partial class MainPage : Page
    {
        // 创建计划任务的常量
        private const string kTaskName = "WallPaperChangeTask";
        private const string kCreatorName = "SHZH";
        private const string kDescription = "MADE BY CSHARP";

        // 是否是第一次选择combobox
        static bool firstSelectTime = true;
        static bool firstSelectStyle = true;
        // 选择图片的路径
        static string selectedFilePath { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        // 浏览文件
        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
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
            selectedFilePath = filePath;
        }

        // 把浏览到的文件添加到json类实例的属性中
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.imagesAttr == null)
            {
                MessageBox.Show("空指针异常！");
            }
            else
            {
                GlobalVariable.imagesAttr.imagesPath.Add(selectedFilePath);
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

        // 获得时间模式
        private void TimeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstSelectTime)
            {
                GlobalVariable.timeSelectIndex = TimeSelect.SelectedIndex;
            }
            else
            {
                firstSelectTime = false;
            }
        }

        // 创建计划任务
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            // 先创建ImageAttr对象
            int imagesNumber = GlobalVariable.imagesAttr.imagesPath.Count;
            for (int i = 0; i < imagesNumber; ++i)
            {
                GlobalVariable.imagesAttr.imagesStyle.Add(GlobalVariable.timeSelectIndex);
            }
            // 导出成json
            string imageAttrJson = JsonConvert.SerializeObject(GlobalVariable.imagesAttr);
            if (Directory.Exists(@".\Config") == false)
            {
                Directory.CreateDirectory(@".\Config");
            }
            File.WriteAllText(@".\Config\image_attr.json", imageAttrJson);

            // 再创建TimeMode对象
            // 这里暂时赋值为0，表示以秒为单位
            // TODO: 这里还有很多功能可以添加
            int targetInterval = 3;
            switch (GlobalVariable.timeSelectIndex)
            {
                case 0: targetInterval = 7; break;
                case 1: targetInterval = 10; break;
            }
            TimeMode timeMode = new TimeMode(DateTime.Now, 0, targetInterval);
            // 导出成json
            string timeModeJson = JsonConvert.SerializeObject(timeMode);
            if (Directory.Exists(@".\Config") == false)
            {
                Directory.CreateDirectory(@".\Config");
            }
            File.WriteAllText(@".\Config\time_mode.json", timeModeJson);


            // 创建计划任务
            if (!SchTaskExt.IsExists(kTaskName))
            {
                var creator = kCreatorName;
                //计划任务名称
                var taskName = kTaskName;
                //执行的程序路径。
                var path = Environment.CurrentDirectory + @"\WallPaperChanger.exe";
                //计划任务执行的频率 PT1M一分钟  PT1H30M 90分钟
                var interval = "PT1M";
                //开始时间 请遵循 yyyy-MM-ddTHH:mm:ss 格式
                DateTime time = DateTime.Now;
                var startBoundary = time.GetDateTimeFormats('s')[0].ToString();
                var description = kDescription;
                _TASK_STATE state = SchTaskExt.CreateTaskScheduler(creator, taskName, path, interval, startBoundary, description);
            }
            else
            {
                MessageBox.Show("不能同时创建多个计划任务！！");
            }
        }

        // 删除计划任务
        private void End_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SchTaskExt.IsExists(kTaskName))
            {
                SchTaskExt.DeleteTask(kTaskName);
            } else
            {
                MessageBox.Show("不能删除未定义的计划任务！！");
            }
        }
    }

}
