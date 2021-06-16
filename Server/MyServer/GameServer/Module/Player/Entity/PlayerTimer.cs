using MongoDB.Bson;
using PbWar;
using PbPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CommonLib.Comm.DBMgr;
using CommonLib;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家定时器
    /// </summary>
    public partial class Player
    {
        private Timer playerTimer;
        //开启角色定时器检测
        public void StartTimer()
        {
            if (playerTimer == null)
            {
                playerTimer = new Timer(1000);//5秒判断一次
                playerTimer.Elapsed += new ElapsedEventHandler(playerTimer_Elapsed);
                playerTimer.Start();
            }
            lastBeartbeatTime = DateTime.Now;
        }
        int m_LastRunMM = -1;
        private void playerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!IsOnline|| Data==null)
                {
                    playerTimer.Stop();
                    return;
                }
                DateTime now = DateTime.Now;
                if (m_LastRunMM != now.Minute)
                {
                    m_LastRunMM = now.Minute;
                 
                }

             
                //1小时恢复一次竞技行动点
                //                 if((now - (DateTime)Data.lastAddArenaPTime).TotalSeconds >= Glob.config.settingConfig.ArenaPointRecoverTime)
                //                 {
                //                     if (Data.arenaPoint < Glob.config.settingConfig.ArenaPointMax)
                //                     {
                //                         //Data.lastAddArenaPTime = ((DateTime)Data.lastAddArenaPTime).AddSeconds(Glob.config.settingConfig.ArenaPointRecoverTime);
                //                         Data.lastAddArenaPTime = DateTime.Now;
                //                         AddVirtualItemNum(EItemSubTypeVirtual.ArenaPoint, 1);
                //                         sendPointChange();
                //                     }
                //                 }

                //1小时恢复一次竞技行动点
                //                 if ((now - (DateTime)Data.lastAddBpTime).TotalSeconds >= Glob.config.settingConfig.BossPointRecoverTime)
                //                 {
                //                     if (Data.bossPoint < Glob.config.settingConfig.BossPointMax)
                //                     {
                //                         Data.lastAddBpTime = DateTime.Now;
                //                         AddVirtualItemNum(EItemSubTypeVirtual.BossPoint, 1);
                //                         sendPointChange();
                //                     }
                //                 }

                //1分钟获得一次挂机奖励
                //if ((now - (DateTime)Data.lastAddBLTime).TotalSeconds >= SystemConfig.BattleLevelAward_Interval)
                //{
                //    Data.lastAddBLTime = ((DateTime)Data.lastAddBLTime).AddSeconds(SystemConfig.BattleLevelAward_Interval);
                //    BattleLevelConfig _config;
                //    if (Glob.config.dicBattleLevel.TryGetValue(BattlelLevelValid, out _config))
                //    {
                //        SC_battleLv_onlineAward award = new SC_battleLv_onlineAward();
                //        award.Money = (int)(_config.lvMoney / 60f * SystemConfig.BattleLevelAward_Interval);
                //        award.HeroExp = (int)(_config.lvHeroExp / 60f * SystemConfig.BattleLevelAward_Interval);
                //        Session.Send(award);
                //        Data.money += award.Money;
                //        Data.heroExpPoint += award.HeroExp;
                //    }
                //    SaveData();
                //}
                //2小时还没收到心跳包，设置玩家为离线 (客户端30秒发一次)7200
                if ((now - lastBeartbeatTime).TotalSeconds > 600)
                {
                    Logger.LogWarning("心跳包超时,设置掉线");
                    //Session.Disconnect(SessionID);
                    playerTimer.Stop();
                }
                //战力发生改变，重新计算人物总战力
                //if (IsFCChange)
                //{
                //    CalculateFC(); 
                //    IsFCChange = false;
                //}
            }
            catch (Exception ex) { Logger.LogError(ex); };
        }

        /// <summary>
        /// 发送行动点数据改变
        /// </summary>
        public void sendPointChange()
        {
            SC_player_point msg = new SC_player_point();
      
            msg.NextAddPowerTime = ActionPointAddNextTime;

            Send(msg);
        }

        /// <summary>
        /// 发送荣耀点数据改变
        /// </summary>
        public void sendHornorChange()
        {
            SC_war_GetBoxUpdate msg = new SC_war_GetBoxUpdate();
            List<int> ids = new List<int>();
      
            if (ids.Count > 0)
            {
               // msg.Ids.Add(ids);
                Send(msg);
            }

        }

        /// <summary>
        /// 发送荣耀点数据改变
        /// </summary>
        public void sendBoxChange()
        {
            SC_war_GetBoxUpdate msg = new SC_war_GetBoxUpdate();

            int id = 0;
        
        }
        /// <summary>
        /// 下次恢复体力时间戳
        /// </summary>
        public int ActionPointAddNextTime
        {
            get
            {
                return 0;
            }

        }

        /// <summary>
        /// 下次恢复竞技行动点时间戳
        /// </summary>
//         public int ArenaPointAddNextTime
//         {
//             get
//             {
//                 if (Data.arenaPoint >= Glob.config.settingConfig.ArenaPointMax)
//                     return 0;
//                 else
//                     return ((DateTime)Data.lastAddArenaPTime).ToTimestamp() + Glob.config.settingConfig.ArenaPointRecoverTime;
//             }
//         }

        /// <summary>
        /// 下次恢复公会Boss行动点时间戳
        /// </summary>
//         public int BossPointAddNextTime
//         {
//             get
//             {
//                 if (Data.bossPoint >= Glob.config.settingConfig.BossPointMax)
//                     return 0;
//                 else
//                     return ((DateTime)Data.lastAddBpTime).ToTimestamp() + Glob.config.settingConfig.BossPointRecoverTime;
//             }
//         }

        /// <summary>
        /// 下次免费召唤时间戳
        /// </summary>
//         public int SummonFreeNextTime
//         {
//             get
//             {
//                 if (Data.lastSummonTime == null)
//                     Data.lastSummonTime = DateTime.Now.AddDays(-1).AddSeconds(-10);
//                 return ((DateTime)Data.lastSummonTime).AddSeconds(Glob.config.summonSettingConfig.SummonFreeTime).ToTimestamp();
//             }
//         }


        /// <summary>
        /// 发送天赋点数
        /// </summary>
        public void AddDowerPoint(int level,int startlevel)
        {

            SC_player_dowerPoint msg = new SC_player_dowerPoint();
            Send(msg);
        }

        //玩家发送未领取奖励
        public void SendLeftAward()
        {
            //TPlayerLeft left = MongoDBHelper.Instance.Select<TPlayerLeft>(Data.id);
            //if (left == null)
            //    return;

            //if(left.LevelItems != null && left.LevelItems.Count > 0)
            //{
            //    List<int[]> tmp = new List<int[]>();
            //    foreach (var n in left.LevelItems)
            //    {
            //        int[] a = new int[2];
            //        a[0] = n.Key;
            //        a[1] = n.Value;
            //        tmp.Add(a);
            //    }
            //    Glob.itemMgr.PlayerAddNewItems(this, tmp, true, PbCom.Enum_bag_itemsType.BiLevelAward);
            //    left.LevelItems.Clear();
            //    left.Update();
            //}

            //if(left.FbItems != null && left.FbItems.Count > 0)
            //{
            //    List<int[]> tmp = new List<int[]>();
            //    foreach (var n in left.FbItems)
            //    {
            //        int[] a = new int[2];
            //        a[0] = n.Key;
            //        a[1] = n.Value;
            //        tmp.Add(a);
            //    }
            //    Glob.itemMgr.PlayerAddNewItems(this, tmp, true, PbCom.Enum_bag_itemsType.BiFbdrop);
            //    left.FbItems.Clear();
            //    left.Update();
            //}

        }

    }
}
