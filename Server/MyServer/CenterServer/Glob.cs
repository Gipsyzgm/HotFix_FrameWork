using System;
using CenterServer.Net;

namespace CenterServer
{
    public class Glob
    {
        /// <summary>通讯管理器对象</summary>
        public static NetMgr net;

        public static bool Initialize()
        {
            //=====最后再初始化启动定器和 Net
            net = new NetMgr();
            if (!net.Start())
                return false;
            return true;
        }
    }
}