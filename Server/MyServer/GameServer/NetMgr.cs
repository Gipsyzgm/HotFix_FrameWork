using System.Threading;
using System.Threading.Tasks;
using GameServer.Net;
using MongoDB.Driver.Linq;
using CSocket;
using System.Net;
using System;
using CommonLib;
using CommonLib.Configuration;
using PbRegister;

namespace GameServer.Net
{
    public class NetMgr
    {

     
        /// <summary>为客户端提供连接的通讯服务对象</summary>

        public ClientToGameServer clientToGameServer;

        public GameToCenterClient gameToCenterClient;
        public ServerElement config;

        public int ServerId = 0;
        public int Port = 0;
        /// <summary>获得当前客户端总数</summary>
        public int ClientToGameCount => clientToGameServer == null ? 0 : clientToGameServer.ClientCount;
        public bool isLoginGameServer = false;
        public NetMgr()
        {
            config = ServerSet.Instance.GetConfig("ClientToGameServer");
            clientToGameServer = new ClientToGameServer(config.receiveBufferSize);
            if (config == null) 
            {
                Logger.LogError("读取ClientToGameServer配置失败！");
            };
            Port = config.port;
            if (config.autoPort == true)
            {
                while (true)
                {
                    if (!CSocketUtils.CheckPortIsUse(Port))
                        break;
                    else
                        Port++;
                }
            }
            bool isLoginGameServer = clientToGameServer.StartForConfig(Port);
            if (isLoginGameServer)
            {
                Logger.Sys("clientToGameServer 启动成功!");
            }
            //连接到中央服务器
            ClientElement config1 = ClientSet.Instance.GetConfig("GameToCenterClient");
            gameToCenterClient = new GameToCenterClient(config1.receiveBufferSize);
            gameToCenterClient.StartClient(config1.ip,config1.port);
            RegisterToCenterServer(config.netIP, Port, ServerId);
        }

        //通知中央服务器注册服务器信息
        public void RegisterToCenterServer(String netIP, int Port, int ServerId)
        {
            Logger.Log($"发送消息");
            CS_Register_GameServer msg = new CS_Register_GameServer();
            msg.ServerIP = netIP;
            msg.ServerPort = Port;
            msg.ServerId = ServerId;
            gameToCenterClient.SendMsg(msg);
        }


        //GameServer 收到客户端发过来的消息
        public void ClientToGameServerMessage_Received(ClientToGameServerMessage args)
        {          
            ClientToGameServerAction.Instance.Dispatch(args);
        }

        //GameServer 收到中央服返回的消息
        public void GameToCenterClientMessage_Received(GameToCenterClientMessage args)
        {
            GameToCenterClientAction.Instance.Dispatch(args);
        }


        public void StartTick()
        {        
            if (isLoginGameServer)
            {
                clientToGameServer.Tick(100000);
            }
            if (gameToCenterClient.Connected)
            {
                gameToCenterClient.Tick(1);
            }
        }


    }
}
