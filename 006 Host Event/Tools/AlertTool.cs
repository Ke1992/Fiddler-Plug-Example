using _006_Host_Event.AlertUI;
using System;
using System.Windows;

namespace _006_Host_Event.Tools
{
    class AlertTool
    {
        #region 暴露出去的方法
        //显示说明弹框
        public static void showExplainAlertUI()
        {
            ExplainAlertUI explainAlertUI = new ExplainAlertUI();
            //初始化窗体
            Window window = initWindow("explain", 450, 700);
            //设置window窗体内容
            window.Content = explainAlertUI;
            //显示窗体
            window.ShowDialog();
        }
        //显示Host弹框
        public static void showHostAlertUI(int index = -1)
        {
            HostAlertUI hostAlertUI = new HostAlertUI(index);
            //初始化窗体
            Window window = initWindow("host", 310);
            //设置window窗体内容
            window.Content = hostAlertUI;
            //自动聚焦
            hostAlertUI.ip.Focus();
            //显示窗体
            window.ShowDialog();
        }
        #endregion

        #region 内部方法
        //初始化窗体
        private static Window initWindow(string type, int height, int width = 500)
        {
            Window window = new Window();
            //设置宽和高
            window.Width = width;
            window.Height = height + 30;//状态栏的高度是30
            //去掉最小化、最大化按钮
            window.ResizeMode = 0;
            window.Title = type == "explain" ? "配置说明" : "Host配置";
            //设置显示在中间
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //返回对应的窗体
            return window;
        }
        #endregion
    }
}
