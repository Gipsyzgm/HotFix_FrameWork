using UnityEngine;
using System.Collections;
using System.IO;

namespace CSF
{
    public class ResUtil
    {
        /// <summary>
        /// 获取资源实际目录
        /// </summary>
        public static string GetRelativePath()
        {
            //if (Application.isEditor)
            //    return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/StreamingAssets/";
            //else if (Application.isMobilePlatform || Application.isConsolePlatform)
            //    return "file:///" + DataPath;
            //else // For standalone player.
            //    return "file://" + Application.streamingAssetsPath + "/";

            //先暂时都从导出资源包中读取
            // return "file://" +AppConfig.ExportResBaseDir+ Utility.GetPlatformName() + "/";

            //if (AppSetting.IsRelease) //发布版模式从PersistentData中读取数据
            //    return Application.persistentDataPath + "/" + AppSetting.PlatformName + "/";

            //if (AppSetting.IsStreamingAssets) //从StreamingAssets中读取资源    
            //    return Application.streamingAssetsPath + "/" + AppSetting.PlatformName + "/";

            ////编辑器开发版模式 Product/AssetBundles中读取           
            //return "file://" + AppSetting.ExportResBaseDir + AppSetting.PlatformName + "/";



            return string.Empty;
        }

        
        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        //public static string DataPath
        //{
        //    get
        //    {
        //        string game = AppConfig.AppName.ToLower();
        //        if (Application.isMobilePlatform)
        //        {
        //            return Application.persistentDataPath + "/" + game + "/";
        //        }
        //        if (AppConfig.DebugMode)
        //        {
        //            return Application.dataPath + "/StreamingAssets/";
        //        }
        //        if (Application.platform == RuntimePlatform.OSXEditor)
        //        {
        //            int i = Application.dataPath.LastIndexOf('/');
        //            return Application.dataPath.Substring(0, i + 1) + game + "/";
        //        }
        //        return "c:/" + game + "/";
        //    }
        //}

        /// <summary>
        /// 获取LUA根目录
        /// </summary>
        /// <returns></returns>
   
    }
}
/**
Debug.Log ("temporaryCachePath = " + UnityEngine.Application.temporaryCachePath);   //temporaryCachePath = /storage/sdcard0/Android/data/com.zwh.p1/cache
Debug.Log ("dataPath = " + UnityEngine.Application.dataPath);                       //dataPath = /mnt/asec/com.zwh.p1-2/pkg.apk
Debug.Log ("persistentDataPath = " + UnityEngine.Application.persistentDataPath);   //persistentDataPath = /storage/sdcard0/Android/data/com.zwh.p1/files
Debug.Log ("streamingAssetsPath = " + UnityEngine.Application.streamingAssetsPath); //streamingAssetsPath = jar:file:///mnt/asec/com.zwh.p1-2/pkg.apk!/assets
*/
