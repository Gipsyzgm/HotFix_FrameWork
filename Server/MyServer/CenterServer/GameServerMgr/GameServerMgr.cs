using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterServer.Player;
using CommonLib;
using PbCenterPlayer;

namespace CenterServer.GameServer
{
    /// <summary>
    /// 游戏服务器管理器
    /// </summary>
    public class GameServerMgr
    {
        //服务器ID 对应的服务器信息
        public DictionarySafe<int, GameServerData> serverList = new DictionarySafe<int, GameServerData>();
        //连接IP和端口对应的服务器信息
        public DictionarySafe<string, GameServerData> ipportServerList = new DictionarySafe<string, GameServerData>();
        private static readonly object o = new object();
        static int _maxServerID = 0;
        private int singleOnlineMax = 0;
        public GameServerMgr()
        {
            singleOnlineMax = int.Parse(ConfigurationManager.AppSettings["SingleServerOnlineMax"]);
            if (singleOnlineMax <= 0)
                singleOnlineMax = 2000;
            Logger.Sys($"单服最高同时在线人数限制为:{singleOnlineMax}人");
        }
        //返回最高在线
        public int getSunOnline() {
            return serverList.Values.Sum(t => t.OnlineCount);
        }


        //增加一个新的服务器
        public int AddNew(GameServerData data)
        {
            lock (o)
            {
                if (data.ServerId == 0)
                    data.ServerId = ++_maxServerID;
                else
                    _maxServerID = Math.Max(_maxServerID, data.ServerId);
            }
            serverList.Add(data.ServerId, data);
            ipportServerList.Add(data.IPPort, data);
            return data.ServerId;  
        }

        public void AddPlayer(PlayerData player)
        {
            if (serverList.TryGetValue(player.ServerId, out var server))
            {
                server.playerList.AddOrUpdate(player.Id,player);
                //发送数据
                SC_Center_PlayerLogin sendMsg = new SC_Center_PlayerLogin();
                sendMsg.SessionId = player.SessionId;
                sendMsg.PlayerId = player.Id.ToString();
                sendMsg.IsReLogin = player.IsReLogin;
         
                player.Send(sendMsg);
            }
        }

        public void RemovePlayer(PlayerData player)
        {
            if (serverList.TryGetValue(player.ServerId, out var server))
            {
                server.playerList.Remove(player.Id);
            }
        }
        //移除服务器的全部人员信息
        public void RemoveServerAllPlayer(int serverId)
        {
            if (serverList.TryGetValue(serverId, out var server))
            {
                foreach (var id in server.playerList.Keys)
                    Glob.playerMgr.dicPlayerList.Remove(id);
                server.playerList.Clear();
                server.server = null;
            }
        }

        //获取人数最少的服务器信息
        public GameServerData GetMinServer()
        {
            GameServerData minData = null;
            foreach (var data in serverList.Values)
            {
                if (data.server == null) continue;
                if (minData == null || data.OnlineCount < minData.OnlineCount)
                    minData = data;
            }
            if (minData == null) return null;
            if (minData.OnlineCount >= singleOnlineMax)
            {
                Logger.LogError("服务器人数已满，请开服!!!!!!!");
                return null;
            }
            else if (minData.OnlineCount >= singleOnlineMax - 100)
            {
                Logger.LogWarning($"人数最少的服务器还可登{singleOnlineMax - minData.OnlineCount}人,请增加服务器!!!");
            }
            return minData;
        }
    }
}
