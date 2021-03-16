using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SDKMgr : BaseMgr<SDKMgr>
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// Unity调用安卓接口，参数不限制
    /// </summary>
    /// <param name="funcName"></param>
    /// <param name="parms"></param>

    private static AndroidJavaClass m_AndroidJavaClass = null;

    private static void CallJavaFunction(string funcName, params object[] parms)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (m_AndroidJavaClass == null)
        {
            m_AndroidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        }
        AndroidJavaObject jo = m_AndroidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        if (jo != null)
        {
            jo.Call(funcName, parms);
        }
#endif
    }
    /// <summary>
    /// Unity调用安卓方法 带返回值，只能返回一个值。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="funcName"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public static T CallJavaFunction<T>(string funcName, params object[] parms)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (m_AndroidJavaClass == null)
        {
            m_AndroidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        }
        AndroidJavaObject jo = m_AndroidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        if (jo != null)
        {
            T returnVar = jo.Call<T>(funcName, parms);
            return returnVar;
        }
#endif
        return default(T);
    }


    /// <summary>
    /// 统计事件
    /// </summary>
    public static void SdkSendEvent(string name, string arg1, string arg2)
    {
        CallJavaFunction("SdkSendEvent", name, arg1, arg2);
    }
    /// <summary>
    /// 请求显示开屏广告
    /// </summary>
    public static void loadSplash()
    {
        CallJavaFunction("loadSplash", "请求显示开屏广告");
    }

    /// <summary>
    /// 加载视频广告
    /// </summary>
    public static void LoadRewardAd()
    {
        Debug.Log("UnitySdk" + "加载广告");
        CallJavaFunction("loadAd", "请求视频广告");
    }


    /// <summary>
    /// 显示视频广告
    /// </summary>
    public static void ShowRewardAd(string ID)
    {
        Debug.Log("UnitySdk" + "显示广告" + ID);
        CallJavaFunction("ShowRewardVideo", ID);
    }

    /// <summary>
    /// 广告是否加载完成(带返回值)
    /// </summary>
    /// <param name="Slotid"></param>
    public static bool IsRewardLoaded()
    {
        return CallJavaFunction<bool>("VideoIsReady", "广告加载完成");

    }

    /// <summary>
    /// 广告奖励的回调
    /// </summary>
    /// <param name="ID"></param>
    public void OnVideoReward(string ID)
    {
        Debug.Log("对应奖励的ID：" + ID);
        MainMgr.ILR.CallHotFix("OnRewarded", ID);
    }

}
