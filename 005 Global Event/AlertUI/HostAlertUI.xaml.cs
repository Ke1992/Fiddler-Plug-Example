using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _005_Global_Event.AlertUI
{
    /// <summary>
    /// HostAlertUI.xaml 的交互逻辑
    /// </summary>
    public partial class HostAlertUI : UserControl
    {
        public HostAlertUI()
        {
            InitializeComponent();
        }

        #region 鼠标点击事件
        private void addHostRule(object sender, MouseButtonEventArgs e)
        {
            string ip = this.ip.Text;
            string port = this.port.Text;
            string url = this.url.Text;

            if (ip.Length == 0)
            {
                Fiddler.FiddlerApplication.DoNotifyUser("请填写IP", "输入提示");
                return;
            }

            if (url.Length == 0)
            {
                Fiddler.FiddlerApplication.DoNotifyUser("请填写URL", "输入提示");
                return;
            }

            //添加UI
            Main.addHostRule(ip, port, url);

            //关闭弹框
            (this.Parent as Window).Close();
        }
        #endregion

        #region 输入框按键监听事件
        private void inputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addHostRule(null, null);
            }
        }
        #endregion
    }
}
