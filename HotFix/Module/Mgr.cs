using HotFix_Archer.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotFix_Archer.Effect;
using HotFix_Archer.Sound;
using HotFix_Archer.UIEffect;
using CSF.Tasks;
using UnityEngine;
using HotFix_Archer.Player;
using Random = System.Random;
using HotFix_Archer.Net;

namespace HotFix_Archer
{
    public class Mgr
    {
        /// <summary>UI管理器</summary>
        public static UIMgr UI;
        /// <summary>网络消息管理器</summary>
        public static NetMgr Net;
        /// <summary>多语言管理器</summary>
        public static LangMgr Lang;
        /// <summary>配置表管理器</summary>
        public static ConfigMgr Config;
        /// <summary>时间管理器</summary>
        public static TimeMgr Time;

        /// <summary>定时器管理器</summary>
        public static TimerMgr Timer;

        /// <summary>特效管理器</summary>
        public static EffectMgr Effect;

        /// <summary>声音管理器</summary>
        public static SoundMgr Sound;
        /// <summary>声音管理器</summary>
        public static UIItemEffectMgr UIItemEffect;


        public static LayerMgr Layer;

        public static GameObjectPool GoPool;

        /// <summary>红点管理器</summary>
        //public static RedDotMgr RedDot;

        /// <summary>是否使用HTTP传输消息</summary>   
        public static readonly bool IsHTTPNet = true;

        /// <summary>缓存的数据管理器,用来统一清空缓存数据用</summary>
        public static List<IDisposable> __dataMgrList = new List<IDisposable>();
        public static async CTask Initialize()
        {
            if (!PlayerPrefs.HasKey("UID"))
            {
                Random rd = new Random();
                int i = rd.Next();
                PlayerPrefs.SetString("UID", i.ToString());
            }
            PlayerMgr.UID = PlayerPrefs.GetString("UID");
            GoPool = new GameObjectPool();
            UI = new UIMgr();
            Net = new NetMgr();
            Lang = new LangMgr();
            Config = new ConfigMgr();
            Time = new TimeMgr();
            Timer = new TimerMgr();
            Effect = new EffectMgr();
            Sound = new SoundMgr();
            UIItemEffect = new UIItemEffectMgr();
            Layer = new LayerMgr();
            
            //RedDot = new RedDotMgr();
            float startTime = UnityEngine.Time.realtimeSinceStartup;
            await Config.Initialize();
            float elapsedTime = UnityEngine.Time.realtimeSinceStartup - startTime;
            CLog.Log("Load Config Use Time " + elapsedTime + " seconds");
            await UIItemEffect.Initialize();
        }

        /// <summary>
        /// 释放全部缓存数据,断线重连后调用
        /// </summary>
        public static void Dispose()
        {
            UI.CloseAll(); //关闭全部UI
            Timer.StopAll();
            for (int i = 0; i < __dataMgrList.Count; i++)
                    __dataMgrList[i].Dispose();

            if (Effect != null)
                Effect.Dispose();

            if (Sound != null)
                Sound.Dispose();

            if (Time != null)
                Time.Dispose();
        }

    }
}
