using _007_Host_Mapping.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace _007_Host_Mapping
{
    partial class GlobalEvent
    {
        #region HOST事件
        //是否生效事件
        private void changeRuleEnable(object sender, MouseButtonEventArgs e)
        {
            int index = Convert.ToInt32((sender as Rectangle).Tag.ToString());
            //变更状态
            Main.changeRuleEnableByIndex(index);
        }
        //修改规则
        private void modifyRule(object sender, MouseButtonEventArgs e)
        {
            int index = Convert.ToInt32((sender as Label).Tag.ToString());
            //显示弹框
            AlertTool.showHostAlertUI(index);
        }
        #endregion

        #region 菜单点击事件
        private void handleRuleMenuClick(object sender, RoutedEventArgs e)
        {
            string type = (sender as MenuItem).Tag.ToString();
            object target = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as object;//获取点击源控件
            int index = (int)(target as Label).Tag;

            if (type == "modify")
            {
                modifyRule(target, null);
            }
            else if (type == "delete")
            {
                //删除数据
                Main.deleteRuleByIndex(index);
                //删除对应UI
                Main.container.deleteRuleFromUI(index);
            }
            else if (type == "up" || type == "down")
            {
                //移动对应的数据 
                Main.moveRuleByType(index, type);
                //移动对应的UI
                Main.container.moveRuleFromUI(index, type);
            }
        }
        #endregion
    }
}