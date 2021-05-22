using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using System.Text;
using PbGameSerCommand;

namespace CenterServer
{
    public partial class ServerCommand
    {      
        /// <summary>
        /// 向GameServer 发送控制台命令
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="cmd"></param>
        private static void sendGameCommand(int serverId,string cmd)
        {
            if (string.IsNullOrEmpty(cmd)) return;
            SC_Command comm = new SC_Command();
            comm.Command = cmd;

            //foreach (var server in Glob.gameServer.serverList.Values)
            //{
            //    if (server.ServerId == serverId || serverId == 0)
            //    {
            //        if (server.Session != null)
            //            server.Session.Send(comm);
            //    }
            //}
        }
    }
}
