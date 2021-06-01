using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Bson;
using System.Linq;
using Google.Protobuf;
using System.Configuration;
using CommonLib;
using GameServer.Module;
using CommonLib.Comm.DBMgr;

namespace GameServer
{
    /// <summary>
    /// 控制台命令解释器
    /// </summary>
    public partial class ServerCommand
    {      
        public static (bool,string) Command(string cmd)
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
                        opionInfo =  addItem(cmdS[1], (int)EItemSubTypeVirtual.Ticket, count);
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
                        //Glob.net.ServerNet.Dispose();

                        //Glob.cylogMgr.FlushAll();                      
                        //DBWrite.Instance.SaveAll();
                        //倒计时关闭
                        System.Timers.Timer exitTimer = new System.Timers.Timer(1000);//     
                        exitTimer.Elapsed += new System.Timers.ElapsedEventHandler(exit_ElapsedCheck);
                        exitTimer.Start();
                        break;
                    default:
                        opionInfo = (false, "未找到命令行:"+ cmdS[0]);
                        break;
                    //case "-reset"://重新启程序
                    //    //Logger.Sys("正在重启服务器...");
                    //    //System.Windows.Forms.Application.Restart(); //重新启动
                    //    //Environment.Exit(0);//关闭程序
                    //    //Thread.Sleep(1000);
                    //    break;
                    //case "-updateacc":
                    //    updateAccount();
                    //    break;
                   
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
                //if (!DBWrite.Instance.IsRuning())
                //{
                    ((System.Timers.Timer)sender).Stop();
                    Logger.Sys("服务器即将关闭..");
                    ////记录服务器关闭时间
                    //CookieMgr.Info.ServerStopTime = DateTime.Now;
                    //CookieMgr.Save();
                    Thread.Sleep(500);
                    Environment.Exit(0);//关闭程序
                //}
                //else
                //{
                //    DBWrite.Instance.SaveAll();
                //}
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
        private static bool TryGetOnlinePlayer(string playerId,out Player player)
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

        private static List<TAccount> list = new List<TAccount>();
        private static void insertTest()
        {
            //for (int i = 0; i < 1; i++)
            //{
            //    TLogTicket t = new TLogTicket(true);
            //    t.test1 = new Dictionary<int, int[]>() { { 1, new int[] { 11, 12 } }, { 2, new int[] { 21, 12 } }, { 3, null } };
            //    t.test2 = new Dictionary<string, int[]>() { { "a1", new int[] { 11, 12 } }, { "a2", new int[] { 21, 12 } }, { "a3", null } };
            //    t.test3 = new Dictionary<int, List<int>>() { { 1, new List<int> { 11, 12 } }, { 2, new List<int> { 21, 12 } }, { 3, null } };
            //    t.Insert();               
            //}
            
            // TAccount acc = new TAccount(true);
            // acc.Insert();
            //for (int i = 0; i < 10000; i++)
            //{
            //    TAccount acc = new TAccount();
            //    acc.platform = DateTime.Now;
            //    acc.platformId = i.ToString();
            //    acc.Insert();
            //    list.Add(acc);
            //}

            //List<int[]> list =  Glob.config.dicSlotZoneScore[102].items;
            //int b = Glob.config.playerInitConfig.initMoney;
            //Logger.LogError(Glob.config.testVConfig.langVal.Value);
        }

        private static void updateTest(bool isIm)
        {            
            //TAccount acc;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    acc = list[i];
            //    if(isIm)
            //        acc.ingot = 10000000 + i;
            //    else
            //        acc.ingot = 20000000 + i;
            //    acc.Update(isIm);
            //}
        }
        private static void deleteTest()
        {
            //TAccount acc;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    acc = list[i];
            //    acc.Delete();
            //}
        }


        /// <summary>
        /// 用时测试
        /// </summary>
        private static void usedTimeTest()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //---------------------------------------------------------------
            int num = 1000;

            //PbError.SC_error_code err = new PbError.SC_error_code();
            //err.Protocol = 999;
            //err.Msg = "xxdfsdf111";
            //byte[] bytes = Glob.net.ServerNet.GetMsgBody(err);
            //PbError.SC_error_code err2 = new PbError.SC_error_code();
            //err2.MergeFrom(bytes);
            //Logger.Log(err2.Protocol);
            //Logger.Log(err2.Msg);

            Player player = Glob.playerMgr.onlinePlayerList.Values.ElementAt(0);

            // Glob.sysShareMgr.Share(player,ESysShareType.LevelUp, 3);
            //Glob.sysShareMgr.Share(player, ESysShareType.LevelUp, 5);
            // Glob.sysShareMgr.Share(player, ESysShareType.LevelUp, 10);
            //Glob.sysShareMgr.Share(player, ESysShareType.HorLvUp, 1,0, "小马".ToQualityColorStr(1));
            //Glob.sysShareMgr.Share(player, ESysShareType.GuessWinMult, 299);

            //Glob.sysShareMgr.ShareGuideResult(1, 2.3, 46, 33.2);


            //TChat chat = new TChat(true);
            //chat.type = (int)EChatType.World;
            //chat.sId = player.ID;
            ////(公会邀请)&公会名|公会SID
            //chat.cont = Glob.chatMgr.CreateParseContent(EChatParseType.ClubInvite, player.Club.SID, player.Club.Data.name);
            //chat.isParse = true;
            //chat.sTime = DateTime.Now;
            ////chat不需要存库
            //ChatMsg cmsg = Glob.chatMgr.CreateChatMsg(chat);
            //cmsg.Send();

            for (int i = 0; i < num; i++)
            {
                // int[] list = Glob.shopMgr.GenerateMarketShopIds(100);
                //string a = "";
                // foreach (int i in list)
                //     a = a + i + ",";
                //Logger.LogWarning(a);

                //List<THero> sortZS = (from a in list where a.job == 3 orderby a.FC descending select a).ToList();
            }
            //---------------------------------------------------------------
            watch.Stop();
            Logger.LogWarning($"执行了{num}次，用时:{watch.ElapsedMilliseconds}毫秒  {watch.Elapsed}");
        }

        //private static void updateAccount()
        //{
        //    TAccount acc;
        //    Dictionary<int, int> lvCount = new Dictionary<int, int>();
        //    for (int i = 1; i < 100; i++)
        //        lvCount.Add(i, 0);
        //    foreach (TPlayer play in Glob.tPlayerMgr.playerDataList.Values)
        //    {                
        //        if (Glob.tAccountMgr.accountList.TryGetValue(play.id, out acc))
        //        {
        //            acc.pfType = 0;
        //            acc.pfId = "t" + play.level + "_" + (++lvCount[play.level]);
        //            acc.Update();
        //        }               
        //    }
        //    Logger.Sys("账号重命名操作完成!!");
        //}


    }
}
