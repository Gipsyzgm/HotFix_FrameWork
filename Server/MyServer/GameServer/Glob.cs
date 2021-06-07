using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using GameServer.Module;
using GameServer.Net;
using System;



namespace GameServer
{
    public class Glob
    {
        /// <summary>通讯管理器对象</summary>
        public static NetMgr net;

        /// <summary>配置数据</summary>
        public static ConfigMgr config;

        /// <summary>服务器定时器管理</summary>
        public static TimerMgr timerMgr;

        /// <summary> 账号数据管理器</summary>
        public static TAccountMgr tAccountMgr;

        /// <summary> 玩家数据管理器</summary>
        public static TPlayerMgr tPlayerMgr;

        /// <summary>玩家实体管理器</summary>
        public static PlayerMgr playerMgr;

        /// <summary>英雄管理</summary>
        public static HeroMgr hreoMgr;
        /// <summary>物品管理</summary>
        public static ItemMgr itemMgr;

        /// <summary>福利管理</summary>
        public static BonusMgr bonusMgr;

        /// <summary>Vip管理</summary>
        public static VipMgr vipMgr;

        /// <summary>活动礼包</summary>        
        //public static PayPackMgr payPackMgr;

        /// <summary>充值管理</summary>
        public static PayMgr payMgr;

        /// <summary>CDKey管理</summary>
        public static CDKeyMgr cdkeyMgr;

        /// <summary>邮件机管理</summary>
        public static MailMgr mailMgr;


        /// <summary>语言管理</summary>
        public static LangMgr langMgr;

        /// <summary>副本挑战赛</summary>
        public static FBMgr fbMgr;


        /// <summary>竞技场管理器</summary>
        //public static ArenaMgr arenaMgr;
        /// <summary>任务管理器</summary>
        public static TaskMgr taskMgr;

        /// <summary>活动管理器</summary>
        public static ActivityMgr activityMgr;


        /// <summary>商城管理器</summary>
        public static StoreMgr storeMgr;
        /// <summary>排行榜</summary>
        public static RankMgr rankMgr;

        /// <summary>玩家红点</summary>
        public static PlayerRedDotMgr redDotMgr;
        

        /// <summary>
        /// 服务器开启时间
        /// </summary>
        public static int ServerStartTime;

        public static bool Initialize()
        {
            langMgr = new LangMgr();
            config = new ConfigMgr();
            config.Initialize();
            if (!DBWrite.Instance.Initialize())
                return false;
            if (!DBReader.Instance.Initialize())
                return false;
            if (!DBReaderPlayer.Instance.Initialize())
                return false;
            tAccountMgr = new TAccountMgr();
            tPlayerMgr = new TPlayerMgr();
            playerMgr = new PlayerMgr();
            hreoMgr = new HeroMgr();
            itemMgr = new ItemMgr();
            bonusMgr = new BonusMgr();
            vipMgr = new VipMgr();
            payMgr = new PayMgr();
            cdkeyMgr = new CDKeyMgr();
            mailMgr = new MailMgr();
            fbMgr = new FBMgr();
            taskMgr = new TaskMgr();
            activityMgr = new ActivityMgr();
            storeMgr = new StoreMgr();
            redDotMgr = new PlayerRedDotMgr();
            rankMgr = new RankMgr();
            
            //=====最后再初始化启动定器和 Net
            timerMgr = new TimerMgr();
            net = new NetMgr();

            ServerStartTime = DateTime.Now.ToTimestamp();

            return true;
        }
    }
}