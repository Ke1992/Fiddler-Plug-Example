using Fiddler;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace _002_Global_Style
{
    public class Main : UserControl, IFiddlerExtension
    {
        public static Container container;

        public void OnBeforeUnload()
        { }

        public void OnLoad()
        {
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
        }
    }
}
