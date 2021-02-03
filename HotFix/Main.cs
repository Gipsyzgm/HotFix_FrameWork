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

        private static async CTask Initialize()
        {
            await Mgr.Initialize();
            Debug.Log("结束等待");
            await Mgr.UI.Show<MainUI>(UIAnim.FadeIn,UILoading.Mask);

        }


        public static void Update(float deltaTime)
        {
            Debug.Log("模拟Update");

        }
    }
}
