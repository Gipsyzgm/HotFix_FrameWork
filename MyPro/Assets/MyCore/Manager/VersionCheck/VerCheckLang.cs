using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仅用于主工程版本检测页面,逻辑和热更的加载一致，只是资源位置不同。
public class VerCheckLang
{
    /// <summary>
    /// 语言类型，和热更项目中的 ELangType保持一至
    /// </summary>
    public enum EVLangType
    {
        ZH_CN,   //简体中文
        ZH_TW,  //繁体中文
        EN,         //英文
        JA,         //日语
        KO,        //韩语 
    }
    public VerCheckLang()
    {
        if (!PlayerPrefs.HasKey("ELangType"))
        {
            UseLangSystem();
        }
        else
        {
            defaultLangType = (EVLangType)PlayerPrefs.GetInt("ELangType", (int)EVLangType.EN);
        }
    }
    public static string InitRes => GetLang("InitRes", "初始化资源");
    public static string CheckResInfo => GetLang("CheckResInfo", "检测资源信息");
    public static string Confirm => GetLang("Confirm", "确定");
    public static string Cancel => GetLang("Cancel", "取消");
    public static string ErrorTitle => GetLang("ErrorTitle", "错误提示");
    public static string Request_Version_Error => GetLang("Request_Version_Error", "请求服务器信息失败，可能因为你的网络情况不佳，请检查网络设置，点击确定尝试重新请求!!!");
    public static string Version_Platform_Error => GetLang("Version_Platform_Error", "服务器平台配置错误，点击确定尝试重新请求!!!");
    public static string Version_Low => GetLang("Version_Low", "当前版本过低，请前往平台更新最新版本!");
    public static string Version_Update => GetLang("Version_Update", "正在更新资源 {0}/{1}  速度:{2}/秒");
    public static string Version_Update_Complate => GetLang("Version_Update_Complate", "更新完成!!");
    public static string Version_Update_Error => GetLang("Version_Update_Error", "下载文件失败，可能网络情况不佳,正在重新下载！！！\n{0}");
    public static string Version_Update_MD5Error => GetLang("Version_Update_MD5Error", "文件MD5效验失败，正在重新下载\n{0}");

    public static string Version_Update_Confirm_Title => GetLang("Version_Update_Confirm_Title", "资源下载");
    public static string Version_Update_Confirm_Content => GetLang("Version_Update_Confirm_Content", "有资源需要下载,大小{0},\n现在是否开始下载？");
   

    public static EVLangType defaultLangType = EVLangType.EN;

    private static Dictionary<string, VerLangConfig> m_dicVerLang;
    public static Dictionary<string, VerLangConfig> dicVerLang
    {
        get
        {
            if (m_dicVerLang == null)
            {
                m_dicVerLang = new Dictionary<string, VerLangConfig>();
                TextAsset text = Resources.Load<TextAsset>("VersionCheck/VerCheckLang");
                if (text != null)
                {
                    List<VerLangConfig> list = JsonMapper.ToObject<List<VerLangConfig>>(text.ToString());
                    for (int i = 0; i < list.Count; i++)
                        dicVerLang.Add(list[i].Id, list[i]);
                }
                defaultLangType = (EVLangType)PlayerPrefs.GetInt("ELangType", (int)defaultLangType);
            }
            return m_dicVerLang;
        }
    }

    public static EVLangType LangType
    {
        get
        {
            return defaultLangType;
        }
        set
        {
            if (defaultLangType != value)
            {
                defaultLangType = value;
                MainMgr.UI.RefreshLang();
                PlayerPrefs.SetInt("ELangType", (int)value);
                PlayerPrefs.Save();
            }
        }
    }

    public static void UseLangSystem()
    {
        if (!PlayerPrefs.HasKey("ELangType"))
        {
            string sysLang = Application.systemLanguage.ToString();
            switch (sysLang)
            {
                case "ChineseSimplified":
                    LangType = EVLangType.ZH_CN;
                    break;
                case "ChineseTraditional":
                    LangType = EVLangType.ZH_TW;
                    break;
                case "English":
                    LangType = EVLangType.EN;
                    break;
                case "Japanese":
                    LangType = EVLangType.EN;
                    break;
                case "Korean":
                    LangType = EVLangType.EN;
                    break;
                default:
                    LangType = defaultLangType;
                    break;
            }
        }
        else
        {
            VerCheckLang.defaultLangType = (VerCheckLang.EVLangType)PlayerPrefs.GetInt("ELangType", (int)VerCheckLang.EVLangType.EN);
        }

    }
    private static string GetLang(string key, string defVal)
    {
        VerLangConfig config;
        if (dicVerLang.TryGetValue(key, out config))
        {
            string str = string.Empty;
            switch (defaultLangType)
            {
                case EVLangType.ZH_CN:
                    str = config.Zh_cn;
                    break;
                case EVLangType.ZH_TW:
                    str = config.Zh_tw;
                    break;
                case EVLangType.EN:
                    str = config.En;
                    break;
                case EVLangType.JA:
                    str = config.Ja;
                    break;
                case EVLangType.KO:
                    str = config.Ko;
                    break;
            }
            str = str.Replace("\\n", "\n");
            return str;
        }
        return defVal;
    }
}
