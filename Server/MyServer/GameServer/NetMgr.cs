using System.Threading;
using System.Threading.Tasks;
using GameServer.Net;
using MongoDB.Driver.Linq;
using CSocket;
using System.Net;
using System;
using CommonLib;

namespace GameServer.Net
{
    public class NetMgr
    {
        /// <summary>为客户端提供连接的通讯服务对象</summary>
  
        public ClientToGameServer clientToGameServer;

        public GameToCenterClient gameToCenterClient;

        /// <summary>获得当前客户端总数</summary>
        public int ClientToGameCount => clientToGameServer == null ? 0 : clientToGameServer.ClientCount;
        
        public NetMgr()
        {            
            clientToGameServer = new ClientToGameServer();
            bool isLoginGameServer = clientToGameServer.StartForConfig();
            if (isLoginGameServer)
            {
                Logger.Sys("clientToGameServer 启动成功!");
                clientToGameServer.StartTick();
            }
            ////连接到中央服务器
            gameToCenterClient = new GameToCenterClient();       
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
        
        
    }
}
