using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WallPaperCrontab.Pages
{
    /// <summary>
    /// Questions.xaml 的交互逻辑
    /// </summary>
    public partial class Questions : Page
    {
        public Questions()
        {
            InitializeComponent();
        }
        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


    }
}
