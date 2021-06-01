using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using GameServer.Module;

namespace GameServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {
        private static (bool, string) kickPlayer(string playerId)
        {

            ObjectId id = GetPlayerId(playerId);
            Player player;
            if (Glob.playerMgr.onlinePlayerList.TryGetValue(id, out player))
            {
                string name = player.Data.name;
                return (true,"踢掉玩家["+ name + "]操作成功!");
            }
            else
            {
                return (false, "玩家不存在或者不在线!");
            }
        }
    }
}
