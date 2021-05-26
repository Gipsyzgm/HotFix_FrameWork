using System;
using System.Net;
using System.Threading;
using System.Timers;
using CommonLib;
using CommonLib.Configuration;
using CSocket;
using Google.Protobuf;
using MongoDB.Driver.Core.Servers;
using Telepathy;

namespace CenterServer.Net
{
    public class GameToCenterServer
    {
        public Server server;
        protected ServerElement config;
        public GameToCenterServer () 
        {
            config = ServerSet.Instance.GetConfig("GameToCenterServer");
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
            var timer = new System.Timers.Timer(1000.0 / 20);
            // THIS HAPPENS IN DIFFERENT THREADS.
            // so make sure that GetNextMessage is thread safe!
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                lock (this)
                {
                    // tick and process as many as we can. will auto reply.
                    // (100k limit to avoid deadlocks)
                    server.Tick(100000);
                }
            };
            timer.AutoReset = true;
            timer.Enabled = true;
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
            Console.WriteLine(connectionId + " Connected");
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnData(int connectionId, ArraySegment<byte> data)
        {
            byte[] buff = data.Array;
            ushort protocol = BitConverter.ToUInt16(buff, 0);   //协议号          
            byte[] body = new byte[buff.Length - 2];
            Array.Copy(buff, 2, body, 0, body.Length);

            IMessage proto = GameToCenterServerProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                Logger.LogError("GameToCenterServerProtocol 协议类型未定义:protocol", protocol);
                return;
            }
            try
            {
                proto.MergeFrom(body);
                GameToCenterServerMessage args = new GameToCenterServerMessage(server,connectionId, proto, protocol);
                Glob.net.GameToCenterServerMessage_Received(args);

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

