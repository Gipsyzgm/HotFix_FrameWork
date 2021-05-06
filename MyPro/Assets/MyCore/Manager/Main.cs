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

    bool IsStart = false;
    void Awake()
    { 
        DontDestroyOnLoad(this);
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
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
}
