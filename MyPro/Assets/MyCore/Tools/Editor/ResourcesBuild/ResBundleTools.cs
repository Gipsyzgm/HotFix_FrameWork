using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using marijnz.EditorCoroutines;
namespace CSF
{
    public class ResBundleTools
    {
       
        /// <summary>
        /// 重新打包资源
        /// </summary>
        /// <returns></returns>
        public static void ReBuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();
            string outputPath = GetExportPath();
            Directory.Delete(outputPath, true);
            ToolsHelper.Log("删除目录: " + outputPath);
            BuildAllAssetBundles();           
        }

        /// <summary>
        /// 打包所有资源
        /// isBuildLua 是否打包Lua文件
        /// </summary>
        public static void BuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();
            //EditorCoroutineRunner.StartEditorCoroutine(_OnBuildAllAssetBundles());
            //2019不支持上面的写法
            EditorCoroutines.StartCoroutine(_OnBuildAllAssetBundles(), new object());
        }

        /// <summary>
        /// 复制热更文件
        /// </summary>
        private static void CopyHotFix()
        {
            //string fileDll = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".dll";
            //string filePdb = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".pdb";
            //FileInfo file = new FileInfo(fileDll);
            //if (file.Exists)
            //{
            //    string targetPaht = Path.Combine(AppSetting.BundleResDir, AppSetting.HoxFixBundleDir, AppSetting.HotFixName);
            //    file.CopyTo(targetPaht+ ".bytes", true);
            //    new FileInfo(filePdb).CopyTo(targetPaht+ "_pdb.bytes", true);
            //}
            //AssetDatabase.Refresh();
        }

        static IEnumerator _OnBuildAllAssetBundles()
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            //yield return new WaitForSeconds(0.3f);
            //CopyHotFix();
            //yield return null;
            //RemoveAssetBundleNames();
            //MakeAssetBundleNames();           
            //yield return null;
            //ToolsHelper.Log("资源打包中...");
            //yield return null;
            //var outputPath = GetExportPath();
            ////_SetAtlasIncludeInBuild(false);
            //EditSpritAtlas.SetUIAtlas();
            //yield return new WaitForSeconds(0.2f);//DeterministicAssetBundle
            //if (!string.IsNullOrEmpty(AssetbundleMgr.AssetKey))
            //{
            //    ToolsHelper.Log("加密资源");
            //    BuildPipeline.SetAssetBundleEncryptKey(AssetbundleMgr.AssetKey);
            //    BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.EnableProtection, EditorUserBuildSettings.activeBuildTarget);             
            //}
            //else
            //{
            //    BuildPipeline.SetAssetBundleEncryptKey(null);
            //    BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            //}
            //yield return new WaitForSeconds(0.2f);
            ////_SetAtlasIncludeInBuild(true);
            //yield return null;
            //CreateAssetBundleFileInfo();
            //yield return null;
            //watch.Stop();
            //ToolsHelper.Log("资源打包完成!!用时:" + (watch.ElapsedMilliseconds / 1000.0f)+ "秒");
            //AssetDatabase.Refresh();
            yield break;
        }
        /// <summary>
        /// 获取导出资源路径目录
        /// </summary>
        /// <returns></returns>
        public static string GetExportPath()
        {
            //BuildTarget platfrom = EditorUserBuildSettings.activeBuildTarget;
            //string basePath = AppSetting.ExportResBaseDir;

            //if (File.Exists(basePath))
            //{
            //    ToolsHelper.ShowDialog("路径配置错误: " + basePath);
            //    throw new System.Exception("路径配置错误");
            //}
            //string path = null;
            //var platformName = AppSetting.PlatformName;
            //path = basePath +platformName+"/";
            //ToolsHelper.CreateDir(path);
            return "";
        }

     
     
    }
}