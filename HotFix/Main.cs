using DG.Tweening;
using HotFix.Module.Login;
using HotFix.Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix
{
    public class Main
    {
        public static bool IsDataEnd = false;
        public static bool IsUIEnd = false;
        public static void Start()
        {
            Debug.Log("=====进入热更主程序,启动游戏!!!!=====");
            //Application.targetFrameRate = 60;
            //QualitySettings.vSyncCount = 0;
            Application.runInBackground = true;//用于后台挂起
            Screen.sleepTimeout = SleepTimeout.NeverSleep;//用于禁止屏幕休眠   
            Initialize().Run();
            
        }
        private static async CTask Initialize()
        {
            //热更的初始化
            await HotMgr.Initialize();              
            HotMgr.Sound.PlayMusic(SoundName.BGM_EmptyPort);
            //如果需要显示多个服务器，需要先获取服务器列表
            if (AppSetting.IsMoreServers)
            {
                ServerListMgr.I.ReqServerList().Run(); //请求服务器列表                
            }
            //进入登录页面
            await HotMgr.UI.Show<LoginUI>();
            //显示完登录页面关掉主工程热更页面，热更流程结束，即无缝切换。
            MainMgr.VersionCheck.CloseUpDataUI();
            //等待Login流程结束直到接收到必要的进入游戏的数据。     
            await CTask.WaitUntil(() => { return IsDataEnd;});                   
            await HotMgr.UI.Show<MainUI>();
            //等待游戏主页打开之后关闭Login页面,到此主工程的所有流程结束。
            HotMgr.UI.ClosePanel<LoginUI>();
         

        }

        static int count = 0;
        public static void Update(float deltaTime)
        {
            HotMgr.Timer?.timerUpdateEvent(deltaTime);
            HotMgr.Net.Update();

            if (Input.GetKeyDown(KeyCode.A))
            {
                count++;
                Confirm.ShowConfirm(() =>
                {
                    Debug.LogError("确定"+ count);
                    Confirm.ManualClose();
                }, "随便什么把", HotMgr.Lang.Get("Setting_Sound"), false);
            }
        }
    }
}
