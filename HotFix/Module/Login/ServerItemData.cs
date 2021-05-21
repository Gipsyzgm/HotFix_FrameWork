using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix.Module.Login
{   
    /// <summary>
    /// 服务器数据
    /// </summary>
    public class ServerItemData
    {
        /// <summary>服务器Id</summary>
        public int ServerId;
        /// <summary>服务器状态  0 正常 1 维护</summary>
        public int State;
        /// <summary>服务器标记 0无 1新服 2推荐</summary>
        public int Flag;
        /// <summary>服务器名</summary>
        public string ServerName;
        /// <summary>服务连接IP</summary>
        public string IP;
        /// <summary>服务连接端口</summary>
        public int Port;
    }
}
