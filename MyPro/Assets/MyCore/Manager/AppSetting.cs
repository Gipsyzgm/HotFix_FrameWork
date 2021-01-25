using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppSetting
{
    /// <summary>
    /// 是否为发布版
    /// </summary>
    public static bool IsRelease = false;                             //走正式流程设为true(发布会强制修改)
    public static bool ILRNotABTest = true;                        //不使用AB资源加载ILR(只有编辑器下有效)  
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
    /// UIPrefabs的储存位置
    /// </summary>
    public static string UIPrefabsPath = "Assets/GameRes/MyUI/View/";
    /// <summary>
    /// UIItemPrefabs的储存位置
    /// </summary>
    public static string UIItemPrefabsPath = "Assets/GameRes/MyUI/Item/";
    /// <summary>
    /// UI文件生成位置
    /// </summary>
    public static string ExportScriptDir
    {
        get { return Path.GetFullPath("../" + AppSetting.HotFixName + "/Module/UI/").Replace("\\", "/"); }
    }
    /// <summary>
    /// 需要放进Addressable的文件位置
    /// </summary>
    public static string AssetResDir = "Assets/GameRes/";

}
