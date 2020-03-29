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

namespace WallPaperCrontab.Pages
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        //public class DropDownItem
        //{
        //    public DropDownItem(int id, string name)
        //    {
        //        ID = id; Name = name;
        //    }
        //    public int ID { get; set; }
        //    public string Name { get; set; }
        //}
        public MainPage()
        {
            InitializeComponent();
            //List<DropDownItem> drop_down_items = new List<DropDownItem>();
            //drop_down_items.Add(new DropDownItem(1, "平铺"));
            //drop_down_items.Add(new DropDownItem(2, "拉伸"));
            //drop_down_items.Add(new DropDownItem(3, "居中"));
            //Display.ItemsSource = drop_down_items;
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
    }

}
