using System;
using System.Collections.Generic;
namespace GameServer.Net
{
    /// <summary>
    /// 工具生成，不要修改
    /// </summary>
    public class ClientToCrossTransit
    {
        public HashSet<ushort> requestProtocols = new HashSet<ushort>();
        public HashSet<ushort> returnProtocols = new HashSet<ushort>();
        private HashSet<int> _encryptList = new HashSet<int>();
        private static readonly ClientToCrossTransit instance = new ClientToCrossTransit();
        public static ClientToCrossTransit Instance => instance;
        
        private ClientToCrossTransit()
        {
            //请求中转的消息


            //返回中转的消息

 
        }
        /// <summary>是否加密协议</summary>
        public bool IsEncryptProtocol(ushort protocl)
        {
            return _encryptList.Contains(protocl);
        }
    }
}
