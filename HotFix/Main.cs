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
            await HotMgr.UI.Show<MainUI>(UIAnim.FadeIn,UILoading.Mask);

            time1 = HotMgr.Timer.Loop(2f, () => { Debug.Log("Time1:" + time1); },true);
            time2 = HotMgr.Timer.Loop(2f, () => { Debug.Log("Time2:" + time2); },true);
            time3 = HotMgr.Timer.Loop(2f, () => { Debug.Log("Time3:" + time3); },true);
            HotMgr.Timer.Stop(time1);
            time4 = HotMgr.Timer.Loop(2f, () => { Debug.Log("Time4:" + time4); }, true);
        }


        public static void Update(float deltaTime)
        {
            HotMgr.Timer?.timerUpdateEvent(deltaTime);
        }
    }
}
