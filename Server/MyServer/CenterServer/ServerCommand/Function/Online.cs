using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using System.Text;
using CommonLib;

namespace CenterServer
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
        private static void online()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"当前在线人数：{Glob.gameServer.serverList.Values.Sum(t => t.OnlineCount)}");
            //foreach (var server in Glob.gameServer.serverList.Values)
            //{
            //    sb.AppendLine($"服务器ID:{server.ServerId} 人数:{server.OnlineCount} 服务器状态:{(server.Session==null?"断开":"正常")}");
            //}
            Logger.Sys(sb);
        }      
    }
}
