using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using Google.Protobuf;
using CommonLib;
using CommonLib.Comm.DBMgr;
using GameServer;

namespace GameServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {
        public static (bool, string) Command(string cmd)
        {
            (bool, string) opionInfo = (true, "");
            try
            {
                int templId = 0;
                int count = 0;
                List<string> cmdS = cmd.ToLower().Split(' ').ToList();
                cmdS.Add(string.Empty);  //加几个默认参数
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                cmdS.Add(string.Empty);
                switch (cmdS[0])
                {
                    case "-online":
                        opionInfo.Item2 = "在线人数:" + Glob.playerMgr.onlinePlayerList.Count;
                        break;
                    case "-msglog":
                        Logger.MsgLogOnOff = !Logger.MsgLogOnOff;
                        opionInfo.Item2 = "消息日志:" + (Logger.MsgLogOnOff ? "开启" : "关闭");
                        break;
                    case "-resetday":   //重置每天数据
                        Glob.timerMgr.ResetDayCmd();
                        opionInfo.Item2 = "重置每天数据操作成功！";
                        break;
                    case "-additem":
                        templId = tryParseInt(cmdS[2]);
                        count = tryParseInt(cmdS[3], 1);
                        opionInfo = addItem(cmdS[1], templId, count);
                        break;
                    case "-addticket":
                        count = tryParseInt(cmdS[2], 10000);
                        opionInfo = addItem(cmdS[1], (int)EItemSubTypeVirtual.Ticket, count);
                        break;
                    case "-sendmail":
                        opionInfo = sendMail(cmdS);
                        break;
                    case "-reloadconfig":
                        Glob.config.ReloadConfig();
                        opionInfo.Item2 = "重新加载配置表完成！";
                        break;
                    case "-t":
                        opionInfo = kickPlayer(cmdS[1]);
                        break;
                    case "-gc":
                        GC.Collect();
                        Logger.Sys("执行GC");
                        break;
                    case "-exit": //退出程序                      
                        //先关闭Web所有接口，以防有充值数数进入
                        Glob.net?.clientToGameServer.Stop();            
                        //倒计时关闭
                        System.Timers.Timer exitTimer = new System.Timers.Timer(1000);//     
                        exitTimer.Elapsed += new System.Timers.ElapsedEventHandler(exit_ElapsedCheck);
                        exitTimer.Start();
                        break;
                    default:
                        opionInfo = (false, "未找到命令行:" + cmdS[0]);
                        break;
                     

                }
            }
            catch (Exception ex)
            {
                opionInfo = (false, ex.Message);
            }

            if (opionInfo.Item1)
                Logger.Sys(opionInfo.Item2);
            else
                Logger.LogError(opionInfo.Item2);

            return opionInfo;
        }
        /// <summary>
        /// 字符串参数转为Int类型
        /// </summary>
        /// <param name="cmdArg">字符串参数</param>
        /// <param name="defVal">默认值</param>
        /// <returns></returns>
        private static int tryParseInt(string cmdArg,int defVal = 0)
        {
            int value = defVal;
            if (cmdArg != string.Empty)
                Int32.TryParse(cmdArg, out value);
            return value;
        }
        private static void exit_ElapsedCheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {       
                ((System.Timers.Timer)sender).Stop();
                Logger.Sys("服务器即将关闭..");
                Thread.Sleep(500);
                Environment.Exit(0);//关闭程序       
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        /// <summary>
        /// 获取在线玩家
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool TryGetOnlinePlayer(string playerId, out Player player)
        {
            int spid = 0;
            Int32.TryParse(playerId, out spid);
            if (spid == 0)
                Glob.playerMgr.onlinePlayerList.TryGetValue(GetPlayerId(playerId), out player);
            else
                player = Glob.playerMgr.GetOnlinePlayer(spid);

            if (player == null)
                return false;
            return true;
        }


        /// <summary>
        /// 获取玩家ObjectId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        private static ObjectId GetPlayerId(string playerId)
        {
            ObjectId oid = ObjectId.Empty;
            int spid = 0;
            Int32.TryParse(playerId, out spid);
            if (spid != 0)
            {
                oid = TPlayer.ToObjectId(spid);
            }
            else
            {
                oid = new ObjectId(playerId);
            }
            return oid;
        }

        /// <summary>
        /// 控制台帮助输出
        /// </summary>
        private static void showHelp()
        {
            string str = @"控制台命令帮助[命领名 参数1 参数2 参数3 ],参数""[]""里表示默认值:
-exit             退出程序
-online             在线人数
-additem          增加物品 参数:1玩家id 2物品id 3数量[1]
-addmoney         增加金钱 参数:1玩家id 2数量[10000]
-addticket        增加钻石 参数:1玩家id 2数量[10000]
";
            Logger.Log(str);
        }
    
    }
}
