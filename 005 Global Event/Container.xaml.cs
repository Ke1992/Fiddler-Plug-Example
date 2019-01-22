using _005_Global_Event.Tools;
using System.Windows.Controls;
using System.Windows.Input;

namespace _005_Global_Event
{
    /// <summary>
    /// Container.xaml 的交互逻辑
    /// </summary>
    public partial class Container : UserControl
    {
        public Container()
        {
            InitializeComponent();
        }

        #region Alert--事件
        //显示说明弹框
        private void showExplainAlertUI(object sender, MouseButtonEventArgs e)
        {
            AlertTool.showExplainAlertUI();
        }
        //增加Host
        private void addHost(object sender, MouseButtonEventArgs e)
        {
            AlertTool.showHostAlertUI();
        }
        #endregion

        #region 暴露出去的方法
        public void addHostRule(HostModel rule)
        {
            //创建UI对象
            Label label = new Label();
            //设置UI对象属性
            label.Template = Resources["content_host"] as ControlTemplate;
            label.DataContext = rule;
            //添加Rule
            this.host.Children.Add(label);
        }
        //禁止所有Item
        private void disabledAllItem(object sender, MouseButtonEventArgs e)
        {
            Main.disabledAllHostFromData();
        }
        #endregion
    }
}

