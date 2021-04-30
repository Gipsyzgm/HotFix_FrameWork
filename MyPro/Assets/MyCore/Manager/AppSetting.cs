using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppSetting
{
    /// <summary>
    /// 是否为发布版
    /// </summary>
    public static bool IsRelease = true;                          
    /// <summary>
    /// 是否启用版本检测 
    /// </summary>
    public static bool IsVersionCheck = true;                     
    /// <summary>
    /// 热更工程名
    /// </summary>
    public const string HotFixName = "HotFix";

    /// <summary>
    /// ILR逻辑代码目录,只用于编辑环境
    /// </summary>
    public static string ILRCodeDir
    {
        get { return Path.GetFullPath("../Product/ILR/").Replace("\\", "/"); }
    }

    /// <summary>
    /// 需要放进Addressable的文件位置
    /// </summary>
    public static string AssetResDir = "Assets/GameRes/AddressableRes/";

    /// <summary>
    /// 存放热更工程dll的位置
    /// </summary>
    public static string HotFixDir = AssetResDir + "HotFixDlls/";

    /// <summary>
    /// 背景音乐和音效的储存位置
    /// </summary>
    public static string SoundPath = "Assets/GameRes/AddressableRes/Sound/";

    /// <summary>
    /// UI相关的Prefabs的储存位置
    /// </summary>
    public static string AllUIPrefabs = "Assets/GameRes/AddressableRes/MyUI/";

    /// <summary>
    /// 需要生成图集的图片文件的储存位置
    /// </summary>
    public static string UISpritePath = "Assets/GameRes/ArtRes/UIAtlas/";

    /// <summary>
    /// 图集的储存位置
    /// </summary>
    public static string UIAtlasPath = "Assets/GameRes/AddressableRes/UIAtlas/";
    /// <summary>
    /// UIPrefabs的储存位置
    /// </summary>
    public static string UIPrefabsPath = "Assets/GameRes/AddressableRes/MyUI/View/";
    /// <summary>
    /// UIItemPrefabs的储存位置
    /// </summary>
    public static string UIItemPrefabsPath = "Assets/GameRes/AddressableRes/MyUI/Item/";
    /// <summary>
    /// UI文件生成位置
    /// </summary>
    public static string ExportScriptDir
    {
        get { return Path.GetFullPath("../" + AppSetting.HotFixName + "/Module/UI/").Replace("\\", "/"); }
    }

    /// <summary>
    /// 音效文件生成位置
    /// </summary>
    public static string ExportSoundDir
    {
        get { return Path.GetFullPath("../" + AppSetting.HotFixName + "/Module/Sound/").Replace("\\", "/"); }
    }


}
