using System;
using CenterServer.Net;
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


        /// <summary>通讯管理器对象</summary>
        public static NetMgr net;

        public static bool Initialize()
        {
            langMgr = new LangMgr();
            config = new ConfigMgr();

            //=====最后再初始化启动定器和 Net
            net = new NetMgr();
            if (!net.Start())
                return false;
            return true;
        }
    }
}