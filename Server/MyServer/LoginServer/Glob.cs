using LoginServer.Http;
using LoginServer.Net;
using LoginServer.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    public class Glob
    {
        /// <summary>Web HTTP服务</summary>
        public static HttpServer http;

        /// <summary>通讯管理器对象</summary>
        public static NetMgr net;

        /// <summary>平台管理器</summary>
        public static PlatformMgr platformMgr;

        /// <summary>服务器定时器管理</summary>
        public static TimerMgr timerMgr;

        /// <summary>服务器信息管理</summary>
        public static ServerStateMgr serverStateMgr;

        public static bool Initialize()
        {
            //初始化服务平台配置
            platformMgr = new PlatformMgr();
            //初始化服务器信息
            serverStateMgr = new ServerStateMgr();
            //初始化定时器
            timerMgr = new TimerMgr();
            //开启网络连接服务，作为服务器接收客户端的登录请求
            http = new HttpServer();
            //开启网络连接服务，作为客户端连接对应的客户端服务器
            net = new NetMgr();
         
            return true;
        }
    }
}
