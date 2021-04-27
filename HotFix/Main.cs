using DG.Tweening;
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
            await HotMgr.Initialize();              
            HotMgr.Sound.PlayMusic(SoundName.BGM_EmptyPort);

            await HotMgr.UI.Show<MainUI>(UIAnim.FadeIn,UILoading.Mask);
   
            //Debug.LogError("开始连接服务器");
            //await HotMgr.Net.Connect("127.0.0.1", 1337);
            //byte[] data = System.Text.Encoding.Default.GetBytes("你好");
            //Debug.LogError("发送的是什么："+data);
            //for (int i = 0; i < 50; i++)
            //{
            //    HotMgr.Net.Send(data);
            //}

            for (int i = 0; i < 10; i++)
            {
                Tips.Show("卧槽",true);
            }
         
     

        }

        static int count = 0;
        public static void Update(float deltaTime)
        {
            HotMgr.Timer?.timerUpdateEvent(deltaTime);
            HotMgr.Net.Update();

            if (Input.GetKeyDown(KeyCode.A))
            {
                count++;
                Confirm.Show(() =>
                {
                    Debug.LogError("确定"+ count);
                }, () =>
                {
                    Debug.LogError("确定"+ count);
                }, "随便什么把", count.ToString(), false);
            }
        }
    }
}
