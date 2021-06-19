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
        /// <summary>预制体的对象池</summary>
        public static GameObjectPool GoPool;
        /// <summary>UI管理器</summary>
        public static UIMgr UI;
        /// <summary>声音管理器</summary>
        public static SoundMgr Sound;
        /// <summary>定时器管理器</summary>
        public static TimerMgr Timer;
        /// <summary>多语言管理器</summary>
        public static LangMgr Lang;
        /// <summary>网络消息管理器</summary>
        public static NetMgr Net;
        /// <summary>与服务器相关的时间管理器</summary>
        public static TimeMgr TimeMgr;
        /// <summary>配置表管理器</summary>
        public static ConfigMgr Config;
        ///// <summary>特效管理器</summary>
        //public static EffectMgr Effect;
        ///// <summary>特效管理器</summary>
        //public static UIItemEffectMgr UIItemEffect;


        /// <summary>缓存的数据管理器,用来统一清空缓存数据用</summary>
        public static List<IDisposable> DataMgrList = new List<IDisposable>();
        public static async CTask Initialize()
        {
            GoPool = new GameObjectPool();
            UI = new UIMgr();           
            Sound = new SoundMgr();             
            Timer = new TimerMgr();
            Lang = new LangMgr();
            //非单机模式下才初始化一些网络相关的数据
            if (AppSetting.GameType != GameType.NoServers)
            {
                Net = new NetMgr();
                TimeMgr = new TimeMgr();
            }
            Config = new ConfigMgr();              
            await Config.Initialize();                         
            Debug.Log("初始化完成");

        }


        /// <summary>
        /// 释放全部缓存数据,断线重连后调用
        /// </summary>
        public static void Dispose()
        {
            UI.CloseAll(); //关闭全部UI
            Timer.StopAll();//停掉所有计时器
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