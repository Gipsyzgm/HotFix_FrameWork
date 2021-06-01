using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;

namespace GameServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {
        /// <summary>
        /// 输出玩家信息
        /// 参数:1玩家id(0群邮) 2标题 3内容 4物品(id1_数量,id2_数量)
        /// </summary>
//        private static (bool, string) playerInfo(List<string> cmds)
//        {
//            string playerId = cmds[1];
//            int spid = 0;
//            Int32.TryParse(playerId, out spid);
//            ObjectId pid = ObjectId.Empty;
//            if (spid == 0)
//            {
//                pid = new ObjectId(playerId);
//                spid = TPlayer.ToShortId(pid);
//            }
//            else
//                pid = TPlayer.ToObjectId(spid);

//            TAccount tacc;
//            TPlayer tplayer;
//            Player player;
//            bool isOnline = false;
//            if (Glob.playerMgr.onlinePlayerList.TryGetValue(pid, out player))
//            {
//                tplayer = player.Data;
//                tacc = player.AccountData;
//                isOnline = true;
//            }
//            else
//            {
//                Glob.tPlayerMgr.playerDataList.TryGetValue(pid, out tplayer);
//                Glob.tAccountMgr.accountList.TryGetValue(pid, out tacc);
//            }
//            if (tplayer == null)
//            {
//                return (false, "玩家不存在!");
//            }
//            string str = $@"玩家名:{tplayer.name} ObjectId:{pid} SID:{spid} 是否在线:{isOnline}　充值金额:{tplayer.vipExp}
//平台:{tacc.pfCh} 平台用户Id:{tacc.pfId} 服务器Id:{tacc.serverId} 连续登录天数:{tacc.keepLoginNum} 
//等级:{tplayer.level} VIP等级:{tplayer.vipLv} 钻石:{tplayer.ticket} 金币:{tplayer.gold}
//注册时间:{tacc.regDate} 最后登录时间:{tacc.lastLoginDate}  最后下线时间:{tacc.lastLogoutDate}";
//            return (true, str);
//        }      
    }
}
