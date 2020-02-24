# Hello World
## 一、环境准备
Visual Studio 2015、Fiddler 5.0
## 二、新建工程
### 1、创建项目
选择新建工程，选择类库，因为Fiddler5.0使用的是.Net4.6.1，因此选择.Net4.6.1
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/001.png "创建项目")
### 2、添加Fiddler的类库
项目右键选择添加引用，点击浏览，然后找到Fiddler的安装地址选择Fiddler.exe添加即可
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/002.png "添加引用")
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/003.png "浏览")
### 3、添加Fiddler版本要求
打开AssemblyInfo.cs，增加要求的Fiddler版本信息，因为使用的是Fiddler5.0，因此填写版本号支持Fiddler5.0以上（这里理论上填写4.0也可以，因为Fiddler从4.0开始就使用.Net4.0的架构了）
```
[assembly: Fiddler.RequiredVersion("5.0.0.0")]
```
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/004.png "版本要求")
## 三、Hello Word
1、新建Main.cs文件，并添加public前缀  
2、继承IFiddlerExtension类，并且实现OnLoad和OnBeforeUnload函数
```
public class Main : IFiddlerExtension
{
    public void OnBeforeUnload()
    {}

    public void OnLoad()
    {}
}
```
3、项目中添加System.Windows.Forms引用，接着在Main加入using System.Windows.Forms，然后Main继承UserControl类  
4、在OnLoad中加入以下代码
```
//新建一个Fiddler插件的page
TabPage page = new TabPage("Hello World");
//将page加入Fiddler的tab选项卡中
FiddlerApplication.UI.tabsViews.TabPages.Add(page);
//输出Hello World
FiddlerApplication.DoNotifyUser("Hello", "Hello World");
```
## 四、打包测试
1、使用快捷键Ctrl+Shift+B来快速生成dll文件  
2、将项目目录下的bin\Debug文件夹里面的.dll文件拷贝到Fiddler安装目录里的Scripts文件夹下  
3、重启Fiddler，如下图所示，则代表插件安装成功  
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/005.png "弹框")
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/001%20Hello%20World/006.png "插件")
