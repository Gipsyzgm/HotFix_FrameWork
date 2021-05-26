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
        /// 发送邮件
        /// 参数:1玩家id(0群邮) 2标题 3内容 4物品(id1_数量,id2_数量)
        /// </summary>
        private static (bool, string) sendMail(List<string> cmds)
        {
            //string playerId = cmds[1];
            //string title = cmds[2];
            //string cont = cmds[3];

            //List<int[]> itemInfos = StringHelper.SplitToItems(StringHelper.SplitToArr<string>(cmds[4], ','));

            //if (playerId == "0") //群发邮件
            //{
            //    Glob.mailMgr.SendMassMail(title, cont, itemInfos, true);
            //    return(true,"群发送邮件操作成功!");
            //}
            //else //个人邮件
            //{
            //    ObjectId id = GetPlayerId(playerId);
            //    if (Glob.tPlayerMgr.playerDataList.ContainsKey(id))
            //    {
            //        Glob.mailMgr.SendPersonMail(id, title, cont, itemInfos, true);
            //        return (true, "发送个人邮件操作成功!");
            //    }
            //    else
            //    {
            //        return (false, "未找到玩家信息!");
            //    }
            //}
            return (false, "功能暂是不可用!");
        }
      
    }
}
