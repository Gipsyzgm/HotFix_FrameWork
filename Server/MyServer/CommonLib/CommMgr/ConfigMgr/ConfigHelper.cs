using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using CommonLib;
namespace CommonLib.Comm
{
    public partial class ConfigMgr
    {
        public static bool IsGM = false;
        /// <summary>
        /// 配置数据集合[表名,[行Id,行Config]]
        /// </summary>
        private Dictionary<string, Dictionary<object, object>> _configData = new Dictionary<string, Dictionary<object, object>>();

        /// <summary>
        /// 获得类型的所有数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>数据组</returns>
        private List<T> Select<T>() where T : BaseConfig
        {
            string name = typeof(T).Name;
            List<T> list = null;
            if (_configData.ContainsKey(name))
            {
                list = new List<T>();
                foreach (KeyValuePair<object, object> k in _configData[name])
                    list.Add((T)k.Value);
            }
            return list;
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T SelectOne<T>() where T : BaseConfig
        {
            string name = typeof(T).Name;
            if (_configData.ContainsKey(name))
            {
                foreach (KeyValuePair<object, object> k in _configData[name])
                    return (T)k.Value;
            }
            return null;
        }

        /// <summary> 跟据唯一Id查找记录 </summary>
        private T Select<T>(int uid) where T : BaseConfig
        {
            string name = typeof(T).Name;
            if (_configData.ContainsKey(name) && _configData[name].ContainsKey(uid))
                return (T)_configData[name][uid];
            return null;
        }

        /// <summary>
        /// 跟据查询条件获取数据集
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="predicate">查询条件 例: q =>(q.id==1 && q.type==2 ) [查询结构中id==1&&type==2的数据]</param>
        /// <returns></returns>
        private List<T> Select<T>(Func<T, bool> predicate) where T : BaseConfig
        {
            string name = typeof(T).Name;
            List<T> list = new List<T>();
            if (_configData.ContainsKey(name))
            {
                foreach (KeyValuePair<object, object> k in _configData[name])
                {
                    if (predicate((T)k.Value))
                        list.Add((T)k.Value);
                }
            }
            return list;
        }


        /// <summary>
        /// 读取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void loadConfig<T>() where T : BaseConfig
        {
            string name = typeof(T).Name;
            string filePath = $"../PublicFolder/{(IsGM ? "GM" : "")}DataConfig/{name}.txt";

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string strconfig = sr.ReadToEnd();
                        List<T> list = JsonConvert.DeserializeObject<List<T>>(strconfig);
                        Dictionary<object, object> dicList = new Dictionary<object, object>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (dicList.ContainsKey(list[i].UniqueID))
                            {
                                Logger.LogError($"表[{name}]中有相同键({list[i].UniqueID})");
                            }
                            else
                                dicList.Add(list[i].UniqueID, list[i]);
                        }
                        _configData.Add(name, dicList);
                        //sr.Close();
                        //fs.Close();
                    }
                }
            }
            else
            {
                Logger.LogWarning($"配置文件不存在{filePath}");
            }
        }


        /// <summary>
        /// 重新读取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected Dictionary<object, T> reloadConfig<T>() where T : BaseConfig
        {
            string name = typeof(T).Name;
            string filePath = $"../PublicFolder/{(IsGM ? "GM" : "")}DataConfig/{name}.txt";
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                StreamReader sr = new StreamReader(fs);
                string strconfig = sr.ReadToEnd();
                List<T> list = JsonConvert.DeserializeObject<List<T>>(strconfig);
                Dictionary<object, T> dicList = new Dictionary<object, T>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (dicList.ContainsKey(list[i].UniqueID))
                    {
                        Logger.LogError($"表[{name}]中有相同键({list[i].UniqueID})");
                    }
                    else
                        dicList.Add(list[i].UniqueID, list[i]);
                }
                sr.Close();
                fs.Close();
                return dicList;
            }
            else
            {
                Logger.LogWarning($"配置文件不存在{filePath}");
                return null;
            }
        }

        protected T reloadConfigV<T>() where T : BaseConfig
        {
            string name = typeof(T).Name;
            string filePath = $"../PublicFolder/{(IsGM ? "GM" : "")}DataConfig/{name}.txt";
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                StreamReader sr = new StreamReader(fs);
                string strconfig = sr.ReadToEnd();
                List<T> list = JsonConvert.DeserializeObject<List<T>>(strconfig);
                sr.Close();
                fs.Close();
                return list[0];
            }
            else
            {
                Logger.LogWarning($"配置文件不存在{filePath}");
                return null;
            }
        }


        //读取地图配置
        public List<MapConfig> LoadMapConfig()
        {
            string filePath = $"../PublicFolder/{(IsGM ? "GM" : "")}DataConfig/MapConfig.txt";
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                StreamReader sr = new StreamReader(fs);
                string strconfig = sr.ReadToEnd();
                List<MapConfig> list = JsonConvert.DeserializeObject<List<MapConfig>>(strconfig);
                sr.Close();
                fs.Close();
                return list;
            }
            else
            {
                Logger.LogWarning($"配置文件不存在{filePath}");
            }
            return null;
        }

        private void readConfig<T>(Dictionary<object, T> source) where T : BaseConfig
        {
            List<T> list = Select<T>();
            if (list == null) return; //没有加载的
            foreach (T _item in list)
                source.Add(_item.UniqueID, _item);

            if (list.Count == 0)
                Logger.LogWarning($"配置表{ typeof(T).Name} 没有数据");
        }
        private void readConfigV<T>(ref T source) where T : BaseConfig
        {
            source = SelectOne<T>();
        }

    }
}
