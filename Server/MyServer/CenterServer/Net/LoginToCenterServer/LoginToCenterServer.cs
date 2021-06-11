using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Timers;
using CommonLib;
using CommonLib.Configuration;
using CSocket;
using Google.Protobuf;
using Telepathy;

namespace CenterServer.Net
{
    public class LoginToCenterServer : Server
    {

        public int ClientCount = 0;
        public LoginToCenterServer(int MaxMessageSize) : base(MaxMessageSize)
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
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnMsgData(int connectionId, ArraySegment<byte> data)
        {
            //先解析一下插件的封装
            byte[] buff  = new byte[data.Count];
            Buffer.BlockCopy(data.Array, data.Offset, buff, 0, data.Count);

            //解析自己定的协议
            ushort protocol = BitConverter.ToUInt16(buff, 0);   //协议号          
            byte[] body = new byte[buff.Length - 2];
            Array.Copy(buff, 2, body, 0, body.Length);

            IMessage proto = LoginToCenterServerProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                Logger.LogError("GameToCenterServerProtocol 协议类型未定义:protocol", protocol);
                return;
            }
            try
            {             
                proto.MergeFrom(body);
                LoginToCenterServerMessage args = new LoginToCenterServerMessage(this,connectionId, proto, protocol);
                LoginToCenterServerAction.Instance.Dispatch(args);
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

