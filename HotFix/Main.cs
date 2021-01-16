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

            GameObject obj = GameObject.Find("Main Camera");
            obj.transform.DOMove(Vector3.one, 0f);
   

        }



        public static void Update(float deltaTime)
        {
            Debug.Log("模拟Update");

        }
    }
}
