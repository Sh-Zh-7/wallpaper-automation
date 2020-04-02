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
using Utils;

namespace WallPaperCrontab
{
    // 全局变量
    public class GlobalVariable
    {
        // 对象这个东西未定义也是会报错的
        public static ImagesAttr imagesAttr = new ImagesAttr();
        public static string lastImagePath = @"/Resources/blank.png";
        public static int timeSelectIndex { get; set; }
        public static int styleSelectIndex { get; set; }
    }

    /// MainWindow.xaml 的交互逻辑
    public partial class MainWindow : Window
    {
        // 所有页面何其路径的映射
        private Dictionary<string, Uri> allViews = new Dictionary<string, Uri>();
        public MainWindow()
        {
            InitializeComponent();
            // 给各个导航栏绑定事件
            MainPageLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MainPageLBI_MouseLeftButtonDown), true);
            WallPaperLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(WallPaperLBI_MouseLeftButtonDown), true);
            QuestionsLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(QuestionsLBI_MouseLeftButtonDown), true);
            AboutUsLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(AboutUsLBI_MouseLeftButtonDown), true);
            OthersLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OthersLBI_MouseLeftButtonDown), true);
            // 初始化导航栏和对应页面的映射
            allViews.Add("main_page", new Uri("Pages/MainPage.xaml", UriKind.Relative));
            allViews.Add("wall_paper", new Uri("Pages/WallPaper.xaml", UriKind.Relative));
            allViews.Add("questions", new Uri("Pages/Questions.xaml", UriKind.Relative));
            allViews.Add("about_us", new Uri("Pages/AboutUs.xaml", UriKind.Relative));
            allViews.Add("others", new Uri("Pages/Others.xaml", UriKind.Relative));
            // 默认是main_page这个页面
            mainFrame.Navigate(allViews["main_page"]);
        }

        // 各个导航栏的点击切换事件
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
