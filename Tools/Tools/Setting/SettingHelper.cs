using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Tools
{
    public class SettingMgr
    {
        /// <summary>
        /// 配置数据集合[表名,[行Id,行Config]]
        /// </summary>
        private Dictionary<string, Dictionary<int, object>> _settingData = new Dictionary<string, Dictionary<int, object>>();

        public void Initialize()
        {
            string path = "Setting/ProjectSetting.json";
            if (!File.Exists(path))
            {
                File.Copy("Setting/ProjectSettingTemplate.json", "Setting/ProjectSetting.json");
                Logger.LogAction("项目配置文件不存在,已自动创建,注意修改项目配置中的目录位置!!!");
            }
            Load<ProjectSetting>();
            Load<CodeOutSetting>();
        }

        /// <summary>
        /// 获得类型的所有数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>数据组</returns>
        public List<T> Select<T>() where T : BaseSetting
        {
            string name = typeof(T).Name;
            List<T> list = new List<T>();
            if (_settingData.ContainsKey(name))
            {
                foreach (KeyValuePair<int, object> k in _settingData[name])
                    list.Add((T)k.Value);
            }
            return list;
        }

        /// <summary> 跟据唯一Id查找记录 </summary>
        public T Select<T>(int uid) where T : BaseSetting
        {
            string name = typeof(T).Name;
            if (_settingData.ContainsKey(name) && _settingData[name].ContainsKey(uid))
                return (T)_settingData[name][uid];
            return null;
        }

        private T Load<T>() where T : BaseSetting
        {
            string name = typeof(T).Name;
            string path = "Setting/" + name + ".json";
            T config = default(T);
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strconfig = sr.ReadToEnd();
                var reg = new Regex(@"(/\*.*?\*/)|//.*", RegexOptions.IgnoreCase);
                strconfig = reg.Replace(strconfig, "");

                List<T> list = JsonConvert.DeserializeObject<List<T>>(strconfig);
                Dictionary<int, object> dicList = new Dictionary<int, object>();
                for (int i = 0; i < list.Count; i++)
                {
                    dicList.Add(list[i].UniqueID, list[i]);
                }
                _settingData.Add(name, dicList);
                sr.Close();
                fs.Close();
            }
            else
            {
                Logger.LogError("配置文件未找到:" + path);
            }
            return config;
        }
    }
}
