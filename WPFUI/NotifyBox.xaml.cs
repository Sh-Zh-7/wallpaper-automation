using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPFUI
{
    /// <summary>
    /// NotifyBox.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyBox : Window
    {
        public NotifyBox()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Size.Width - Width;
            Top = SystemParameters.WorkArea.Size.Height - Height;
        }

        async private void Storyboard_Completed(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            BeginStoryboard(FindResource("CloseStoryboard") as Storyboard);
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
            Close();
        }
        public string NotifyMessage { get; set; }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Message.Text = NotifyMessage;
        }
    }
}
