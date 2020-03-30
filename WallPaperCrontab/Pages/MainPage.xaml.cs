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
using static WallPaperCrontab.TaskScheduler;

namespace WallPaperCrontab.Pages
{
    public partial class MainPage : Page
    {

        static Boolean firstSelectTime = true;
        static Boolean firstSelectStyle = true;

        static string selectedFilePath { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    ////Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
            // 接下来就是处理读取的图片问题了
            //System.Windows.MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButton.OK);
            selectedFilePath = filePath;
        }

        /// <summary>
        /// 创建计划任务
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 创建两个实体类对象
            // 并将他们导出成json的形式

            // 先创建ImageAttr对象
            int images_number = GlobalVariable.imagesAttr.images_path.Count;
            for (int i = 0; i < images_number; ++i)
            {
                GlobalVariable.imagesAttr.images_style.Add(GlobalVariable.timeSelectIndex);
            }
            // 导出成json
            string image_attr_json = JsonConvert.SerializeObject(GlobalVariable.imagesAttr);
            if (Directory.Exists(@".\Config") == false)
            {
                Directory.CreateDirectory(@".\Config");
            }
            File.WriteAllText(@".\Config\image_attr.json", image_attr_json);

            // 再创建TimeMode对象
            // 这里暂时赋值为0，表示以秒为单位
            // TODO: 这里还有很多功能可以添加
            int target_interval = 3;
            switch (GlobalVariable.timeSelectIndex)
            {
                case 0: target_interval = 7; break;
                case 1: target_interval = 10; break;
            }
            TimeMode timeMode = new TimeMode(DateTime.Now, 0, target_interval);
            // 导出成json
            string timeModeJson = JsonConvert.SerializeObject(timeMode);
            if (Directory.Exists(@".\Config") == false)
            {
                Directory.CreateDirectory(@".\Config");
            }
            File.WriteAllText(@".\Config\time_mode.json", timeModeJson);


            // TODO：将常数转变为静态变量
            // 创建计划任务
            if (!SchTaskExt.IsExists("WallPaperChangeTask"))
            {
                var creator = "Tonge";
                //计划任务名称
                var taskName = "WallPaperChangeTask";
                //执行的程序路径
                // 这里似乎是直接把路径给改变了。。
                var path = Environment.CurrentDirectory + @"\WallPaperChanger.exe";
                //计划任务执行的频率 PT1M一分钟  PT1H30M 90分钟
                var interval = "PT1M";
                //开始时间 请遵循 yyyy-MM-ddTHH:mm:ss 格式
                DateTime time = DateTime.Now;
                var startBoundary = time.GetDateTimeFormats('s')[0].ToString();
                var description = "this is description";
                _TASK_STATE state = SchTaskExt.CreateTaskScheduler(creator, taskName, path, interval, startBoundary, description);
            } else
            {
                MessageBox.Show("不能同时创建多个计划任务！！");
            }
        }

        /// <summary>
        /// 删除计划任务
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SchTaskExt.IsExists("WallPaperChangeTask"))
            {
                SchTaskExt.DeleteTask("WallPaperChangeTask");
            } else
            {
                MessageBox.Show("不能删除未定义的计划任务！！");
            }
        }

        // 如果是本来就存在的时间的话只要在标签里面添加事件的类别就可以不全了
        private void TimeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 原来静态成员是这么用得
            if (!firstSelectTime)
            {
                 GlobalVariable.timeSelectIndex = TimeSelect.SelectedIndex;
            }
            else
            {
                firstSelectTime = false;
            }
        }

        private void  StyleSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 原来静态成员是这么用得
            if (!firstSelectStyle)
            {
                GlobalVariable.styleSelectIndex = StyleSelect.SelectedIndex;
            }
            else
            {
                firstSelectStyle = false;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.imagesAttr == null)
            {
                MessageBox.Show("空指针异常！");
            } else
            {
                GlobalVariable.imagesAttr.images_path.Add(selectedFilePath);
            }

        }
    }

}
