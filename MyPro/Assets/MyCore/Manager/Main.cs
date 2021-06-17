using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    /// <summary>
    /// 是否进行资源更新
    /// </summary>
    public bool EditorVerCheck = true;
    /// <summary>
    /// HTTP Server地址类型
    /// </summary>
    public AppServerType ServerType;
    /// <summary>
    /// 不使用AB包读ILR(只有编辑器下有效果)
    /// </summary>
    public bool ILRNotABTest = false;

    /// <summary>
    /// 是否使用多服务器模式
    /// </summary>
    public GameType IsMoreServers;

    bool IsStart = false;
    void Awake()
    { 
        DontDestroyOnLoad(this);
        if (!Application.isEditor)
        {
            //非编辑器模式下，强制改为true
            Debug.unityLogger.logEnabled = false;           
            AppSetting.IsRelease = true;
            ServerType = AppServerType.ReleaseServer;
        }
        else
        {
            AppSetting.ILRNotABTest = ILRNotABTest;
            Debug.unityLogger.logEnabled = true;
        }
        AppSetting.GameType = IsMoreServers;
        SetHttpServer();
        MainMgr.Initialize();
        StartTask().Run();
    }
  
    public async CTask StartTask()
    {
        //初始化UI
        MainMgr.UI.Initialize();
        //创建版本检测的ui
        MainMgr.VersionCheck.CrateCheckUI();
        //进行版本检测并更新资源
#if UNITY_EDITOR
        if (EditorVerCheck)
#else
        if (AppSetting.IsVersionCheck)
#endif
        {
            MainMgr.VersionCheck.Check();
        }
        else 
        {
            //如果不需要检测直接跳过这个流程
            MainMgr.VersionCheck.SkipCheck();
        }
        //等待资源检测结束再进行下一步
        await CTask.WaitUntil(() =>
        {
            return MainMgr.VersionCheck.IsUpdateCheckComplete;
        });
        Debug.LogError("到这里，资源的流程一定结束了"+ MainMgr.VersionCheck.IsUpdateCheckComplete);        

        //初始化ILR
        await MainMgr.ILR.Init();
        IsStart = true;
        //主工程流程结束，进入热更工程
        MainMgr.ILR.StartHotFixPro();

    }


    // Update is called once per frame
    void Update()
    {
        if (IsStart)
            MainMgr.ILR.CallHotFixMainUpdate(Time.deltaTime);
    }
    /// <summary>
    /// 设置http地址
    /// </summary>
    public void SetHttpServer()
    {
        switch (ServerType)
        {
            case AppServerType.ReleaseServer:
                AppSetting.HTTPServerURL = AppSetting.ReleaseServerURL;
                break;
            case AppServerType.LocalServer:
                AppSetting.HTTPServerURL = AppSetting.LocalServerURL;
                break;
            case AppServerType.TestServer:
                AppSetting.HTTPServerURL = AppSetting.TestServerURL;
                break;      
        }
     
    }
}
