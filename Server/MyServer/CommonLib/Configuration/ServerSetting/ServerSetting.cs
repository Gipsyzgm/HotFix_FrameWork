using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonLib.Comm;
using Newtonsoft.Json;

namespace CommonLib.Configuration
{
    public class ServerSetting
    {
        public ELangType LangType = ELangType.ZH_CN;
        public string[] MongoDBs;
        public string GMDB;
        public Dictionary<string, RedisSetting> RedisSettings = new Dictionary<string, RedisSetting>();
        public static ServerSetting Instance { get; } = new ServerSetting(true);

        public ServerSetting(bool isLoad = false)
        {
            if (isLoad)
                Load();
        }
        public RedisSetting GetRedisSetting(string name)
        {
            if (RedisSettings.TryGetValue(name, out var redis))
                return redis;
            else
                Logger.LogError("未找到RedisSetting Name:" + name);
            return null;
        }

        private void Load()
        {
            string path = "../PublicFolder/ServerSetting.json";
            Logger.LogError("path:" + path);
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string strconfig = sr.ReadToEnd();
                        //正则移除注释
                        var reg = new Regex(@"(/\*.*?\*/)", RegexOptions.IgnoreCase);
                        strconfig = reg.Replace(strconfig, "");
                        ServerSettings settings = JsonConvert.DeserializeObject<ServerSettings>(strconfig);
                        foreach (var set in settings.Configs)
                        {
                            if (set.Id == settings.Id)
                            {
                                Enum.TryParse(set.Lang.ToUpper(), out LangType);
                                MongoDBs = set.MongoDBs;
                                GMDB = set.GMDB;
                                if (string.IsNullOrEmpty(GMDB))
                                    GMDB = MongoDBs[0];

                                foreach (var redis in set.Redis)
                                {
                                    if (!RedisSettings.ContainsKey(redis.Name))
                                        RedisSettings.Add(redis.Name, redis);
                                    else
                                        Logger.LogError("ServerSetting中 Redis配置重名:" + redis.Name);
                                }
                                foreach (var redis in RedisSettings.Values)
                                {
                                    if (!string.IsNullOrWhiteSpace(redis.ConnectionAS))
                                    {
                                        if (RedisSettings.TryGetValue(redis.ConnectionAS, out var asredis))
                                        {
                                            redis.ReadConnection = asredis.ReadConnection;
                                            redis.WriteConnection = asredis.WriteConnection;
                                        }
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.LogError("配置文件未找到:" + path);
            }
        }
    }

    internal class ServerSettings
    {
        public int Id;
        public List<ServerSettingItem> Configs;
    }

    internal class ServerSettingItem
    {
        public int Id;
        public string Name;
        public string Lang;
        public string[] MongoDBs;
        public string GMDB;
        public List<RedisSetting> Redis;
    }
    
    public class RedisSetting
    {
        /// <summary>Redis配置名</summary>
        public string Name;
        /// <summary>数据库号</summary>
        public int DBNo;
        /// <summary>读取连接配置</summary>
        public string ReadConnection;
        /// <summary>写入连接配置</summary>
        public string WriteConnection;
        /// <summary>使用同样读写配置</summary>
        public string ConnectionAS;
    }
}
   
