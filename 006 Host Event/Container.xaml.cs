using _006_Host_Event.Tools;
using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace _006_Host_Event
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

        #region 全局事件
        //禁止所有Item
        private void disabledAllItem(object sender, MouseButtonEventArgs e)
        {
            Main.disabledAllHostFromData();
        }
        #endregion

        #region 私有方法(内部工具方法)
        //初始化Rule面板
        private void initRuleToUI()
        {
            ArrayList items = Main.mainData;

            //遍历添加Rule到UI
            for (int i = 0, len = items.Count; i < len; i++)
            {
                addHostRule(items[i] as HostModel);
            }
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
        //删除Rule控件
        public void deleteRuleFromUI(int index)
        {
            this.host.Children.RemoveAt(index);
        }
        //移动Rule控件
        public void moveRuleFromUI(int index, string moveType)
        {
            if (index <= 0 && moveType == "up")
            {
                Fiddler.FiddlerApplication.DoNotifyUser("已在最顶部", "无法上移");
                return;
            }

            StackPanel panel = this.host;

            if (index == panel.Children.Count - 1 && moveType == "down")
            {
                Fiddler.FiddlerApplication.DoNotifyUser("已在最底部", "无法下移");
                return;
            }

            //移除所有的Item
            panel.Children.Clear();
            //重新渲染所有的Item
            initRuleToUI();
        }
        #endregion
    }
}


