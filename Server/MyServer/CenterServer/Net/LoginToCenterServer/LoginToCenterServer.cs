using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using CommonLib;
using CommonLib.Configuration;
using CSocket;
using Google.Protobuf;
using Telepathy;

namespace CenterServer.Net
{
    public class LoginToCenterServer
    {
        public Server server;
        protected ServerElement config;
        public LoginToCenterServer()
        {
            config = ServerSet.Instance.GetConfig("LoginToCenterServer");
            if (config == null)
                Logger.LogError("GameToCenterServer配置未找到");
            InitForConfig();
        }
        public void InitForConfig()
        {
            int numConnections = 0;
            int receiveBufferSize = 0;
            if (config != null)
            {
                numConnections = config.maxConnection;
                receiveBufferSize = config.receiveBufferSize;
            }
            //从配置表里读取参数
            server = new Server(receiveBufferSize);
            server.OnConnected = OnConnected;
            server.OnData = OnData;
            server.OnDisconnected = OnDisconnected;

        }
        //从配置文件里读取启动
        public bool StartForConfig()
        {
            if (config == null) return false;
            return server.Start(config.port);
        }

        public void StartTick() 
        {         
            while (true)
            {
                // tick and process as many as we can. will auto reply.
                // (100k limit to avoid deadlocks)
                server.Tick(100000);
                // sleep
                Thread.Sleep(1000 / 60);             
            }
        }


        /// <summary>  
        /// 客户端连接数量变化时触发  
        /// </summary>  
        /// <param name="num">当前增加客户的个数(用户退出时为负数,增加时为正数,为1)</param>  
        /// <param name="token">增加用户的信息</param>  
        //protected override void OnClientNumberChange(int num, GameToCenterServerSession token)
        //{
        //    if (num < 0) //GameServer 与 中央服断开连接
        //    {
        //        Glob.gameServer.RemoveServerAllPlayer(token.GameServerId);             
        //    }
        //}

        public void OnConnected(int connectionId)
        {
            Logger.Log(connectionId + " Connected");
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnData(int connectionId, ArraySegment<byte> data)
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
                LoginToCenterServerMessage args = new LoginToCenterServerMessage(server,connectionId, proto, protocol);
                Glob.net.LoginToCenterServerMessage_Received(args);
            }
            catch
            {
                Logger.LogError($"消息 {protocol} 解析错误,可能客户端与服务端PB文件不一致");
            }
        }

        /// <summary>
        /// 断线事件
        /// </summary>
        public void OnDisconnected(int connectionId)
        {
            Logger.LogError(connectionId + " Disconnected");
        }
    }
}

