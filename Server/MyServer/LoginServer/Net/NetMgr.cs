using CommonLib;
using CommonLib.Configuration;
using CSocket;
using Google.Protobuf;
using LoginServer;
using LoginServer.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Telepathy;

namespace Telepathy
{
    public class NetMgr
    {
        public Client _socket;
        public const int MaxMessageSize = 16 * 1024;
        public DictionarySafe<string, HttpListenerContext> dicHttpContext = new DictionarySafe<string, HttpListenerContext>();

        public NetMgr()
        {
            _socket = new Client(MaxMessageSize);
            _socket.OnConnected = OnConnected;
            _socket.OnData = OnData;
            _socket.OnDisconnected = OnDisconnected;
            ClientElement config = ClientSet.Instance.GetConfig("LoginToCenterClient");
            Logger.Log("ip:" + config.ip + "port:" + config.port);
            StartClient(config.ip, config.port);
        }

        public void StartClient(string ip ,int port) 
        {
            Connect(ip, port);
            //多长时间检测一下回调信息，毫秒
            if (!_socket.Connected)
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
                if (_socket.Connected)
                {                   
                    _socket.Tick(1);
                }                         
            };
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect(string ip, int port)
        {
            _socket.Connect(ip, port);
            Thread.Sleep(1500);
        }

        public void OnConnected()
        {
            Logger.Log("Connected");
        }

        public bool IsConnect => _socket.Connected;
        public bool mIsConnect = false;

        /// <summary>
        /// 发送消息的重载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="requestUID"></param>
        /// <param name="context"></param>
        public void Send<T>(T data, string requestUID, HttpListenerContext context) where T : IMessage
        {
            dicHttpContext.Add(requestUID, context);
            Send(data);
        }

        /// <summary>
        /// 发送消息
        /// </summary>        
        public void Send<T>(T data) where T : IMessage
        {
            if (!IsConnect)
            {
                Logger.LogError("服务器未连接"); 
                return;
            }
            ushort protocol = LoginToCenterClientProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                Logger.LogError("ProtocolType 协议号未定义,协议类型:" + data.GetType());
                return;
            }      
            byte[] body = data.ToByteArray();
            LogMsg(true, protocol, body.Length, data);
            byte[] md5 = null;
            if (LoginToCenterClientProtocol.Instance.IsEncryptProtocol(protocol))
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
            _socket.Send(new ArraySegment<byte>(package));
        }
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnData(ArraySegment<byte> buff)
        {
            Log.Error("收到消息");
            //先解析一下插件的封装
            byte[] bytes = new byte[buff.Count];
            Buffer.BlockCopy(buff.Array, buff.Offset, bytes, 0, buff.Count);  

            //解析自己定的协议
            ushort protocol = BitConverter.ToUInt16(bytes, 0);   //协议号          
            byte[] body = new byte[bytes.Length - 2];            //消息内容
            Array.Copy(bytes, 2, body, 0, body.Length);
            IMessage proto = LoginToCenterClientProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                Logger.LogError("LoginToCenterClientProtocol 协议类型未定义:protocol", protocol);
                return;
            }
            try
            {
                proto.MergeFrom(body);
                LoginToCenterClientMessage msg = new LoginToCenterClientMessage(protocol, proto);
                Logger.LogMsg(false, protocol, body.Length, proto);
                LoginToCenterClientAction.Instance.Dispatch(msg);
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
        public void OnDisconnected()
        {
            Console.WriteLine(" Disconnected");
        }
  
    }
}
