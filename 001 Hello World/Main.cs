using Fiddler;
using System.Windows.Forms;

namespace _001_Hello_World
{
    public class Main:  UserControl, IFiddlerExtension
    {
        public void OnBeforeUnload()
        { }

        public void OnLoad()
        {
            //新建一个Fiddler插件的page
            TabPage page = new TabPage("Heelo World");
            //将page加入Fiddler的tab选项卡中
            FiddlerApplication.UI.tabsViews.TabPages.Add(page);
            //输出Hello World
            FiddlerApplication.DoNotifyUser("Hello", "Hello World");
        }
    }
}
