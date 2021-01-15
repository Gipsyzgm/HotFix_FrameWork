using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using UnityEngine.Networking;
using System.IO;
using System.Threading.Tasks;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using Cysharp.Threading.Tasks;

public class ILRMgr :BaseMgr<ILRMgr>
{
    /// <summary>
    /// AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个。
    /// </summary>
    public AppDomain appdomain { get; private set; }

    /// <summary>
    /// 调用热更工程的统一类
    /// </summary>
    private string HotFixClass = AppSetting.HotFixName + ".ILRMainCall";

    /// <summary>
    /// 热更工程Main
    /// </summary>
    private string HotFixMainClass = AppSetting.HotFixName + ".Main";

    /// <summary>
    /// 热更工程内模拟Update
    /// </summary>
    IMethod HotFixMainUpdate;
    public async UniTask Init()
    {
       await  LoadHotFixAssembly();
  
    }
    /// <summary>
    /// 加载热更dll资源
    /// </summary>
    /// <returns></returns>
    async UniTask LoadHotFixAssembly()
    {
        bool isDebug = getIsDebug();
        appdomain = new AppDomain();
        byte[] dll = null;
        byte[] pdb = null;
        if (AppSetting.IsRelease && !AppSetting.ILRNotABTest)
        {
            // Addressablesd加载Dll资源

            TextAsset asset = new TextAsset();
            dll = asset.bytes;
        }
        else
        {
            //ILR DLL 直接加载
            string dllpath = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".dll";
            UnityWebRequest dllrequest = UnityWebRequest.Get(dllpath);
            await dllrequest.SendWebRequest();
            if (!string.IsNullOrEmpty(dllrequest.error))
                UnityEngine.Debug.LogError(dllrequest.error + " URL:" + dllpath);
            byte[] dllfileByte = dllrequest.downloadHandler.data;
            dllrequest.Dispose();
            dll = dllfileByte;
            if (isDebug) 
            {
                string pdbpath = AppSetting.ILRCodeDir + AppSetting.HotFixName + ".pdb";
                UnityWebRequest pdbrequest = UnityWebRequest.Get(pdbpath);
                await pdbrequest.SendWebRequest();
                if (!string.IsNullOrEmpty(pdbrequest.error))
                    UnityEngine.Debug.LogError(pdbrequest.error + " URL:" + pdbpath);
                byte[] pdbfileByte = pdbrequest.downloadHandler.data;
                pdbrequest.Dispose();
                pdb = pdbfileByte;
            }         
        }
        MemoryStream fs = new MemoryStream(dll);
        {
            if (pdb != null)
            {
                MemoryStream pdbStream = new MemoryStream(pdb);
                appdomain.LoadAssembly(fs, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            else 
            {
                appdomain.LoadAssembly(fs);
            }             
        }
        //通过IMethod调用方法;预先获得IMethod，可以减低每次调用查找方法耗用的时间,根据方法名称和参数个数获取方法
        HotFixMainUpdate = appdomain.LoadedTypes[HotFixMainClass].GetMethod("Update", 1);
        InitializeILRuntime(isDebug);
        StartHotFixPro();
    }

    void InitializeILRuntime(bool isDebug)
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        //这里做一些ILRuntime的注册，这个示例暂时没有需要注册的
#endif
        ILRHelper.InitILRuntime(appdomain);
        SetIsDebug(isDebug);
    }
    /// <summary>
    /// 启动热更项目
    /// </summary>
    public void StartHotFixPro()
    {
        appdomain.Invoke("HotFix.Main", "Start", null, null);
    }

    public void SetIsDebug(bool isDebug)
    {

        Debug.Log("是否启用调试:" + (isDebug ? "是" : "否(并且不显示日志)"));
        PlayerPrefs.SetInt("Reporter_msglog", isDebug ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 调用热更工程的访求
    /// </summary>
    /// <param name="method">方法名</param>
    /// <param name="args">参数</param>
    public void CallHotFix(string method, params object[] args)
    {
        appdomain.Invoke(HotFixClass, method , null,args);
    }

    /// <summary>
    /// 返回热更方法的值
    /// </summary>
    /// <param name="method">方法名</param>
    /// <param name="args">参数</param>
    public object CallBackHotFix (string method, params object[] args)
    {
        return  appdomain.Invoke(HotFixClass, method, null, args) ;
    }

    /// <summary>
    /// 通过无GC Alloc方式调用方法模拟Update
    /// </summary>
    /// <param name="deltaTime"></param>
    public void CallHotFixMainUpdate(float deltaTime)
    {
        using (var ctx = appdomain.BeginInvoke(HotFixMainUpdate))
        {
            ctx.PushFloat(deltaTime);
            ctx.Invoke();
        }
    }

    private bool getIsDebug()
    {
        //设置日志开关
        bool isShowLog = false;
#if UNITY_EDITOR
        isShowLog = true;//(PlayerPrefs.GetInt("Reporter_msglog", 1) == 1) ? true : false; //默认显示日志
#else
            isShowLog = (PlayerPrefs.GetInt("Reporter_msglog") == 1) ? true : false;          
#endif
        return isShowLog;
    }
}
