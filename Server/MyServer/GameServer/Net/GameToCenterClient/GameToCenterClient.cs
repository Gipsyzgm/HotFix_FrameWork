using System;
using System.Collections.Generic;
using CSocket;
using CommonLib;
using Telepathy;
using CommonLib.Configuration;
using System.Timers;
using System.Threading;
using Google.Protobuf;
using PbRegister;

namespace GameServer.Net
{
    public class GameToCenterClient: Client
    {

        public GameToCenterClient(int MaxMessageSize):base(MaxMessageSize)
        {
            OnConnected = OnConnect;
            OnData = OnMsgData;
            OnDisconnected = OnDisconnect;       
        }
        //启动客户端
        public void StartClient(string ip, int port)
        {
            StartConnect(ip, port);
            //多长时间检测一下回调信息，毫秒
            if (!Connected)
            {
                Logger.Log($"没连接上服务器{ip}:{port}");
                return;
            }
            Logger.Log($"已连接上服务器{ip}:{port}");
            var timer = new System.Timers.Timer(1000.0 / 20);
            // THIS HAPPENS IN DIFFERENT THREADS.
            // so make sure that GetNextMessage is thread safe!
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                lock (this)
                {
                    if (Connected)
                    {
                        Tick(1);
                    }
                }
            };
            timer.AutoReset = true;
            timer.Enabled = true;           
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void StartConnect(string ip, int port)
        {
            Connect(ip, port);
            Thread.Sleep(1500);
        }

        public void OnConnect()
        {
            Logger.Log("Connected");
        }

        /// <summary>
        /// 发送消息
        /// </summary>        
        public void SendMsg<T>(T data) where T : IMessage
        {
            if (!Connected)
            {
                Logger.LogError("服务器未连接");
                return;
            }
            ushort protocol = GameToCenterClientProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                Logger.LogError("ProtocolType 协议号未定义,协议类型:" + data.GetType());
                return;
            }
            byte[] body = data.ToByteArray();
            LogMsg(true, protocol, body.Length, data);
            byte[] md5 = null;
            if (GameToCenterClientProtocol.Instance.IsEncryptProtocol(protocol))
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
            Send(new ArraySegment<byte>(package));
        }
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnMsgData(ArraySegment<byte> buff)
        {
            //先解析一下插件的封装
            byte[] bytes = new byte[buff.Count];
            Buffer.BlockCopy(buff.Array, buff.Offset, bytes, 0, buff.Count);

            //解析自己定的协议
            ushort protocol = BitConverter.ToUInt16(bytes, 0);   //协议号          
            byte[] body = new byte[bytes.Length - 2];            //消息内容
            Array.Copy(bytes, 2, body, 0, body.Length);
            IMessage proto = GameToCenterClientProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                Logger.LogError("LoginToCenterClientProtocol 协议类型未定义:protocol", protocol);
                return;
            }
            try
            {
                proto.MergeFrom(body);
                GameToCenterClientMessage msg = new GameToCenterClientMessage(protocol, proto);
                Logger.LogMsg(false, protocol, body.Length, proto);
                Glob.net.GameToCenterClientMessage_Received(msg);
            }
            catch
            {
                Logger.LogError($"消息 {protocol} 解析错误,可能客户端与服务端PB文件不一致");
            }
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        private void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        {
            if (protocol < 10) return;
            Logger.Log($"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]");
        }
        /// <summary>
        /// 断线事件
        /// </summary>
        public void OnDisconnect()
        {
            Console.WriteLine(" Disconnected");
        }
    }
}
