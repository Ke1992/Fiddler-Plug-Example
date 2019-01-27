using Fiddler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;

namespace _008_Other.Tools
{
    class DataTool
    {
        //对应文件夹路径
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Fiddler2\\FiddlerExample";
        //配置文件路径
        private static string configPath = path + "\\config.json";
        //备份数据路径
        private static string backupPath = path + "\\Backup";

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
        //读取配置文件的数据
        private static JObject readConfigFromFile()
        {
            if (!File.Exists(configPath))
            {
                //文件不存在，则返回null
                return null;
            }
            try
            {
                //读取对应文件的数据
                StreamReader file = new StreamReader(configPath);
                String content = file.ReadToEnd();
                file.Close();
                //解析数据
                JObject data = JObject.Parse(content);
                //返回数据
                return data;
            }
            catch (Exception e)
            {
                FiddlerApplication.Log.LogString("FiddlerExample出现错误(readConfigFromFile函数)：" + e.ToString());
                return null;
            }
        }
        #endregion

        #region 内部工具--备份配置文件
        private static void backupConfigFile()
        {
            try
            {
                int fileNum = Directory.GetFiles(backupPath, "*.json").Length;

                if (fileNum < 10)
                {
                    FileStream fs = new FileStream(backupPath + "\\backup_" + (fileNum + 1) + ".json", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(formatConfigData().ToString());
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    //首先删除第一个文件
                    File.Delete(backupPath + "\\backup_1.json");
                    //然后将之前的全部改名
                    for (int i = 1; i < 10; i++)
                    {
                        File.Move(backupPath + "\\backup_" + (i + 1) + ".json", backupPath + "\\backup_" + i + ".json");
                    }
                    //重新写入最新备份文件
                    FileStream fs = new FileStream(backupPath + "\\backup_10.json", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(formatConfigData().ToString());
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                FiddlerApplication.Log.LogString("FiddlerExample出现错误(backupConfigFile函数)：" + e.ToString());
            }
        }
        #endregion

        #region 暴露出去的方法
        //初始化文件夹
        public static void initFolder()
        {
            //备份文件夹不存在直接创建
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
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
                //数据备份
                backupConfigFile();
            }
            catch (Exception e)
            {
                FiddlerApplication.Log.LogString("FiddlerExample出现错误(writeConfigToFile函数)：" + e.ToString());
            }
        }
        //初始化配置数据
        public static ArrayList initConfigData()
        {
            ArrayList result = new ArrayList();
            //获取配置数据
            JObject config = readConfigFromFile();

            if (config != null)
            {
                JArray rules = config["host"] as JArray;

                for (int i = 0, len = rules.Count; i < len; i++)
                {
                    //获取规则
                    JObject rule = rules[i] as JObject;
                    //生成对应数据对象
                    HostModel item = new HostModel(i, (bool)rule["enable"], rule["ip"].ToString(), rule["port"].ToString(), rule["url"].ToString());
                    //添加到结果中
                    result.Add(item);
                }
            }

            //返回数据
            return result;
        }
        #endregion
    }
}
