using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _007_Host_Mapping.AlertUI
{
    /// <summary>
    /// HostAlertUI.xaml 的交互逻辑
    /// </summary>
    public partial class HostAlertUI : UserControl
    {
        private int _index;

        public HostAlertUI(int index = -1)
        {
            _index = index;

            InitializeComponent();

            //初始化输入框内容
            initInputText();
        }

        #region 初始化输入框内容
        private void initInputText()
        {
            //小于0代表是新增，直接返回
            if (_index < 0)
            {
                return;
            }

            //获取数据
            HostModel rule = Main.mainData[_index] as HostModel;

            //设置数据
            this.ip.Text = rule.IP;
            this.port.Text = rule.Port;
            this.url.Text = rule.Url;
        }
        #endregion

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

            if (_index >= 0)
            {
                //修改数据
                Main.modifyRuleByIndex(_index, ip, port, url);
            }
            else
            {
                //添加UI
                Main.addHostRule(ip, port, url);
            }

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