using Cysharp.Threading.Tasks;
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
            Initialize();
        }

        private static async UniTask Initialize()
        {
            await Mgr.Initialize();
        
            //await Mgr.UI.Show<LoginUI>().Await();         
            //await Mgr.UI.Show<War.WarUI>().Await();

            //加载玩家数据
            //DataMgr.I.LoadAll();
            //await LoginMgr.I.Login();  //登录游戏


        }


        public static void Update(float deltaTime)
        {
            Debug.Log("模拟Update");

        }
    }
}
