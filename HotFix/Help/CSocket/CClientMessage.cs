using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace CSocket
{
    /// <summary>
    /// 网络消息,收到消息时包装一下，发送不需要
    /// </summary>
    public class CClientMessage
    {
        public ushort Protocol { get; private set; }

        public IMessage Msg { get; private set; }
        /// <summary>
        /// 二进制结构体转为消息
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="bytes"></param>
        public CClientMessage(ushort protocol, IMessage msg)
        {
            Protocol = protocol;
            Msg = msg;
        }
    }
}
