using System;
using System.Collections.Generic;
using System.Timers;
using CommonLib;
using CommonLib.Configuration;
using CSocket;
using Google.Protobuf;
using Telepathy;

namespace GameServer.Net
{
    public class ClientToGameServer: Server
    {
        public int ClientCount = 0;
        public ClientToGameServer(int MaxMessageSize) : base(MaxMessageSize)
        {
            OnConnected = OnConnect;
            OnData = OnMsgData;
            OnDisconnected = OnDisconnect;
        }

        //从配置文件里读取启动
        public bool StartForConfig(int port)
        {
            return Start(port);
        }

        public void StartTick()
        {
            var timer = new System.Timers.Timer(1000.0 / 20);
            // THIS HAPPENS IN DIFFERENT THREADS.
            // so make sure that GetNextMessage is thread safe!
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                lock (this)
                {
                    // tick and process as many as we can. will auto reply.
                    // (100k limit to avoid deadlocks)
                    Tick(100000);
                }
            };
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void OnConnect(int connectionId)
        {
            Console.WriteLine(connectionId + " Connected");
            ClientCount++;
        }



        /// <summary>
        /// 向会话的客户端发送消息
        /// </summary>
        /// <param name="data"></param>
        public void Send<M>(M data, int connectionId) where M : IMessage
        {
            ushort protocol = ClientToGameServerProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                Logger.LogError("ProtocolType 协议号未定义,协议类型:" + data.GetType());
                return;
            }
            byte[] body = data.ToByteArray();
            Logger.LogMsg(true, protocol, body.Length, data);
            byte[] md5 = null;
            if (ClientToGameServerProtocol.Instance.IsEncryptProtocol(protocol))
                md5 = CSocketUtils.MD5Encrypt(ref body, protocol);
            byte[] package;
            if (md5 != null)
                package = new byte[body.Length + 2 + md5.Length];
            else
                package = new byte[body.Length + 2];
            Array.Copy(BitConverter.GetBytes(protocol), 0, package, 0, 2);  //协议号
            Array.Copy(body, 0, package, 2, body.Length);
            if (md5 != null)
                Array.Copy(md5, 0, package, 2 + body.Length, md5.Length);
            Send(connectionId, new ArraySegment<byte>(package));
        }


        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnMsgData(int connectionId, ArraySegment<byte> data)
        {
            //先解析一下插件的封装
            byte[] buff = new byte[data.Count];
            Buffer.BlockCopy(data.Array, data.Offset, buff, 0, data.Count);

            ushort protocol = BitConverter.ToUInt16(buff, 0);   //协议号          
            byte[] body = new byte[buff.Length - 2];
            Array.Copy(buff, 2, body, 0, body.Length);

            IMessage proto = ClientToGameServerProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                Logger.LogError("GameToCenterServerProtocol 协议类型未定义:protocol", protocol);
                return;
            }
            try
            {
                proto.MergeFrom(body);
                ClientToGameServerMessage args = new ClientToGameServerMessage(this, connectionId, proto, protocol);
                Glob.net.ClientToGameServerMessage_Received(args);

            }
            catch
            {
                Logger.LogError($"消息 {protocol} 解析错误,可能客户端与服务端PB文件不一致");
            }
        }

        /// <summary>
        /// 断线事件
        /// </summary>
        public void OnDisconnect(int connectionId)
        {
            Logger.LogError(connectionId + " Disconnected");
            ClientCount--;
        }
    }
}
