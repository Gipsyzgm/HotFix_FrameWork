using System;
using System.IO;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 硬链接目录工具。。。支持win+mac, 需要win 7以上才有mklink命令
/// </summary>
public class LinkHelper
{


    /// <summary>
    /// 创建StreamingAssets链接
    /// </summary>
    public static void MkLinkStreamingAssets()
    {
        string linkPath = Application.streamingAssetsPath + "/" /*+ AppSetting.PlatformName*/;
        if (IsLinkStreamingAssets)
        {
            ToolsHelper.CreateDir(Application.streamingAssetsPath);
            //var exportPath = /*AppSetting.ExportResBaseDir + AppSetting.PlatformName*/"";
            //SymbolLinkFolder(exportPath, linkPath);
        }

        AssetDatabase.Refresh();
    }

    private static bool _IsLinkStreamingAssets = false;
    static string kIsLinkStreamingAssets = "IsLinkStreamingAssets" /*+ AppSetting.ProjectName*/;
    /// <summary>
    /// 是否连接资源StreamingAssets
    /// </summary>
    public static bool IsLinkStreamingAssets
    {
        get
        {
            _IsLinkStreamingAssets = EditorPrefs.GetBool(kIsLinkStreamingAssets, false);
            return _IsLinkStreamingAssets;
        }
        set
        {
            _IsLinkStreamingAssets = value;
            EditorPrefs.SetBool(kIsLinkStreamingAssets, value);
        }
    }
}
