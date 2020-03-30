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
    /// <summary>
    /// 交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {

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

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            // 接下来就是处理读取的图片问题了
            System.Windows.MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButton.OK);
        }

        /// <summary>
        /// 创建计划任务
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // TODO:不能同时创建多个计划任务
            if (!SchTaskExt.IsExists("WallPaperChangeTask"))
            {
                var creator = "Tonge";
                //计划任务名称
                var taskName = "WallPaperChangeTask";
                //执行的程序路径
                var path = "C:\\Windows\\System32\\calc.exe";
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
    }

}
