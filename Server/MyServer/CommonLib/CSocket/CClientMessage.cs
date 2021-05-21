using System;
using Google.Protobuf;

namespace CSocket
{
    /// <summary>
    /// 客户端收到消息参数
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

        public void Dispose()
        {
            Protocol = 0;
            Msg = null;
        }
    }
}