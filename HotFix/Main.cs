using DG.Tweening;
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
            Initialize().Run();
        }

        static int time1 = 0;
        static int time2 = 0;
        static int time3 = 0;
        static int time4 = 0;
        private static async CTask Initialize()
        {
            await HotMgr.Initialize();

            Debug.Log("结束等待");
            HotMgr.Sound.PlayMusic(SoundName.BGM_EmptyPort);
            await HotMgr.UI.Show<MainUI>(UIAnim.FadeIn,UILoading.Mask);
        }


        public static void Update(float deltaTime)
        {
            HotMgr.Timer?.timerUpdateEvent(deltaTime);
        }
    }
}
