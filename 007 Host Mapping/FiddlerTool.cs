using Fiddler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _007_Host_Mapping
{
    class FiddlerTool
    {
        #region Fiddler监听事件
        //监听请求前的事件
        public static void handleRequest(Session session)
        {
            //HOST映射
            handleHostMapping(session);
        }
        //HOST映射
        private static void handleHostMapping(Session session)
        {
            ArrayList rules = getValidRulesByType();

            //如果没有有效的host配置，直接返回
            if (rules.Count == 0)
            {
                return;
            }

            //获取Url的Path
            string path = getPathFromSession(session.fullUrl);

            //遍历配置去修改映射值
            for (int i = 0; i < rules.Count; i++)
            {
                //获取对应的各种参数
                string url = (rules[i] as HostModel).Url.ToString();
                string ip = (rules[i] as HostModel).IP.ToString();
                string port = (rules[i] as HostModel).Port.ToString();
                //新建正则表达式来检测
                Regex urlRegex = new Regex(url);
                //判断当前session的path是否在配置中
                if (path.IndexOf(url) >= 0 || urlRegex.IsMatch(path))
                {
                    //修改背景颜色、字体颜色
                    session["ui-color"] = "#FFFFFF";
                    session["ui-backcolor"] = "#9966CC";

                    if (port.Length > 0)
                    {
                        session["x-overrideHost"] = ip + ":" + port;
                    }
                    else
                    {
                        //映射到对应的ip和端口(这里必须写上端口号，不然https下会有问题)
                        session["x-overrideHost"] = ip + ":" + session.port;
                    }
                    session.bypassGateway = true;
                    break;
                }
            }
        }
        #endregion

        #region 内部工具函数
        //根据类型获取有效的规则
        private static ArrayList getValidRulesByType()
        {
            ArrayList rules = new ArrayList();

            //遍历获取有效的数据
            for (int i = 0, len = Main.mainData.Count; i < len; i++)
            {
                HostModel item = Main.mainData[i] as HostModel;

                //为false则直接跳过
                if (!item.Enable)
                {
                    continue;
                }

                rules.Add(item);
            }

            return rules;
        }
        //从Session中获取path
        private static string getPathFromSession(string fullUrl)
        {
            string path = fullUrl;

            if (path.IndexOf("?") > 0)
            {
                path = path.Substring(0, path.IndexOf("?"));
            }

            return path;
        }
        #endregion
    }
}
