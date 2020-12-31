
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CSF
{
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
    public partial class CSFToolsMenu
    {
        private const string LastScenePrefKey = "CSF.LastSceneOpen."/*+AppSetting.ProjectName*/;

        #region 资源打包



        /// <summary>
        /// SpriteAtlas 
        /// isInBuild/true 编辑器下可显示图片，ab包会重复打包UI里
        /// isInBuild/false 编辑器下不可显示图片，ab包不会打到UI里
        /// </summary>
        [MenuItem("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)")]
        public static void ToggleSpriteAtlasInbulidMode()
        {
            EditSpritAtlas.SpritAtlasIsInBuild = !EditSpritAtlas.SpritAtlasIsInBuild;
            EditSpritAtlas.SetUIAtlas(EditSpritAtlas.SpritAtlasIsInBuild);
            ToolsHelper.Log("SpriteAtlas 编辑下可使用:" + (EditSpritAtlas.SpritAtlasIsInBuild ? "true" : "false"));
        }
        [MenuItem("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)", true)]
        public static bool ToggleSpriteAtlasInbulidModeValidate()
        {
            Menu.SetChecked("★工具★/资源打包/SpriteAtlas 编辑下可使用(打包时关闭)", EditSpritAtlas.SpritAtlasIsInBuild);
            return true;
        }
   
        ///// <summary>
        ///// 资源缓存清理
        ///// </summary>
        //[MenuItem("★工具★/资源打包/其它/Clear Cache")]
        //public static void ClearCache()
        //{
        //    Caching.ClearCache();
        //    ToolsHelper.Log("缓存清理完成!");
        //}
        /// <summary>
        /// 资源缓存清理
        /// </summary>
        //[MenuItem("★工具★/资源打包/其它/MkLink StreamingAssets")]
        //public static void MkLinkStreamingAssets()
        //{
        //    LinkHelper.MkLinkStreamingAssets();
        //}
        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets")]
        public static void MkLinkStreamingAssets()
        {
            LinkHelper.IsLinkStreamingAssets = !LinkHelper.IsLinkStreamingAssets;
            LinkHelper.MkLinkStreamingAssets();
            ToolsHelper.Log("链接资源到StreamingAssets:" + (LinkHelper.IsLinkStreamingAssets ? "链接" : "关闭"));
        }
        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets", true)]
        public static bool MkLinkStreamingAssetsValidate()
        {
            Menu.SetChecked("★工具★/资源打包/其它/Link StreamingAssets", LinkHelper.IsLinkStreamingAssets);
            return true;
        }
        //=====================================================

        /// <summary>
        /// 重新打包，删除原始文件
        /// </summary>
        [MenuItem("★工具★/资源打包/重新生成资源(Delete)")]
        public static void ReBuildAllAssetBundles()
        {
            ResBundleTools.ReBuildAllAssetBundles();
        }
        /// <summary>
        /// 导出资源
        /// </summary>
        [MenuItem("★工具★/资源打包/生成资源")]
        public static void BuildAllAssetBundles()
        {
            ResBundleTools.BuildAllAssetBundles();
        }


        /// <summary>
        /// 打开资源目录
        /// </summary>
        [MenuItem("★工具★/资源打包/Show in Explorer")]
        public static void ShowInExplorer()
        {
            ToolsHelper.ShowExplorer(ResBundleTools.GetExportPath());
        }
        #endregion

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
        [MenuItem("★工具★/指引&&功能开放 调试")]
        public static void OpenGuideDebugWindow()
        {
            GuideDebugWindow win = EditorWindow.GetWindow<GuideDebugWindow>(false, "指引&功能开放", true);
            win.autoRepaintOnSceneChange = true;
            win.Show(true);
        }
        [MenuItem("★工具★/打开游戏工具")]
        public static void OpenTools()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory, "../../工具/Tools.exe");
            ToolsHelper.OpenEXE(path);
        }

        #endregion

        #region 多语言设置
        [MenuItem("★工具★/多语言[Editor]/重新加载多语言配置",false,0)]
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

        #region 场景切换       
        /// <summary>
        /// 打开主场景之前的一个场景
        /// </summary>
        [MenuItem("★工具★/打开上个场景 _F4")]
        public static void OpenLastScene()
        {
            var lastScene = EditorPrefs.GetString(LastScenePrefKey);
            if (!string.IsNullOrEmpty(lastScene))
                ToolsHelper.OpenScene(lastScene);
            else
                ToolsHelper.Error("Not found last scene!");
        }
        /// <summary>
        /// 打开主场景
        /// </summary>
        [MenuItem("★工具★/开始游戏 _F5")]
        public static void OpenMainScene()
        {
#if UNITY_5|| UNITY_2017_1_OR_NEWER
            var currentScene = EditorSceneManager.GetActiveScene().path;
#else
            var currentScene = EditorApplication.currentScene;
#endif
            var mainScene = "Assets/Main.unity";
            if (mainScene != currentScene)
                EditorPrefs.SetString(LastScenePrefKey, currentScene);

            ToolsHelper.OpenScene(mainScene);

            if (!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;

        }
        #endregion

        #region 一键打包
        private static string[] platformGruop = new string[] { "OWN_GP", "OWN_IOS" };
        [MenuItem("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)")]
        public static void ChangeAccountPwd()
        {
            //BuildAPKTools.ChagePlatformType(EPlatformType.AccountPwd);
            DefineSymbolsTools.SetDefineSymbolsGroup("", platformGruop);
        }

        [MenuItem("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)", true)]
        public static bool ChangeAccountPwd_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)", DefineSymbolsTools.IsUnDefineSymbols("OWN_GP", "OWN_IOS"));
            return true;
        }

#if UNITY_ANDROID
        [MenuItem("★工具★/一键打包/平台宏切换/Own_GP")]
        public static void ChangeHY_MC()
        {
            //BuildAPKTools.ChagePlatformType(EPlatformType.OWN_GP);
            DefineSymbolsTools.SetDefineSymbolsGroup("OWN_GP", platformGruop);
        }
        [MenuItem("★工具★/一键打包/平台宏切换/Own_GP", true)]
        public static bool ChangeHY_MC_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/Own_GP", DefineSymbolsTools.IsDefineSymbols("OWN_GP"));
            return true;
        }
#endif

#if UNITY_IOS
        [MenuItem("★工具★/一键打包/平台宏切换/OWN_IOS")]
        public static void ChangeHY_IOS()
        {
            //BuildAPKTools.ChagePlatformType(EPlatformType.OWN_IOS);
            DefineSymbolsTools.SetDefineSymbolsGroup("OWN_IOS", platformGruop);
        }
        [MenuItem("★工具★/一键打包/平台宏切换/OWN_IOS", true)]
        public static bool ChangeHY_IOS_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/OWN_IOS", DefineSymbolsTools.IsDefineSymbols("OWN_IOS"));
            return true;
        }
#endif
        /////////////////////////////////测试宏/////////////////////////////////////////
        private static string[] testGruop = new string[] { "LTEST", "TEST" };
        [MenuItem("★工具★/一键打包/平台宏切换/正式服", false, 1)]
        public static void ChangeRelease()
        {
            DefineSymbolsTools.SetDefineSymbolsGroup("", testGruop);
        }

        [MenuItem("★工具★/一键打包/平台宏切换/正式服", true, 1)]
        public static bool ChangeRelease_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/正式服", DefineSymbolsTools.IsUnDefineSymbols("LTEST", "TEST"));
            return true;
        }

        [MenuItem("★工具★/一键打包/平台宏切换/外网测试服", false, 1)]
        public static void ChangeOuternetTest()
        {
            DefineSymbolsTools.SetDefineSymbolsGroup("TEST", testGruop);
        }

        [MenuItem("★工具★/一键打包/平台宏切换/外网测试服", true, 1)]
        public static bool ChangeOuternetTest_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/外网测试服", DefineSymbolsTools.IsDefineSymbols("TEST"));
            return true;
        }

        [MenuItem("★工具★/一键打包/平台宏切换/内网测试服", false, 1)]
        public static void ChangeInnetTest()
        {
            DefineSymbolsTools.SetDefineSymbolsGroup("LTEST", testGruop);
        }
        [MenuItem("★工具★/一键打包/平台宏切换/内网测试服", true, 1)]
        public static bool ChangeInnetTest_Valide()
        {
            Menu.SetChecked("★工具★/一键打包/平台宏切换/内网测试服", DefineSymbolsTools.IsDefineSymbols("LTEST"));
            return true;
        }


        ///////////////打包//////////////////////////
#if UNITY_ANDROID
        [MenuItem("★工具★/一键打包/BuildAndRun")]
        public static void CreateApp_Run()
        {
            BuildAPKTools.BulidTarget(true);
        }
#endif

        [MenuItem("★工具★/一键打包/打包")]
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
        [MenuItem("★工具★/批量添加受伤效果预制体")]
        public static void ChangePre()
        {
            string TargetPath = Application.dataPath + "/GameRes/BundleRes/Maps";
            //获得指定路径下面的所有资源文件
            if (Directory.Exists(TargetPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(TargetPath);
                FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录
                Debug.Log(files.Length);
                for (int i = 0; i < files.Length; i++)
                {
                    //所有预制体
                    if (files[i].Name.EndsWith(".prefab"))
                    {

                        string tempPath = files[i].FullName.Replace(@"\", "/");
                        string[] ary = tempPath.Split('/');
                        int tempint = 0;
                        string path = "Assets";
                        //拼接资源路径
                        for (int n = 0; n < ary.Length; n++)
                        {
                            if (ary[n] == "Assets")
                            {
                                tempint = n;
                            }
                        }
                        for (int x = tempint + 1; x < ary.Length; x++)
                        {
                            path += "/" + ary[x];
                        }
                        Debug.Log("路径：" + path);
                        GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                        GameObject MainCamera = obj.transform.Find("@Path/Main Camera").gameObject;
                        if (MainCamera.transform.Find("PlayerHurt") == null)
                        {
    
                            GameObject parent = GameObject.Instantiate(obj);
                            GameObject childPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameRes/BundleRes/Model/PlayerHurt.prefab");
                            GameObject child = GameObject.Instantiate(childPrefab);
                            child.name = "PlayerHurt";
                            child.transform.parent = parent.transform.Find("@Path/Main Camera").transform;
                            child.transform.localPosition = new Vector3(0,0,2);
                            child.transform.localRotation = new Quaternion(0,0,0,0);


                            PrefabUtility.SaveAsPrefabAsset(parent, path);
                            Editor.DestroyImmediate(parent);

                        }
                        else
                        {
                            Debug.Log("未找到需要添加的物体");
                        }
                    }
                    else
                    {
                        Debug.Log("已存在该物体");
                    }



                    AssetDatabase.Refresh();
                }

            }

        }

    }
}