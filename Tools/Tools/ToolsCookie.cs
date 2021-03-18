using Newtonsoft.Json;
using System.IO;

namespace Tools
{
    public class ToolsCookie
    {
        /// <summary>最后一次选中Tab项的索引</summary>
        public int LastSelectTabIndex = 0;
        /// <summary>最后一次选中Project项的索引</summary>
        public int LastSelectProjectId = 1;
        /// <summary>日志类型</summary>
        public int LogType = (int)ELogType.All;
        /// <summary>是/否客户端配置</summary>
        public bool IsClientDev = true;
        /// <summary>是/否服务器配置</summary>
        public bool IsServerDev = false;
    }

    public class ToolsCookieHelper
    {
        //配置文件保存路径
        private static string ConfigSavePath => System.Environment.CurrentDirectory + "\\Cookie.txt";

        private static ToolsCookie _config = null;
        //保存配置
        public static void Save()
        {
            FileStream fs = new FileStream(ConfigSavePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string strconfig = JsonConvert.SerializeObject(_config);
            sw.Write(strconfig);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 加载一些基础配置
        /// </summary>
        public static void Load()
        {
            if (File.Exists(ConfigSavePath))
            {
                FileStream fs = new FileStream(ConfigSavePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strconfig = sr.ReadToEnd();
                if (strconfig != string.Empty)
                    _config = JsonConvert.DeserializeObject<ToolsCookie>(strconfig);
                else
                    _config = new ToolsCookie();
                sr.Close();
                fs.Close();
            }
            else
                _config = new ToolsCookie();
        }
        //读取配置
        public static ToolsCookie Config
        {
            get
            {
                if (_config == null)
                    Load();
                return _config;
            }
        }

        public static string GetDevName()
        {
            string str = string.Empty;
            if (Config.IsClientDev)
                str += "客户端";
            if (Config.IsServerDev)
                str += "服务端";
            if (str == string.Empty)
            {
                str += "未选择对应目标";
                Logger.LogAction("请在项目选择栏中，至少选一个客户端开发或服务端开发");
            }
            return str;
        }
    }
}
