using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterServer.Net;
using CenterServer.Player;
using CommonLib;
using CSocket;
using MongoDB.Bson;
using Telepathy;

namespace CenterServer.GameServer
{
    public class GameServerData
    {
        /// <summary>服务器ID</summary>
        public int ServerId;

        /// <summary>连接Session</summary>
        public GameToCenterServer server;

        /// <summary>服务器外网IP</summary>
        public string ServerNetIP;

        /// <summary>服务器端口</summary>
        public int ServerPort;

        /// <summary>在线人数</summary>
        public int OnlineCount => playerList.Count;

        public string IPPort => ServerNetIP + ":" + ServerPort;

        /// <summary>在线玩家信息</summary>
        public DictionarySafe<ObjectId, PlayerData> playerList = new DictionarySafe<ObjectId, PlayerData>();

        public bool IsConnected => server != null;

    }
}
