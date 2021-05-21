using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
namespace HotFix
{
    public class HotMgr
    {
        /// <summary>UI管理器</summary>
        public static UIMgr UI;
        ///// <summary>网络消息管理器</summary>
        public static NetMgr Net;
        /// <summary>多语言管理器</summary>
        public static LangMgr Lang;
        /// <summary>配置表管理器</summary>
        public static ConfigMgr Config;
        ///// <summary>时间管理器</summary>
        //public static TimeMgr Time;
        /// <summary>定时器管理器</summary>
        public static TimerMgr Timer;
        ///// <summary>特效管理器</summary>
        //public static EffectMgr Effect;
        /// <summary>声音管理器</summary>
        public static SoundMgr Sound;
        ///// <summary>声音管理器</summary>
        //public static UIItemEffectMgr UIItemEffect;

        //public static LayerMgr Layer;
        /// <summary>预制体的对象池</summary>
        public static GameObjectPool GoPool;

        /// <summary>是否使用HTTP传输消息</summary>   
        public static readonly bool IsHTTPNet = true;

        /// <summary>缓存的数据管理器,用来统一清空缓存数据用</summary>
        public static List<IDisposable> DataMgrList = new List<IDisposable>();
        public static async CTask Initialize()
        {
            GoPool = new GameObjectPool();
            UI = new UIMgr();
            Net = new NetMgr();
            Sound = new SoundMgr();
            Timer = new TimerMgr();
            Lang = new LangMgr();
            Config = new ConfigMgr();
            float startTime = Time.realtimeSinceStartup;
            await Config.Initialize();
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Debug.Log("Load Config Use Time " + elapsedTime + " seconds");       
            Debug.Log("初始化完成");

        }


        /// <summary>
        /// 释放全部缓存数据,断线重连后调用
        /// </summary>
        public static void Dispose()
        {
            //    UI.CloseAll(); //关闭全部UI
            //    Timer.StopAll();
            //    if (Effect != null)
            //        Effect.Dispose();
            //if (Time != null)
            //    Time.Dispose();
            for (int i = 0; i < DataMgrList.Count; i++)
                DataMgrList[i].Dispose();

            if (Sound != null)
                Sound.Dispose();

        }

    }
}