using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using PbCenterPlayer;

namespace CenterServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {
        private static void kickPlayer(string playerId)
        {
            ObjectId id = ObjectId.Empty;
            //if (ObjectId.TryParse(playerId, out id))
            //{
            //    if (Glob.playerMgr.dicPlayerList.TryGetValue(id, out var player))
            //    {
            //        SC_Center_PlayerLogout msg = new SC_Center_PlayerLogout();
            //        msg.PlayerId = playerId;
            //        player.Send(msg);
            //    }
            //}

            //Player player;
            //if (Glob.playerMgr.onlinePlayerList.TryGetValue(id, out player))
            //{
            //    string name = player.Data.name;
            //    player.Session.Close();
            //    Logger.Log("踢掉玩家["+ name + "]操作成功!");
            //}
            //else
            //{
            //    Logger.LogError("玩家不存在或者不在线!");
            //}
        }
    }
}
