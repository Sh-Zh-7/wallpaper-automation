using System;
using System.Collections.Generic;
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
using WallPaperCrontab;

namespace WPFUI.Pages
{
    /// <summary>
    /// Wallpaper.xaml 的交互逻辑
    /// </summary>
    public partial class Wallpaper : Page
    {
        private int imageIndex;

        private void UpdateButton()
        {
            // 是否启用左边的按钮
            if (imageIndex == -1 || imageIndex == 0)
            {
                Left.IsEnabled = false;
            }
            else
            {
                Left.IsEnabled = true;
            }
            // 是否启用右边的按钮
            if (imageIndex == -1 || imageIndex == GlobalVariable.imagesAttr.imagesPath.Count - 1)
            {
                Right.IsEnabled = false;
            }
            else
            {
                Right.IsEnabled = true;
            }
            // 是否启用删除按钮
            if (imageIndex == -1)
            {
                Delete.IsEnabled = false;
            } else
            {
                Delete.IsEnabled = true;
            }
        }
        public Wallpaper()
        {
            InitializeComponent();
            if (GlobalVariable.imagesAttr.imagesPath.Count == 0)
            {
                imageIndex = -1;
                string filePath = Environment.CurrentDirectory + @"\Resources\blank.png";
                Display.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            } else
            {
                // 默认跳转到第一张图片
                imageIndex = 0;
                Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[imageIndex], UriKind.Absolute));
            }
            UpdateButton();
        }

        private void Left_Button_Click(object sender, RoutedEventArgs e)
        {
            if (imageIndex >= 1)
            {
                Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[--imageIndex], UriKind.Absolute));
            }
            UpdateButton();
        }

        private void Right_Button_Click(object sender, RoutedEventArgs e)
        {
            if (imageIndex < GlobalVariable.imagesAttr.imagesPath.Count - 1)
            {
                Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[++imageIndex], UriKind.Absolute));
            }
            UpdateButton();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (imageIndex != -1)
            {
                GlobalVariable.imagesAttr.imagesPath.RemoveAt(imageIndex);
                GlobalVariable.imagesAttr.imagesStyle.RemoveAt(imageIndex);
                // 更新图片显示的图片
                if (GlobalVariable.imagesAttr.imagesPath.Count == 0)
                {
                    imageIndex = -1;
                    string filePath = Environment.CurrentDirectory + @"\Resources\blank.png";
                    Display.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                } else if (imageIndex == GlobalVariable.imagesAttr.imagesPath.Count)
                {
                    Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[--imageIndex], UriKind.Absolute));
                } else
                {
                    Display.Source = new BitmapImage(new Uri(GlobalVariable.imagesAttr.imagesPath[imageIndex], UriKind.Absolute));
                }
            }
            UpdateButton();
        }
    }
}
