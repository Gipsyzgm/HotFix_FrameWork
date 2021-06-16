using CommonLib;
using MongoDB.Bson;
using PbCenterPlayer;
using PbRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterServer.Player
{
    public class PlayerMgr
    {
        //玩家总表
        public DictionarySafe<ObjectId, PlayerData> dicPlayerList = new DictionarySafe<ObjectId, PlayerData>();
        //GameServerData 里记录的是服务内的玩家信息 
        public void AddPlayer(string playerId,int sessionid,int serverid,bool isReLogin,int loginType)
        {
            PlayerData data = null;
            ObjectId id = ObjectId.Parse(playerId);

            if (!dicPlayerList.TryGetValue(id, out data))  //不存在
            {
                data = new PlayerData();
                data.Id = id;                
                dicPlayerList.Add(data.Id, data);
            }
        
            //重新修改SessionId和服务器Id
            data.SessionId = sessionid;
            data.ServerId = serverid;
            data.IsReLogin = isReLogin;


            //通知玩家下线                  
            if (Glob.gameServer.serverList.TryGetValue(serverid, out var server))
            {
                if (server.playerList.TryGetValue(id, out var sdata))
                {
                    if (server.server != null)
                    {
                        data.LoginSessionId = sessionid; //正在登录的SessionId
                        //玩家在线，通知T下线
                        SC_Center_PlayerLogout msg = new SC_Center_PlayerLogout();
                        msg.PlayerId = playerId;
                        server.server.Send(msg, data.LoginSessionId);
                        return;
                    }
                }
            }
           
            Glob.gameServer.AddPlayer(data);
        }

        public void RemovePlayer(string playerId)
        {
            ObjectId id = ObjectId.Parse(playerId);
            if (dicPlayerList.TryGetValue(id, out var player))
            {
                if (player.LoginSessionId != 0) //登录时被T下来了
                {
                    player.SessionId = player.LoginSessionId;
                    player.LoginSessionId = 0;
                    Glob.gameServer.AddPlayer(player);
                }
                else
                {
                    dicPlayerList.Remove(id);
                    Glob.gameServer.RemovePlayer(player);
                }
            }
        }


    }
}
