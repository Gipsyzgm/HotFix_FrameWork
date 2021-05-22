using System;
using Google.Protobuf;
using Telepathy;

namespace CSocket
{
    /// <summary>
    /// 网络通讯事件的参数
    /// </summary>
    public class CServerMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="client">客户端会话</param>
        public CServerMessage(Server server, int clientid)
        {
            Server = server;
            ClientId = clientid;
        }

        public CServerMessage(Server server, int clientid, IMessage msg, ushort protocol)
        {
            Server = server;
            ClientId = clientid;
            Msg = msg;
            Protocol = protocol;
        }

        public virtual void Dispose()
        {
            ClientId = -1;
            Msg = null;
        }

        /// <summary>
        /// 客户端的ID会话对象
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// 当前的Server
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// 相关参数
        /// </summary>
        public IMessage Msg { get; private set; }

        public ushort Protocol { get; private set; }


        /// <summary>
        /// 向会话的客户端发送消息
        /// </summary>
        /// <param name="data"></param>
        public virtual void Send<M>(M data) where M : IMessage
        {


        }

        public virtual void SendMessage(byte[] message, ushort protocol, byte[] md5 = null)
        {
            byte[] package;
            if (md5 != null)
                package = new byte[message.Length + 2 + md5.Length];
            else
                package = new byte[message.Length + 2];
           
            Array.Copy(BitConverter.GetBytes(protocol), 0, package, 0, 2);  //协议号
            Array.Copy(message, 0, package, 2, message.Length);
            if (md5 != null)
                Array.Copy(md5, 0, package, 2 + message.Length, md5.Length);

            Log.Error("发送了消息");
            Server.Send(ClientId, new ArraySegment<byte>(package));
        }


        /// <summary>
        /// 发送共用错误消息
        /// </summary>
        /// <param name="msg"></param>
        public virtual void SendError(string msg)
        {
            //SC_error_code err = new SC_error_code();
            //err.Protocol = Msg == null ? 0 : Protocol;
            //err.Msg = msg;
            //Send(err);
        }
    }
}
