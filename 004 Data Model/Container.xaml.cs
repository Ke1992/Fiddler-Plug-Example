using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _004_Data_Model
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
        #endregion
    }
}
