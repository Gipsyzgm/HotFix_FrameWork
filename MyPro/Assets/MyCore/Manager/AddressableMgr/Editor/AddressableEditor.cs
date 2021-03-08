using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 所有资源全用远程构建远程加载非静态的方式来做。
/// 通过DefaultBuild来做首次构建。
/// 关于crc建议：静态资源不要勾选,非静态资源勾不勾选都可以
/// 安卓会直接缓存.待测试.
/// </summary>
public class AddressableEditor
{
    //加载目录地址资源路径
    static string resPath = AppSetting.AssetResDir;
    [MenuItem("★工具★/Addressable/自动分组", false,2)]
    public static void AutoGroup()
    {
        CopyHotFix();
        Debug.Log("自动分组时先把热更文件替换至最新。");
        string targetPath = Path.Combine(AppSetting.HotFixDir, AppSetting.HotFixName);
        FileInfo file = new FileInfo(targetPath + ".bytes");
        if (!file.Exists)
        {
            Debug.LogError("热更dll文件不存在，如不需要请忽略，如果需要请尝试先执行CopyHotFix!");
        }
        string[] dirs = Directory.GetDirectories(resPath);
        foreach (var item in dirs)
        {
            DirectoryInfo dir = new DirectoryInfo(item);
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup Group = CreateOrGetNonStaticGroup(settings, dir.Name);
            SetAssets(item, Group,settings);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    //该文件夹下所有文件均放在同一Group
    static void SetAssets(string path, AddressableAssetGroup Group, AddressableAssetSettings settings)
    {
        //是否启用简单命名方式
        bool simplied = true;
        DirectoryInfo dir = new DirectoryInfo(path);
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i] is DirectoryInfo)
                SetAssets(files[i].FullName, Group, settings);
            else
            {
                if (files[i].FullName.EndsWith(".meta")) continue;
                string  assetPath = "Assets" + files[i].FullName.Substring(Application.dataPath.Length);
                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                var entry = settings.CreateOrMoveEntry(guid, Group);
                Debug.Log(assetPath);
                entry.address = assetPath;
                if (simplied)
                {
                    entry.address = Path.GetFileNameWithoutExtension(assetPath);
                }
                //设置资源标签
                //entry.SetLabel("labelname", true, true);              
            }
        }
    }

    //创建组
    private static AddressableAssetGroup CreateOrGetNonStaticGroup(AddressableAssetSettings settings, string groupName)
    {
        var group = settings.FindGroup(groupName);
        if (group == null)
            group = settings.CreateGroup(groupName, false, false, false, null, typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
        group.GetSchema<ContentUpdateGroupSchema>().StaticContent = false;
        BundledAssetGroupSchema groupSchema = group.GetSchema<BundledAssetGroupSchema>();
        //groupSchema.UseAssetBundleCrc = false;
        //groupSchema.BundleNaming = BundledAssetGroupSchema.BundleNamingStyle.OnlyHash;
        groupSchema.BundleMode = BundledAssetGroupSchema.BundlePackingMode.PackSeparately;
        groupSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);
        groupSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
        return group;    
    }

    //打包
    [MenuItem("★工具★/Addressable/DefaultBuild", false, 4)]
    public static void BuildContent()
    {
        Debug.LogWarning("首次打包使用,会总把bundle资源克隆到对应的StreamingAsset目录");
        string path = AddressableAssetSettingsDefaultObject.Settings.RemoteCatalogBuildPath.GetValue(AddressableAssetSettingsDefaultObject.Settings);
        if (path=="")
        {
            Debug.LogError("Addressable未配置BuildRemoteCatalog!!!");
            return;
        }   
        AddressableAssetSettings.BuildPlayerContent();
        string linkPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AddressableAssetSettingsDefaultObject.Settings.RemoteCatalogBuildPath.GetValue(AddressableAssetSettingsDefaultObject.Settings);   
        var exportPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + Addressables.BuildPath+"/" + UnityEditor.EditorUserBuildSettings.activeBuildTarget;
        Debug.LogWarning("文件从" + linkPath+"——Copy至"+ exportPath);
        ToolsHelper.CopyDirectory(linkPath,exportPath,true); 
        AssetDatabase.Refresh();
    }

    [MenuItem("★工具★/Addressable/CopyHotFix", false,0)]
    public static void CopyHotFix()
    {
        string fileDll = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".dll";
        string filePdb = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".pdb";
        FileInfo file = new FileInfo(fileDll);
        if (file.Exists)
        {
            string targetPath = Path.Combine(AppSetting.HotFixDir, AppSetting.HotFixName);
            file.CopyTo(targetPath + ".bytes", true);
            new FileInfo(filePdb).CopyTo(targetPath + "_pdb.bytes", true);
        }
        else 
        {
            Debug.LogError("CopyHotFix失败，dll文件不存在，请生成文件或检查文件路径！");
        }
        AssetDatabase.Refresh();
        Debug.Log("CopyHotFix成功");
    }


    //更新
    [MenuItem("★工具★/Addressable/打静态资源更新包", false, 6)]
    public static void CheckForUpdateContent()
    {
        //与上次打包做资源对比
        string buildPath = ContentUpdateScript.GetContentStateDataPath(false);
        var m_Settings = AddressableAssetSettingsDefaultObject.Settings;
        List<AddressableAssetEntry> entrys = ContentUpdateScript.GatherModifiedEntries(m_Settings, buildPath);
        if (entrys.Count == 0)
        {
            Debug.Log("没有资源变更");
            return;
        }
        StringBuilder sbuider = new StringBuilder();
        sbuider.AppendLine("Need Update Assets:");
        foreach (var _ in entrys)
        {
            sbuider.AppendLine(_.address);
        }
        Debug.Log(sbuider.ToString());
        //将被修改过的资源单独分组---可以自定义组名
        var groupName = string.Format("UpdateGroup_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        ContentUpdateScript.CreateContentUpdateGroup(m_Settings, entrys, groupName);
    }

    [MenuItem("★工具★/Addressable/清除所有标签", false,8)]
    public static void ClearLabel()
    {
        AddressableAssetSettings assetSettings = AddressableAssetSettingsDefaultObject.GetSettings(false);
        var labelList = assetSettings.GetLabels();
        foreach (var item in labelList)
        {
            assetSettings.RemoveLabel(item);
        }
    }

    [MenuItem("★工具★/Addressable/添加已定义的标签", false,10)]
    public static void ClearAllLabel()
    {
        //需要添加标签
        AddressableAssetSettings assetSettings = AddressableAssetSettingsDefaultObject.GetSettings(false);
        for (int i = assetSettings.groups.Count - 1; i >= 0; --i)
        {
            AddressableAssetGroup assetGroup = assetSettings.groups[i];
            foreach (var item in assetGroup.entries)
            {
               //item.SetLabel("标签",true);
            }       
        }
    }

    [MenuItem("★工具★/Addressable/打开远程Build目录", false,12)]
    public static void BuildUpdate()
    {
        var m_Settings = AddressableAssetSettingsDefaultObject.Settings;
        string path = (Application.dataPath.Substring(0,Application.dataPath.LastIndexOf('/')) +"/"+m_Settings.RemoteCatalogBuildPath.GetValue(m_Settings));
        ToolsHelper.ShowExplorer(path.Substring(0, path.LastIndexOf('/')));
    }

    [MenuItem("★工具★/Addressable/打开本地构建目录", false, 14)]
    public static void OpenLocalBuild()
    {
        Debug.Log("路径：" + Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + Addressables.BuildPath);
        ToolsHelper.ShowExplorer(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) +"/"+Addressables.BuildPath);
    }


    [MenuItem("★工具★/Addressable/打开缓存目录", false, 16)]
    public static void OpenPersist()
    {
        Application.OpenURL(Application.persistentDataPath);
    }
}