using Fiddler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;

namespace _005_Global_Event.Tools
{
    class DataTool
    {
        //对应文件夹路径
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Fiddler2\\FiddlerExample";
        //配置文件路径
        private static string configPath = path + "\\config.json";

        #region 内部工具--配置转JSON
        //格式化配置数据成JSON格式
        private static JObject formatConfigData()
        {
            //最终结果
            JObject result = new JObject();
            //规则数据
            JArray rules = new JArray();
            //获取所有配置数据
            ArrayList items = Main.mainData;

            //遍历添加配置数据到rules
            for (int i = 0, len = items.Count; i < len; i++)
            {
                //获取对应的Item
                HostModel item = items[i] as HostModel;
                //生成Json数据
                JObject rule = new JObject();
                rule.Add("enable", item.Enable);
                rule.Add("ip", item.IP);
                rule.Add("port", item.Port);
                rule.Add("url", item.Url);
                //填充进数据中
                rules.Add(rule);
            }

            //添加骨架
            result.Add("host", rules);

            return result;
        }
        #endregion

        #region 暴露出去的方法
        //初始化文件夹
        public static void initFolder()
        {
            //备份文件夹不存在直接创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        //将配置数据写到本地
        public static void writeConfigToFile()
        {
            try
            {
                FileStream fs = new FileStream(configPath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(formatConfigData().ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                FiddlerApplication.Log.LogString("FiddlerExample出现错误(writeConfigToFile函数)：" + e.ToString());
            }
        }
        #endregion
    }
}

