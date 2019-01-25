# 全局样式
## 一、资源字典
一般项目肯定存在需要复用的样式和模板，在WPF中可以使用资源字典来实现公共样式的编写，然后在页面中引入资源字典即可实现样式继承
### 1、新建字典
首先添加System.Xaml引用，接着新建一个WPF，命名为GlobalStyle.xaml，然后手动删除掉GlobalStyle.xaml.cs，将GlobalStyle.xaml重新编写为资源字典文件格式，接着实现一个确定按钮样式
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/002%20Global%20Style/001.png "新建字典")
```
<ResourceDictionary  x:Class="_002_Global_Style.GlobalStyle"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
        xmlns:local="clr-namespace:_002_Global_Style"
        mc:Ignorable="d" >
    <!-- 确定按钮 -->
    <Style x:Key="alert_style_sure_btn" TargetType="Label">
        <Setter Property="Width" Value="120"></Setter>
        <Setter Property="Height" Value="40"></Setter>
        <Setter Property="Padding" Value="0"></Setter>
        <Setter Property="Margin" Value="0,20,0,0"></Setter>
        <Setter Property="FontSize" Value="14"></Setter>
        <Setter Property="Cursor" Value="Hand"></Setter>
        <Setter Property="Background" Value="#FFEC8E72"></Setter>
        <Setter Property="Foreground" Value="White"></Setter>
        <Setter Property="HorizontalContentAlignment" Value="Center"></Setter>
        <Setter Property="VerticalContentAlignment" Value="Center"></Setter>
    </Style>
</ResourceDictionary>
```
### 2、插件承载页面
新建一个WPF控件，命名为Container.xaml，作为插件内容的承载页面，然后引入全局资源字典，接着创建一个Label标签，指定Style为全局样式中的alert_style_sure_btn
```
<!--#region 样式、模板资源 -->
<UserControl.Resources>
    <ResourceDictionary Source="GlobalStyle.xaml"></ResourceDictionary>
</UserControl.Resources>
<!--#endregion-->

<StackPanel>
    <Label Style="{DynamicResource alert_style_sure_btn}">确定</Label>
</StackPanel>
```
### 3、预览结果
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/002%20Global%20Style/002.png "预览")
## 二、关联WPF
1、在Main.cs文件中，申明静态UI对象属性，同时在OnLoad中初始化
```
public static Container container;
//新建UI对象
container = new Container();
```
2、添加WindowsFormsIntegration引用，同时代码引入System.Windows.Forms.Integration库  
3、使用ElementHost实现在WinForm中调用WPF
```
//将WinForm和WPF联系起来(在WinForm中调用WPF)
ElementHost element = new ElementHost();
element.Child = container;
element.Dock = DockStyle.Fill;
```
4、将UI对象添加进Fiddler插件的Page中
```
//将WPF挂载对象添加到page中
page.Controls.Add(element);
```
## 三、打包调试结果
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/002%20Global%20Style/003.png "结果")
