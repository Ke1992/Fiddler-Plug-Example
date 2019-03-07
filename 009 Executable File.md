# 可执行文件
## 一、准备
1、Nsis  
使用Nsis来制作可执行文件，下载地址：https://nsis.sourceforge.io/Download  
2、ico文件  
可执行文件设置图标需要使用.ico文件，http://www.bitbug.net在线生成.ico文件，并命名为icon.ico  
3、dll文件  
008 Other工程重新编译生成，复制其中的008 Other.dll、Newtonsoft.Json.dll，并重命名008 Other.dll为FiddlerExample.dll
## 二、基础功能
1、新建FiddlerExample.nsi文件，修改文件格式为Unix，同时修改编码为GB2312  
2、基本设置
```
;命名
Name "Fiddler Example"
;输出的文件名字
OutFile "FiddlerExample.exe"
;设置exe文件的图标样式
Icon "icon.ico"
;安装的时候是否要求管理员权限
RequestExecutionLevel "admin"
;指定压缩方式
SetCompressor lzma
;显示在底部的文案
BrandingText "Fiddler Example v1.0.0"
;设置安装路径
;$PROGRAMFILES：程序文件目录(通常为 C:\Program Files 但是运行时会检测)
InstallDir "$PROGRAMFILES\Fiddler\Scripts\"
```
3、安装页面
```
;包含的页面
Page directory ;installation directory selection page
Page instfiles ;installation page where the sections are executed
```
4、执行命令
```
;一个命令区段
Section "Main"
	;$INSTDIR：用户定义的解压路径
	SetOutPath "$INSTDIR"
	;是否开启覆写模式
	SetOverwrite on
	;需要打包进exe的文件
	File "FiddlerExample.dll"
	File "Newtonsoft.Json.dll"
	;输出到日志中
	DetailPrint "安装路径：$INSTDIR"
	DetailPrint "安装成功！"
	;使用MessageBox弹出一个对话框
	MessageBox MB_OK|MB_ICONEXCLAMATION "安装成功"
SectionEnd
```
5、其他设置
```
;是否显示安装日志
ShowInstDetails show
```
6、生成Exe文件  
打开NSIS软件，选择Compile NSI scripts，然后将编写好的FiddlerExample.nsi文件拖入编辑器中；或者直接FiddlerExample.nsi右键选择Compile NSI scripts  
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/009%20Executable%20File/001.png "Exe打包")
7、预览  
点击编译完成的Test Installer按钮或者直接打开编译好的FiddlerExample.exe文件，然后选择目录，点击Install，执行完成以后，对应目录里面就会增加FiddlerExample.dll、Newtonsoft.Json.dll两个文件
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/009%20Executable%20File/002.png "执行Exe")

![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/009%20Executable%20File/003.png "执行结果")
## 三、增强体验
1、读取注册表  
在设置安装路径代码下面增加注册表相关逻辑  
```
;设置安装路径
;$PROGRAMFILES：程序文件目录(通常为 C:\Program Files 但是运行时会检测)
InstallDir "$PROGRAMFILES\Fiddler\Scripts\"
;读取注册表中Fiddler的安装路径(读取失败的情况下会使用上一步的路径)
InstallDirRegKey HKCU "SOFTWARE\Microsoft\Fiddler2\InstallerSettings" "ScriptPath"
```
2、引入逻辑库
```
;引用if操作库(逻辑库)
!include "LogicLib.nsh"
```
3、引入StrContains方法  
由于Nsis没有现成的字符串包含方法，因此使用官方提供的StrContains方法，文档地址：https://nsis.sourceforge.io/StrContains，拷贝源码放在代码最上面  
4、Main命令区段的顶部增加路径校验逻辑
```
;判断是否包含必要字段
${StrContains} $0 "Fiddler\Scripts" $INSTDIR
;如果结果为空(即不包含必要字段)
${If} $0 == ""
	MessageBox MB_OK|MB_ICONEXCLAMATION "请选择Fiddler安装目录下的Scripts文件夹"
	;返回目录选择页
	SendMessage $HWNDPARENT 0x408 -1 0
	;不执行后面的代码
	Abort
${EndIf}
```
5、打包预览
![blockchain](https://raw.githubusercontent.com/Ke1992/Fiddler-Plug-Example/master/images/009%20Executable%20File/004.png "异常提示")
