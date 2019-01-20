样式设计思路：容器均使用内联样式，元素均使用静态样式，对于重复或者复杂的元素使用ControlTemplate来实现复用和简化代码
# 一、Icon
在前端样式实现方案中经常使用到图标字体，在WPF中也可以使用，不过在WPF中图标字体的承载对象是Canvas控件，下面主要介绍如何快捷的生成WPF中可以使用的图标字体
### 1、访问图标字体网站：https://icomoon.io/app/#/select
### 2、选择需要的图标，然后选择Generate SVG & More按钮
### 3、点击Preferences按钮，然后选择性修改图标尺寸，同时选择生成XAML格式，最后点击Download进行下载
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/003%20Static%20Page/001.png "配置面板")
### 4、将zip压缩包进行解压，打开XAML文件夹，编辑对应icon的wxml文件，拷贝其中的整个Canvas对象代码，其中修改Path的Fill可以修改图标颜色
# 二、布局
最外层使用Grid面板，来实现Top - Content布局；其中Content层再内嵌ScrollViewer控件来实现滚动界面
### 1、Grid面板
指定全局唯一标识Name为main，指定背景颜色为FFF0F3F6，最后再声明Top区域的高度为auto，Content区域的高度为100*
```
<Grid x:Name="main" Background="#FFF0F3F6">
    <Grid.RowDefinitions>
        <RowDefinition Height="auto" />
        <RowDefinition Height="100*" />
    </Grid.RowDefinitions>
</Grid>
```
### 2、Top区域
使用DockPanel面板作为容器，并设置Margin为30,0；其中左侧为标题和Icon，右侧为操作按钮；操作按钮使用WrapPanel来作为容器，然后右对齐
```
<DockPanel Grid.Row="0" Margin="30,0">
    <Label Style="{StaticResource top_style_title}">HOST 映射</Label>
    <Label Template="{StaticResource top_guide}" HorizontalAlignment="Left"></Label>
    <WrapPanel HorizontalAlignment="Right" VerticalAlignment="Center">
        <Label Style="{StaticResource top_style_operation}">新增</Label>
        <Label Style="{StaticResource top_style_operation_disable}">全禁</Label>
    </WrapPanel>
</DockPanel>
```
### 3、Content区域
使用ScrollViewer控件作为容器，设置Margin为30,0、VerticalScrollBarVisibility为Auto(当内容超过最大高度才显示滚动条)，内部再内嵌StackPanel，并指定Name为host
```
<ScrollViewer Grid.Row="1" VerticalScrollBarVisibility="Auto" Margin="30,0">
    <StackPanel x:Name="host" Margin="0,0,0,20"></StackPanel>
</ScrollViewer>
```
4、结果预览
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/003%20Static%20Page/002.png "Content区域")
# 三、列表区域
### 1、容器布局
列表内容主要包含IP、Url和选中框，因此分为3列，其中选中框宽度固定为40，IP和Url列分别占剩余的30%和70%宽度
```
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="30*"/>
    <ColumnDefinition Width="70*"/>
    <ColumnDefinition Width="40"/>
</Grid.ColumnDefinitions>
```
### 2、文本和选中框
文本内容使用Label嵌套TextBlock来实现超出自动显示省略号的功能，选中框则使用Rectangle和Canvas来实现，然后使用Visibility来控制选中态和未选中态的展示
```
<Label ToolTip="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Content}" Height="26" VerticalContentAlignment="Center">
    <TextBlock Style="{StaticResource content_style_text}"></TextBlock>
</Label>
<Rectangle Grid.Column="2" Visibility="Visible" Style="{StaticResource content_style_rect}"></Rectangle>
<Canvas Grid.Column="2" Width="12" Height="12" Visibility="Collapsed">
    <Path Fill="#FFEC8E72" Data="M10.125 1.5l-5.625 5.625-2.625-2.625-1.875 1.875 4.5 4.5 7.5-7.5z" />
</Canvas>
<Rectangle Grid.Column="2" Visibility="Collapsed" Style="{StaticResource content_style_rect_check}"></Rectangle>
```
3、结果预览
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/003%20Static%20Page/003.png "列表区域")