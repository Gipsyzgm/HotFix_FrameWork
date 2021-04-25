using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Configuration;

namespace CommonLib.Comm
{
    public class LangMgr
    {
        /// <summary>
        /// 默认语言
        /// </summary>
        public ELangType defaultType = ELangType.ZH_CN;
        private LanguageConfig config;
        public static LangMgr I;
        public LangMgr()
        {
            I = this;
            //string defLang = ConfigurationManager.AppSettings["Lang"];
            //Enum.TryParse(defLang.ToUpper(), out defaultType);
            defaultType = ServerSetting.Instance.LangType;
        }
        /// <summary>
        /// 跟据Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return Get(key, defaultType);
        }
        public string GetFormat(string key, params object[] args)
        {
            return GetFormat(key, defaultType, args);
        }
        public string GetFormat(string key, ELangType type,params object[] args)
        {
            string str = Get(key, type);
            str = string.Format(str, args);
            return str;
        }
        public string Get(string key, ELangType type)
        {
            if (ConfigMgr.I.dicLanguage.TryGetValue(key, out config))
            {
                switch (type)
                {
                    case ELangType.ZH_CN:
                        return config.zh_cn;
                    case ELangType.ZH_TW:
                        return config.zh_tw;
                    case ELangType.EN:
                        return config.en;
                    case ELangType.JA:
                        return config.ja;
                    case ELangType.KO:
                        return config.ko;
                }
            }
            return $"[{ key}]";
        }
    }
}
