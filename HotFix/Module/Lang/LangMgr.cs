using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;

namespace HotFix
{
    public enum ELangType
    {
        ZH_CN,   //简体中文
        ZH_TW,  //繁体中文
        EN,         //英文
        JA,         //日语
        KO,        //韩语 
    }
    public class LangMgr
    {
        /// <summary>
        /// 默认语言
        /// </summary>
        private ELangType defaultType = ELangType.EN;
        private LanguageConfig config;
        public LangMgr()
        {
            if (!PlayerPrefs.HasKey("ELangType"))
            {
                UseLangSystem();
            }
            else
            {
                defaultType = (ELangType)PlayerPrefs.GetInt("ELangType", (int)ELangType.EN);
            }
        }
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="type"></param>
        public ELangType LangType
        {
            get
            {
                return defaultType;
            }
            set
            {
                if (defaultType != value)
                {
                    defaultType = value;
                    MainMgr.UI.RefreshLang();
                    PlayerPrefs.SetInt("ELangType", (int)value);
                    PlayerPrefs.Save();
                }
            }
        }

        /// <summary>
        /// 使用系统语言
        /// </summary>
        public void UseLangSystem()
        {
            string sysLang = Application.systemLanguage.ToString();
            switch (sysLang)
            {
                case "ChineseSimplified":
                    LangType = ELangType.ZH_CN;
                    break;
                case "ChineseTraditional":
                    LangType = ELangType.ZH_TW;
                    break;
                case "English":
                    LangType = ELangType.EN;
                    break;
                case "Japanese":
                    LangType = ELangType.EN;
                    break;
                case "Korean":
                    LangType = ELangType.EN;
                    break;
                default:
                    LangType = defaultType;
                    break;
            }
        }
        /// <summary>
        /// 跟据Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return Get(key, defaultType).Replace("\\n", "\n");
        }
        public string GetFormat(string key, params object[] args)
        {
            return GetFormat(key, defaultType, args);
        }
        public string GetFormat(string key, ELangType type, params object[] args)
        {
            string str = Get(key, type).Replace("\\n", "\n");
            str = string.Format(str, args);
            return str;
        }
        public string Get(string key, ELangType type)
        {
            if (HotMgr.Config.Language.TryGetValue(key, out config))
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
            return $"{ key}";
        }
    }
}
