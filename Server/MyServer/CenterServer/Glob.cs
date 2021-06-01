using System;
using CenterServer.GameServer;
using CenterServer.Net;
using CenterServer.Player;
using CommonLib.Comm;

namespace CenterServer
{
    public class Glob
    {
        /// <summary>语言管理</summary>
        public static LangMgr langMgr;
        /// <summary>配置数据</summary>
        public static ConfigMgr config;
        /// <summary>游戏服管理器</summary>
        public static GameServerMgr gameServer;
        /// <summary>玩家管理器</summary>
        public static PlayerMgr playerMgr;
        /// <summary>通讯管理器对象</summary>
        public static NetMgr net;
        /// <summary记时器</summary>
        public static TimerMgr timerMgr;

        public static bool Initialize()
        {
            langMgr = new LangMgr();
            config = new ConfigMgr();
            gameServer = new GameServerMgr();
            playerMgr = new PlayerMgr();
            timerMgr = new TimerMgr();

            //=====最后再初始化启动定器和 Net
            net = new NetMgr();
            if (!net.Start())
                return false;
            return true;
        }
    }
}