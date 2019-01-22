using _004_Data_Model.Tools;
using Fiddler;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace _004_Data_Model
{
    public class Main : UserControl, IFiddlerExtension
    {
        //UI
        public static Container container;
        //配置数据
        public static ArrayList mainData;

        public void OnBeforeUnload()
        { }

        public void OnLoad()
        {
            //初始化配置数据
            mainData = new ArrayList();
            //新建UI对象
            container = new Container();

            //新建一个Fiddler插件的page
            TabPage page = new TabPage("PlugExample");
            //将page加入Fiddler的tab选项卡中
            FiddlerApplication.UI.tabsViews.TabPages.Add(page);

            //将WinForm和WPF联系起来(在WinForm中调用WPF)
            ElementHost element = new ElementHost();
            element.Child = container;
            element.Dock = DockStyle.Fill;

            //将WPF挂载对象添加到page中
            page.Controls.Add(element);

            //测试代码
            addHostRule("127.0.0.1", "8080", "www.example.com");
            addHostRule("127.0.0.1", "", "www.example.com");
            addHostRule("127.0.0.1", "3366", "www.example.com");
            //本地存储测试代码
            DataTool.initFolder();
            DataTool.writeConfigToFile();
        }

        #region 暴露出去的方法
        //新增Host规则
        public static void addHostRule(string ip, string port, string url)
        {
            //新建数据
            HostModel rule = new HostModel(mainData.Count, true, ip, port, url);
            //添加数据
            mainData.Add(rule);
            //添加UI
            container.addHostRule(rule);
        }
        #endregion
    }
}


