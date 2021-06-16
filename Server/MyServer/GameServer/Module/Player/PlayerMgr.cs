using PbLogin;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using PbSystem;
using System.Diagnostics;
using GameServer.Net;
using PbCenterPlayer;
using System.Linq;
using CommonLib;
using CommonLib.Comm.DBMgr;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家实体管理
    /// </summary>
    public class PlayerMgr
    {
        /// <summary>
        /// 在线玩家列表
        /// </summary>
        public DictionarySafe<ObjectId, Player> onlinePlayerList = new DictionarySafe<ObjectId, Player>();

        /// <summary>
        /// 跟据SID获取在线玩家
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public Player GetOnlinePlayer(int sid)
        {
            Player player = null;
            ObjectId playId = TPlayer.ToObjectId(sid);
            if (playId != ObjectId.Empty)
            {
                onlinePlayerList.TryGetValue(playId,out player);
            }
            return player;
        }
        /// <summary>
        /// 跟据ID获取在线玩家
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Player GetOnlinePlayer(ObjectId id)
        {
            Player player = null;
            onlinePlayerList.TryGetValue(id, out player);
            return player;
        }

        /// <summary>
        /// 新玩家登录，并创建新玩家相关数据
        /// </summary>
        public void NewPlayerLogin(TAccount acc, string name, ClientToGameServerMessage session)
        {            
            if (Glob.tPlayerMgr.playerDataList.ContainsKey(acc.id))
                return;
            TPlayer tplayer =  Glob.tPlayerMgr.CreagePlayerData(acc, name);
            Player player = new Player(tplayer, acc);
            //创建初始装备
            Glob.itemMgr.PlayerCreateInitEpuip(player);
            Glob.bonusMgr.PlayerCreateBonusData(player);
            Glob.storeMgr.PlayerCreatStoreData(player);
            //执行登录流程
            loginGame(player,session,false);
      
        }
        /// <summary>
        /// 已有玩家登录，读取玩家数据
        /// </summary>
        /// <param name="acc"></param>
        public  void PlayerLogin(TAccount acc, ClientToGameServerMessage session,bool isReLogin = false)
        {
            Player oldPlay;
            if (onlinePlayerList.TryGetValue(acc.id, out oldPlay))
            {
                if (oldPlay.Session == session)
                {
                    Logger.LogWarning("一个连接，重复执行多次登录!!");
                    return;
                }
                else if (oldPlay.Data != null)  //有玩家内存数据直接使用内存数据
                {
                    if (oldPlay.IsOnline) //玩家在线，T掉玩家
                    {
                        oldPlay.IsOnline = false;
                        SC_sys_offline offline = new SC_sys_offline();
                        offline.Type = Enum_offline_type.OtOtherLogin;
                        oldPlay.Send(offline);

                    }
                    onlinePlayerList.Remove(oldPlay.ID); //从在线列表中移除，后面会加进去
                    loginGame(oldPlay, session, isReLogin);
                    return;
                }
            }
            TPlayer tplayer = DBReaderPlayer.Instance.ReadPlayerInfo(acc.id);
            if (tplayer == null)
            {
                Logger.LogWarning("没找到玩家数据");
                return;
            }

            //Glob.tPlayerMgr.playerDataList.AddOrUpdate(tplayer.id, tplayer);
            Player player = new Player(tplayer, acc);
            DBReaderPlayer.Instance.ReadPlayerData(player);
            //执行登录流程          
            loginGame(player, session, isReLogin);
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public void LogoutGame(Player player)
        {
            if (player.IsDispose || player.AccountData == null|| player.Data==null) return;
            //Glob.clubWarMgr.OfflineUnlockState(player);
            player.SendLeftAward();
           // player.PayPackPlayerData.upOnlineTime();//更新在线时长，分钟
            player.AccountData.lastLogoutDate = DateTime.Now; //最后离线时间
            
            Glob.bonusMgr.OnlineAwardTimePut(player);
            player.Data?.Update();
            player.AccountData?.Update();
            //Glob.arenaMgr.LoginOutAddArenaPoint(player);
            //Logger.LogError(player.Session.InClosing, player.Session.Connected);
            Player oldPlay;
            if (onlinePlayerList.TryGetValue(player.ID, out oldPlay))
            {
                if (oldPlay == player)
                    onlinePlayerList.Remove(player.ID);
            }
            //玩家退出,通知中央服务器同步
            CS_Center_PlayerLogout msg = new CS_Center_PlayerLogout();
            msg.PlayerId = player.ID.ToString();
            Glob.net.gameToCenterClient.SendMsg(msg);

         
            player.Dispose();
            player = null;

        }

        /// <summary>
        /// 玩家登录游戏
        /// </summary>
        /// <param name="player"></param>
        /// <param name="session"></param>
        private void loginGame(Player player, ClientToGameServerMessage session, bool isReLogin)
        {
            if (player.IsDispose)
                return;
            player.Session = session;
            player.IsOnline = true;
            //player.Data.lang = (int)session.Lang;
            //加在线玩家列表
            if (onlinePlayerList.ContainsKey(player.ID)) //已存在
            {
                Player oldPlayer = onlinePlayerList[player.ID];
                if (oldPlayer.IsOnline) //玩家在线，T掉玩家
                {
                    oldPlayer.IsOnline = false;
                    SC_sys_offline offline = new SC_sys_offline();
                    offline.Type = Enum_offline_type.OtOtherLogin;
                    oldPlayer.Send(offline);                 
                }
                onlinePlayerList[player.ID] = player;
            }
            else
            {
                onlinePlayerList.Add(player.ID, player);
            }
            

            //登录之前需要更新的数据
            loginUpdateDate(player);

            //发送玩家数据到客户端
            sendLoginPlayerInfo(player, isReLogin);



            if (!isReLogin)
            {

                //发送背包数据到客户端            
                Glob.itemMgr.SendPlayerBag(player);

                //发送VIP,月卡，充值信息
                Glob.vipMgr.SendVipInfo(player);
                //发送邮件信息
                Glob.mailMgr.SendMailList(player);
                //发送福利信息
                Glob.bonusMgr.SendBonusInfo(player);
                //新手任务信息
                Glob.bonusMgr.SendTashNewbieInfo(player);

                Glob.fbMgr.SendPlayerFB(player);
                //发送活动信息
                Glob.activityMgr.GetMsg(player);

                //发送红点数据
                Glob.redDotMgr.SendRedDotMsg(player);

                Glob.taskMgr.SendTaskInfo(player);
            }
            //玩家指引数据
            player.Guide = new GuideData(player);
            //============================
            //最后登录时间
            player.AccountData.lastLoginDate = DateTime.Now;
            player.AccountData.Update(false);



            //发送登录完成
            if (!isReLogin)
            {
                SC_login_enter enter = new SC_login_enter();
                enter.ServerTime = DateTime.Now.ToTimestamp();
                enter.ServerStartTime = Glob.ServerStartTime;
                enter.UTCOffset = DateTime.Now.GetUTCOffest();
                player.Send(enter);
            }
            else //发送重登数据
            {
                SC_login_reLogin remsg = new SC_login_reLogin();
                remsg.ServerTime = DateTime.Now.ToTimestamp();
                remsg.UTCOffset = DateTime.Now.GetUTCOffest();
                player.Send(remsg);
            }
            
            player.SaveData();
            //开启角色定时器
            player.StartTimer();         


            //充值未发货的订单操作补单
            Glob.payMgr.SendPayGoods(player);
            //未发送的推送消息
            GetNoSendPushMsg(player);
        }
        /// <summary>登录之前更新数据</summary>
        private void loginUpdateDate(Player player)
        {
            //指引任务
            // player.guideTask = new GuideTaskItem(player);
            Glob.hreoMgr.PlayerCheckHeroList(player);
            player.SendLeftAward();
            //登录签到

            DateTime now = DateTime.Now; 
            if (player.AccountData.lastLoginDate == null) //没有登录时间的处理
            {
                //player.Data.singInNum = Glob.bonusMgr.GetSingInNum(1);
                player.AccountData.keepLoginNum = 1;
                player.lastGetOnlineAwardTime = now;
                player.Data.contri = 1;
                //player.TriggerTask(ETaskType.Login_Times, 1);
                Glob.storeMgr.SendPlayerAdTimes(player);
                Glob.taskMgr.ResetTaskDay(player,false);
            }
            else
            {
                //登录时间跨天
                int acrossDay = Glob.timerMgr.GetAcrossDay((DateTime)player.AccountData.lastLoginDate);
                
                      
                Glob.storeMgr.SendPlayerAdTimes(player);
                //同天登录 逻辑处理
                if (acrossDay == 0)
                {
                    if (player.Data.contri == 0)
                        player.Data.contri = 1;
                    //在线时间算到上次领取时间内
                    Glob.bonusMgr.OnlineAwardTimeShift(player);
                    return;
                }
                //==============
                //跨天登录逻辑处理
                if (acrossDay == 1) //只跨了一天的逻辑处理
                {
                    player.AccountData.keepLoginNum += 1; //连续登录天数+1
                }
                else if (acrossDay > 1)//跨了2天以上的逻辑处理
                {
                    player.AccountData.keepLoginNum = 1;    //重置连续登录天数
                }

                if (acrossDay > 0)  //至少跨一天的逻辑处理
                {
                    //player.Data.horCureNum = 0;
                    player.Data.onlineAwardId = 0;          //当前已领取的在线奖励档位Id
                    player.Data.onlineAwardTime = 0;      //领取奖励在线时间(下线时记录)
          
                    Glob.vipMgr.LoginSetPlayerMC(player, acrossDay);    //登录游戏时减去月卡天数
                                                                        //Glob.busAdMgr.LoginSetPlayerLeftNum(player);

                    //player.Data.clubDonateNum = 0;
                    //player.Data.clubRsNum = 0;

                    //player.TriggerTask(ETaskType.Login_Times, 1);
                    Glob.bonusMgr.SignInDaily(player);

                    Glob.storeMgr.RefreshADitems(player);

                    Glob.taskMgr.ResetTaskDay(player,false);
                }
                player.lastGetOnlineAwardTime = now;
                //player.Data.singInNum = Glob.bonusMgr.GetSingInNum(player.AccountData.keepLoginNum);
                player.Data.contri += 1;
                

            }

        }
        

        /// <summary>
        /// 登录成功/或创角成功 发送角色信息到客户端
        /// </summary>
        private void sendLoginPlayerInfo(Player play, bool isReLogin)
        {
            SC_login_playerInfo playInfo = new SC_login_playerInfo()
            {
                SID = play.SID,
                Id = play.ID.ToString(),
                PfUserId = play.AccountData.pfId,
                Name = play.Data.name,
                Level = play.Data.level,
                Exp = play.Data.exp,
                Gold = play.Data.gold,
                Ticket = play.Data.ticket,
 
             

                NextAddAPTime = play.ActionPointAddNextTime,

                IsReLogin = isReLogin,
           
            };
        

            //连续登录天数
            playInfo.KeepLoginNum = play.AccountData.keepLoginNum;
            //注册天数(登录天数)
            playInfo.RegDay = Glob.timerMgr.GetAcrossDay((DateTime)play.AccountData.regDate) + 1;

            playInfo.CreateTime = play.AccountData.regDate.ToTimestamp();
            playInfo.PayMoney = play.AccountData.payMoney;
            playInfo.LoginDay = play.Data.contri;
            play.Send(playInfo);
        }
        /*
        //发送离线奖励
        private void sendCalculateOfflineAward(Player player)
        {
            DateTime now = DateTime.Now;

            //计算离线副本行动力
            if (player.Data.lastAddApTime == null)
                player.Data.lastAddApTime = now;
            else
            {
                if (player.Data.actionPoint < player.ActionPointMax)
                {
                    double offlineAPTime = (now - (DateTime)player.Data.lastAddApTime).TotalSeconds;
                    if (offlineAPTime > Glob.config.settingConfig.ActionPointRecoverTime)
                    {
                        int totalAP = (int)(offlineAPTime / Glob.config.settingConfig.ActionPointRecoverTime);
                        if (player.Data.actionPoint + totalAP >= player.ActionPointMax)
                        {
                            player.Data.actionPoint = player.ActionPointMax;
                            player.Data.lastAddApTime = now;
                        }
                        else
                        {
                            player.Data.actionPoint += totalAP;
                            player.Data.lastAddApTime = ((DateTime)player.Data.lastAddApTime).AddSeconds(
                                totalAP * Glob.config.settingConfig.ActionPointRecoverTime);
                        }
                    }
                }
            }

            //计算离线竞技行动力
            if (player.Data.lastAddArenaPTime == null)
                player.Data.lastAddArenaPTime = now;
            else
            {
                if (player.Data.arenaPoint < Glob.config.settingConfig.ArenaPointMax)
                {
                    double offlineArenaPTime = (now - (DateTime)player.Data.lastAddArenaPTime).TotalSeconds;
                    if (offlineArenaPTime > Glob.config.settingConfig.ArenaPointRecoverTime)
                    {
                        int totalAP = (int)(offlineArenaPTime / Glob.config.settingConfig.ArenaPointRecoverTime);
                        if (player.Data.arenaPoint + totalAP >= Glob.config.settingConfig.ArenaPointMax)
                        {
                            player.Data.arenaPoint = Glob.config.settingConfig.ArenaPointMax;
                            player.Data.lastAddArenaPTime = now;
                        }
                        else
                        {
                            player.Data.arenaPoint += totalAP;
                            player.Data.lastAddArenaPTime = ((DateTime)player.Data.lastAddArenaPTime).AddSeconds(
                                totalAP * Glob.config.settingConfig.ArenaPointRecoverTime);
                        }
                    }
                }
            }
            //计算离线boss行动点
            if (player.Data.lastAddBpTime == null)
                player.Data.lastAddBpTime = now;
            else
            {
                if (player.Data.bossPoint < Glob.config.settingConfig.BossPointMax)
                {
                    double offlineBossPTime = (now - (DateTime)player.Data.lastAddBpTime).TotalSeconds;
                    if (offlineBossPTime > Glob.config.settingConfig.BossPointRecoverTime)
                    {
                        int totalAP = (int)(offlineBossPTime / Glob.config.settingConfig.BossPointRecoverTime);
                        if (player.Data.bossPoint + totalAP >= Glob.config.settingConfig.BossPointMax)
                        {
                            player.Data.bossPoint = Glob.config.settingConfig.BossPointMax;
                            player.Data.lastAddBpTime = now;
                        }
                        else
                        {
                            player.Data.bossPoint += totalAP;
                            player.Data.lastAddBpTime = ((DateTime)player.Data.lastAddBpTime).AddSeconds(
                                totalAP * Glob.config.settingConfig.BossPointRecoverTime);
                        }
                    }
                }
            }
        }*/

        /// <summary>
        /// 登录时获取AIHelp未发送的通知
        /// </summary>
        /// <param name="player"></param>
        private void GetNoSendPushMsg(Player player)
        {
            CS_Center_AIHelpPush centerMsg = new CS_Center_AIHelpPush();
            centerMsg.PlayerId = player.ID.ToString();
            Glob.net.gameToCenterClient.SendMsg(centerMsg);
        }


    }
}
