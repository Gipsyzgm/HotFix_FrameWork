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
        /// <summary>
        /// 增加物品
        /// </summary>
        private static (bool, string) addItem(string playerId, int tempId, int count)
        {
            Player player;
            if (TryGetOnlinePlayer(playerId, out player))
            {
                Glob.itemMgr.PlayerAddNewItem(player, tempId, count);
                return (true,"增加物品操作成功!");
            }
            return (false, "没找到在线玩家信息!");
        }     
    }
}
