
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/*快捷键用法
% = ctrl 
# = Shift 
& = Alt
LEFT/RIGHT/UP/DOWN = 上下左右
F1…F2 = F...
HOME, END, PGUP, PGDN = 键盘上的特殊功能键
特别注意的是，如果是键盘上的普通按键，比如a ~z，则要写成_a ~_z这种带_前缀的。
*/
/// <summary>
/// CSF框架工具菜单
/// </summary>
public partial class ToolsMenu
{

    #region 用户数据
    /// <summary>
    /// 清理PC用户资源数据
    /// </summary>
    [MenuItem("★工具★/用户数据/删除PersistentData")]
    public static void ClearPCPersistentData()
    {
        foreach (string dir in Directory.GetDirectories(Application.persistentDataPath))
            Directory.Delete(dir, true);
        foreach (string file in Directory.GetFiles(Application.persistentDataPath))
            File.Delete(file);
        ToolsHelper.Log("删除PersistentData完成！");
    }
    /// <summary>
    /// 清理PlayerPrefs
    /// </summary>
    [MenuItem("★工具★/用户数据/删除缓存(PlayerPrefs)")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ToolsHelper.Log("删除缓存(PlayerPrefs)完成！");
    }
    /// <summary>
    /// 打开户资源数据
    /// </summary>
    [MenuItem("★工具★/用户数据/Show in Explorer")]
    public static void ShowInExplorerPersistentData()
    {
        ToolsHelper.ShowExplorer(Application.persistentDataPath);
    }
    #endregion

    #region 工具
    [MenuItem("★工具★/打开游戏工具")]
    public static void OpenTools()
    {
        string path = Path.Combine(System.Environment.CurrentDirectory, "../../工具/Tools.exe");
        ToolsHelper.OpenEXE(path);
    }
    #endregion

    #region 多语言设置
    [MenuItem("★工具★/多语言[Editor]/重新加载多语言配置", false, 0)]
    public static void ReLoadLangConfig()
    {
        LangService.Instance.LoadConfig();
        LangService.Instance.RefAllText();
    }
    [MenuItem("★工具★/多语言[Editor]/简体中文")]
    public static void LangSetZH_CN()
    {
        LangService.Instance.LangType = ELangType.ZH_CN;
    }
    [MenuItem("★工具★/多语言[Editor]/简体中文", true)]
    public static bool LangSetZH_CN_Valide()
    {
        Menu.SetChecked("★工具★/多语言[Editor]/简体中文", LangService.Instance.LangType == ELangType.ZH_CN);
        return true;
    }
    [MenuItem("★工具★/多语言[Editor]/繁体中文")]
    public static void LangSetZH_TW()
    {
        LangService.Instance.LangType = ELangType.ZH_TW;
    }
    [MenuItem("★工具★/多语言[Editor]/繁体中文", true)]
    public static bool LangSetZH_TW_Valide()
    {
        Menu.SetChecked("★工具★/多语言[Editor]/繁体中文", LangService.Instance.LangType == ELangType.ZH_TW);
        return true;
    }
    [MenuItem("★工具★/多语言[Editor]/英文")]
    public static void LangSetZH_EN()
    {
        LangService.Instance.LangType = ELangType.EN;
    }
    [MenuItem("★工具★/多语言[Editor]/英文", true)]
    public static bool LangSetZH_EN_Valide()
    {
        Menu.SetChecked("★工具★/多语言[Editor]/英文", LangService.Instance.LangType == ELangType.EN);
        return true;
    }
    [MenuItem("★工具★/多语言[Editor]/日语")]
    public static void LangSetZH_JA()
    {
        LangService.Instance.LangType = ELangType.JA;
    }
    [MenuItem("★工具★/多语言[Editor]/日语", true)]
    public static bool LangSetZH_JA_Valide()
    {
        Menu.SetChecked("★工具★/多语言[Editor]/日语", LangService.Instance.LangType == ELangType.JA);
        return true;
    }
    [MenuItem("★工具★/多语言[Editor]/韩语")]
    public static void LangSetZH_KO()
    {
        LangService.Instance.LangType = ELangType.KO;
    }
    [MenuItem("★工具★/多语言[Editor]/韩语", true)]
    public static bool LangSetZH_KO_Valide()
    {
        Menu.SetChecked("★工具★/多语言[Editor]/韩语", LangService.Instance.LangType == ELangType.KO);
        return true;
    }
    #endregion

    #region 一键打包
#if UNITY_ANDROID
    [MenuItem("★工具★/一键打包/BuildAndRun")]
    public static void CreateApp_Run()
    {
        BuildAPKTools.BulidTarget(true);
    }
#endif
    [MenuItem("★工具★/一键打包/Build")]
    public static void CreateApp()
    {
        BuildAPKTools.BulidTarget();
    }

    #endregion

    #region 宏定义

    [MenuItem("★工具★/宏定义/ILR GC DEBUG")]
    public static void DefineSymbolsILRGCDebug()
    {
        DefineSymbolsTools.ChangeDefineSymbols("DISABLE_ILRUNTIME_DEBUG");
    }
    [MenuItem("★工具★/宏定义/ILR GC DEBUG", true)]
    public static bool DefineSymbolsILRGCDebug_Valide()
    {
        Menu.SetChecked("★工具★/宏定义/ILR GC DEBUG", DefineSymbolsTools.IsDefineSymbols("DISABLE_ILRUNTIME_DEBUG"));
        return true;
    }

    [MenuItem("★工具★/宏定义/DLL反射调用(不建议)")]
    public static void DefineSymbolsREFLECT()
    {
        DefineSymbolsTools.ChangeDefineSymbols("REFLECT");
    }
    [MenuItem("★工具★/宏定义/DLL反射调用(不建议)", true)]
    public static bool DefineSymbolsREFLECT_Valide()
    {
        Menu.SetChecked("★工具★/宏定义/DLL反射调用(不建议)", DefineSymbolsTools.IsDefineSymbols("REFLECT"));
        return true;
    }
    #endregion

#if REFLECT
        /// <summary>
        /// pdb 转成 mdb文件,Unity调试用
        /// </summary>
        [RuntimeInitializeOnLoadMethodAttribute]
        public static void autoCreateMDB()
        {
            string monoExePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Data\MonoBleedingEdge\bin\mono.exe";
            string pdb2mdbPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Data\MonoBleedingEdge\lib\mono\4.5\pdb2mdb.exe";
            string dllPath = Path.Combine(System.Environment.CurrentDirectory, $@"..\Product\ILR\{AppSetting.HotFixName}.dll");
            ToolsHelper.OpenEXE(monoExePath, pdb2mdbPath + " " + dllPath);
        }
#endif
    [MenuItem("★工具★/加速")]
    public static void Speed()
    {
        Time.timeScale = Time.timeScale == 3 ? 1 : 3;
        Debug.LogError("切换速度" + Time.timeScale.ToString());
    }
    //[MenuItem("★工具★/批量修改预制体")]
    //public static void ChangePre()
    //{
    //    string TargetPath = Application.dataPath + "/GameRes/BundleRes/Maps";
    //    //获得指定路径下面的所有资源文件
    //    if (Directory.Exists(TargetPath))
    //    {
    //        DirectoryInfo dirInfo = new DirectoryInfo(TargetPath);
    //        FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录
    //        Debug.Log(files.Length);
    //        for (int i = 0; i < files.Length; i++)
    //        {
    //            //所有预制体
    //            if (files[i].Name.EndsWith(".prefab"))
    //            {
    //                string tempPath = files[i].FullName.Replace(@"\", "/");
    //                string[] ary = tempPath.Split('/');
    //                int tempint = 0;
    //                string path = "Assets";
    //                //拼接资源路径
    //                for (int n = 0; n < ary.Length; n++)
    //                {
    //                    if (ary[n] == "Assets")
    //                    {
    //                        tempint = n;
    //                    }
    //                }
    //                for (int x = tempint + 1; x < ary.Length; x++)
    //                {
    //                    path += "/" + ary[x];
    //                }
    //                Debug.Log("路径：" + path);
    //                GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
    //                GameObject MainCamera = obj.transform.Find("@Path/Main Camera").gameObject;
    //                if (MainCamera.transform.Find("PlayerHurt") == null)
    //                {

    //                    GameObject parent = GameObject.Instantiate(obj);
    //                    GameObject childPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameRes/BundleRes/Model/PlayerHurt.prefab");
    //                    GameObject child = GameObject.Instantiate(childPrefab);
    //                    child.name = "PlayerHurt";
    //                    child.transform.parent = parent.transform.Find("@Path/Main Camera").transform;
    //                    child.transform.localPosition = new Vector3(0, 0, 2);
    //                    child.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //                    PrefabUtility.SaveAsPrefabAsset(parent, path);
    //                    Editor.DestroyImmediate(parent);
    //                }
    //                else
    //                {
    //                    Debug.Log("未找到需要添加的物体");
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("已存在该物体");
    //            }
    //            AssetDatabase.Refresh();
    //        }
    //    }
    //}
}