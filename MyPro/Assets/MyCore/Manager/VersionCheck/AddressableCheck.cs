using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public partial class VersionCheckMgr : BaseMgr<VersionCheckMgr>
{
    /// <summary>
    /// 本地用于比对的catalog
    /// </summary>
    public IResourceLocator LoaclLocator;
    //开始检查更新
    public async CTask StartCheck()
    {
        Addressables.InternalIdTransformFunc = InternalIdTransformFunc;

#if UNITY_EDITOR
        var path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + Addressables.BuildPath + "/catalog.json";
#else
        var path = string.Format("{0}/{1}", Addressables.RuntimePath, "catalog.json");
#endif
        Debug.Log("地址：" + path);
        LoaclLocator = await Addressables.LoadContentCatalogAsync(path, true).Task;
        //更新Catalog文件
        CheckUpdate();
    }

    //重新定向一下资源路径
    private string InternalIdTransformFunc(UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation location)
    {
        //判定是否是一个AB包的请求
        if (location.Data is AssetBundleRequestOptions)
        {
            //PrimaryKey是AB包的名字
            //path就是StreamingAssets/Bundles/AB包名.bundle,其中Bundles是自定义文件夹名字,发布应用程序时。
#if UNITY_EDITOR
            var path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + Addressables.BuildPath + "/" + UnityEditor.EditorUserBuildSettings.activeBuildTarget + "/" + location.PrimaryKey;
            if (File.Exists(path))
            {
                Debug.Log("在本地找到了资源：" + path);
                return path;
            }
#else
            var path = string.Format("{0}/{1}/{2}",Addressables.RuntimePath,Utility.GetPlatformName(), location.PrimaryKey);
            if (LoaclLocator.Locate(location.PrimaryKey, location.ResourceType, out var locs))
            {
                Debug.Log("在本地找到了资源：" + path);
                return path;
            }
#endif
        }
        Debug.Log("资源地址：" + location.InternalId);
        return location.InternalId;
    }
    async void CheckUpdate()
    {
        CheckUI.Status.text = VerCheckLang.CheckResInfo;
        //初始化Addressable
        var init = Addressables.InitializeAsync();
        await init.Task;
        //开始连接服务器检查更新
        AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
        await checkHandle.Task;
        //检查结束，验证结果 
        if (checkHandle.Status == AsyncOperationStatus.Succeeded)
        {
            List<string> catalogs = checkHandle.Result;
            if (catalogs != null && catalogs.Count > 0)
            {
                Debug.Log("download catalogs start");
                var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                await updateHandle.Task;
                Debug.Log("download catalogs finish");
                //获取所有需要更新的key
                //List<object> keys = new List<object>();
                //foreach (var item in updateHandle.Result)
                //{
                //    Debug.Log(item.LocatorId);
                //    foreach (var key in item.Keys)
                //    {
                //        Debug.Log(item.LocatorId+":"+key);
                //    }
                //    keys.AddRange(item.Keys);
                //}
                IEnumerable<object> keys = updateHandle.Result[0].Keys;
                // 获取下载内容的大小
                var sizeHandle = Addressables.GetDownloadSizeAsync(keys);
                await sizeHandle.Task;
                long totalDownloadSize = sizeHandle.Result;
                Debug.Log("下载大小：" + totalDownloadSize);
                if (totalDownloadSize > 0)
                {
                    CheckUI.LoadProgress.gameObject.SetActive(true);                  
                    Debug.Log("download bundle start");
                    var downloadHandle = Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.Union);
                    while (!downloadHandle.IsDone)
                    {
                        float percent = downloadHandle.PercentComplete;
                        if (percent > 0.1f)
                        {
                            CheckUI.LoadProgress.value = percent - 0.1f;
                        }
                        await Task.Yield();
                    }
                    CheckUI.LoadProgress.value = downloadHandle.PercentComplete - 0.1f;
                    Addressables.Release(downloadHandle);
                    CheckUI.Status.text = "更新完成";
                    Debug.Log("download bundle finish");
                }
                else
                {
                    CheckUI.Status.text = "正在进入游戏...";
                }
                Addressables.Release(updateHandle);
            }
            else
            {
                CheckUI.Status.text = "正在进入游戏...";
                Debug.Log("Catalogs是空的!");
            }
        }
        Addressables.Release(checkHandle);
        CheckUI.LoadProgress.value = 0.9f;
        CheckUI.Status.text = "加载游戏资源...";
        IsUpdateCheckComplete = true;
      
    }
    /// <summary>
    /// 这个方法和Addressables.UpdateCatalogs获取到的资源是一致的，强制更新所有资源。
    /// </summary>
    async void StartUpdate()
    {
        Debug.Log("开始更新资源");
        IEnumerable<IResourceLocator> locators = Addressables.ResourceLocators;
        foreach (var item in locators)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(item.Keys);
            await sizeHandle.Task;
            if (sizeHandle.Result > 0)
            {
                var downloadHandle = Addressables.DownloadDependenciesAsync(item.Keys);
                await downloadHandle.Task;
            }
        }
        Debug.Log("更新完成");
    }
   
}
