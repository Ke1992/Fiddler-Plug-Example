using _008_Other.Tools;
using Fiddler;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace _008_Other
{
    public class Main : UserControl, IFiddlerExtension
    {
        public static TabPage page;
        //UI
        public static Container container;
        //配置数据
        public static ArrayList mainData;

        #region 构造函数、Init函数(初始化UI界面)
        public Main()
        {
            //添加tab的icon图片进入列表
            FiddlerApplication.UI.tabsViews.ImageList.Images.Add("FiddlerExampleIcon", Properties.Resources.icon);
            //新建一个Fiddler插件的page
            page = new TabPage("PlugExample");
            //将page加入Fiddler的tab选项卡中
            FiddlerApplication.UI.tabsViews.TabPages.Add(page);
            //初始化icon
            page.ImageIndex = FiddlerApplication.UI.tabsViews.ImageList.Images.IndexOfKey("FiddlerExampleIcon");
        }
        private void Init()
        {
            //将WinForm和WPF联系起来(在WinForm中调用WPF)
            ElementHost element = new ElementHost();
            element.Child = container;
            element.Dock = DockStyle.Fill;

            //将WPF挂载对象添加到page中
            page.Controls.Add(element);
        }
        #endregion

        public void OnBeforeUnload()
        { }

        public void OnLoad()
        {
            //初始化文件夹
            DataTool.initFolder();
            //初始化配置数据
            mainData = DataTool.initConfigData();
            //新建UI对象
            container = new Container();

            //创建委托对象
            TabControlEventHandler tabSelectedEvent = null;
            tabSelectedEvent = delegate (object obj, TabControlEventArgs e)
            {
                if (e.TabPage == page)
                {
                    //初始化UI
                    Init();
                    //移除委托监听
                    FiddlerApplication.UI.tabsViews.Selected -= tabSelectedEvent;
                    FiddlerApplication.Log.LogString("FiddlerExample初始化完成！");
                }
            };

            //添加委托监听
            FiddlerApplication.UI.tabsViews.Selected += tabSelectedEvent;

            //监听请求响应之前
            FiddlerApplication.BeforeRequest += delegate (Session session)
            {
                FiddlerTool.handleRequest(session);
            };
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
            //重新写入文件
            DataTool.writeConfigToFile();
        }
        //禁止所有Host规则
        public static void disabledAllHostFromData()
        {
            //遍历更新数据
            for (int i = 0, len = mainData.Count; i < len; i++)
            {
                HostModel item = mainData[i] as HostModel;
                //更新数据
                item.Enable = false;
            }
            //重新写入文件
            DataTool.writeConfigToFile();
        }
        //变更Rule的Enable数据
        public static void changeRuleEnableByIndex(int index)
        {
            //获取数据
            HostModel rule = mainData[index] as HostModel;
            //变更状态
            rule.Enable = !rule.Enable;
            //重新写入文件
            DataTool.writeConfigToFile();
        }
        //修改Rule数据
        public static void modifyRuleByIndex(int index, string ip, string port, string url)
        {
            //获取规则
            HostModel rule = mainData[index] as HostModel;
            //更新数据
            rule.IP = ip;
            rule.Port = port;
            rule.Url = url;
            //重新写入文件
            DataTool.writeConfigToFile();
        }
        //删除Rule
        public static void deleteRuleByIndex(int index)
        {
            //删除对应的数据
            mainData.RemoveAt(index);

            //遍历修改下标值
            for (int i = 0, len = mainData.Count; i < len; i++)
            {
                HostModel item = mainData[i] as HostModel;
                item.Index = i;
            }

            //重新写入文件
            DataTool.writeConfigToFile();
        }
        //移动Rule
        public static void moveRuleByType(int index, string moveType)
        {
            //第一个数据
            if (index == 0 && moveType == "up")
            {
                return;
            }

            //最后一个数据
            if (index == mainData.Count - 1 && moveType == "down")
            {
                return;
            }

            //移动数据
            if (moveType == "up")
            {
                mainData.Insert(index - 1, mainData[index]);
                mainData.RemoveAt(index + 1);
            }
            else
            {
                mainData.Insert(index, mainData[index + 1]);
                mainData.RemoveAt(index + 2);
            }

            //遍历修改下标值
            for (int i = 0, len = mainData.Count; i < len; i++)
            {
                HostModel item = mainData[i] as HostModel;
                item.Index = i;
            }

            //重新写入文件
            DataTool.writeConfigToFile();
        }
        #endregion
    }
}