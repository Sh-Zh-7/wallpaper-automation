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

namespace WallPaperCrontab
{
    public enum Style : int
    {
        Tiled,
        Centered,
        Stretched
    }
    // 全局变量
    public class GlobalVariable
    {
        // 对象这个东西未定义也是会报错的
        public static ImagesAttr imagesAttr = new ImagesAttr();
        public static int timeSelectIndex { get; set; }
        public static int styleSelectIndex { get; set; }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Uri> allViews = new Dictionary<string, Uri>(); //包含所有页面
        public MainWindow()
        {
            if (Environment.GetEnvironmentVariable("WALLPAPER_CRONTAB", EnvironmentVariableTarget.User) == null)
            {
                Environment.SetEnvironmentVariable("WALLPAPER_CRONTAB", Environment.CurrentDirectory, EnvironmentVariableTarget.User);
            }
            InitializeComponent();
            // 果然还要主动地绑定事件
            MainPageLBI.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MainPageLBI_MouseLeftButtonDown), true);
            WallPaperLBI.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(WallPaperLBI_MouseLeftButtonDown), true);
            QuestionsLBI.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(QuestionsLBI_MouseLeftButtonDown), true);
            AboutUsLBI.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AboutUsLBI_MouseLeftButtonDown), true);
            OthersLBI.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OthersLBI_MouseLeftButtonDown), true);
            allViews.Add("main_page", new Uri("Pages/MainPage.xaml", UriKind.Relative));
            allViews.Add("wall_paper", new Uri("Pages/WallPaper.xaml", UriKind.Relative));
            allViews.Add("questions", new Uri("Pages/Questions.xaml", UriKind.Relative));
            allViews.Add("about_us", new Uri("Pages/AboutUs.xaml", UriKind.Relative));
            allViews.Add("others", new Uri("Pages/Others.xaml", UriKind.Relative));
            mainFrame.Navigate(allViews["main_page"]);
        }

        private void MainPageLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(allViews["main_page"]);
        }

        private void WallPaperLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(allViews["wall_paper"]);
        }

        private void QuestionsLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(allViews["questions"]);
        }

        private void AboutUsLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(allViews["about_us"]);
        }

        private void OthersLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(allViews["others"]);
        }
    }

}
