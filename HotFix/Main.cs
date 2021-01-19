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
         
            Initialize().AsTask();
        }

        private static async UniTask Initialize()
        {
            await Mgr.Initialize();
            Button button = GameObject.Find("Canvas").transform.Find("Button").GetComponent<Button>();
            if (button != null)
            {

                Debug.Log("空吗？");
                button.onClick.AddListener(() =>
                {
                    Debug.Log("可以吗？");
                });

            }
            else
            {
                Debug.Log("空");
            }
            GameObject gameObject =  GameObject.Find("Main Camera");
            gameObject.GetComponent<Camera>().backgroundColor = Color.green;

            Debug.Log("到这里了吗？");
            Slider slider = GameObject.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
            if (slider != null)
            {
                Debug.Log("sliderBu空");
                slider.onValueChanged.AddListener((f) =>
                {

                    Debug.Log(f);
                });
            }
            else
            {
                Debug.Log("slider空");
            }

            Debug.Log("到这里了吗？111");







        }


        public static void Update(float deltaTime)
        {
            Debug.Log("模拟Update");

        }
    }
}
