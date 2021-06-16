using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbBonus;
using MongoDB.Driver;
using PbCom;
using CommonLib.Comm.DBMgr;

namespace GameServer.Module
{
    /// <summary>
    /// 福利逻辑管理
    /// </summary>
    public class BonusMgr
    {
        /// <summary>宝藏开启状态</summary>
        public bool TreasureState = false;

        public BonusMgr()
        {
            TreasureState = IsTreasureOpen;
        }

        /// <summary>
        /// 检测宝藏是否开启
        /// </summary>
        public bool IsTreasureOpen
        {
            get
            {
//                 if (DateTime.Now >= Glob.config.bonusSettingsConfig.TreasureStartTime &&
//                     DateTime.Now < Glob.config.bonusSettingsConfig.TreasureEndTime)
//                     return true;
                return false;
            }
        }

        /// <summary>
        /// 宝藏结束时间戳
        /// </summary>
        public int TreasureEndTime
        {
            get
            {
                //                 if (!IsTreasureOpen)
                //                     return 0;
                //                 return Glob.config.bonusSettingsConfig.TreasureEndTime.ToTimestamp() - DateTime.Now.ToTimestamp();
                return 0;
            }
        }

        /// <summary>
        /// 检测宝藏开启状态
        /// </summary>
        public void CheckTreasureState()
        {
            bool state = IsTreasureOpen;
            if(state != TreasureState)
            {
                TreasureState = state;
                SC_treasure_state msg = new SC_treasure_state();
                msg.IsTreasureOpen = state;
                foreach (var p in Glob.playerMgr.onlinePlayerList.Values)
                    p.Send(msg);
            }
        }

        /// <summary>
        /// 玩家创建福利数据
        /// </summary>
        /// <param name="player"></param>
        public void PlayerCreateBonusData(Player player)
        {
            if (player.bonusData != null)
                return;
            TBonus data = new TBonus(player.ID);      
            data.levelIds = new int[] { };    
            data.signInAwards = new List<int>();       
            data.Insert();
            player.bonusData = data;
        }

        /// <summary>
        /// 发送福利信息
        /// </summary>
        public void SendBonusInfo(Player player)
        {
            SC_bonus_info msg = new SC_bonus_info();
            
         
            if (player.bonusData.levelIds != null)
                msg.LevelIds.Add(player.bonusData.levelIds);        
            msg.SigInDays = player.bonusData.signInAwards.Count;        
            player.Session.Send(msg);
            player.sendBoxChange();
        }

        /// <summary>
        /// 发送新手任务信息
        /// </summary>
        public void SendTashNewbieInfo(Player player)
        {
//            SC_taskNewbie_list msg = new SC_taskNewbie_list();
//            msg.IsNewbieComplete = player.bonusData == null ? false : player.bonusData.newbieOver;
////             foreach (var task in player.taskNewbieList.Values)
////             {
////                 msg.TaskNewbies.Add(task.GetTaskMsg());
////             }
//            player.Send(msg);
        }


        /// <summary>
        /// 玩家每日签到
        /// </summary>
        /// <param name="player"></param>
        public void SignInDaily(Player player)
        {
           
        }

        /// <summary>
        /// 每日重置在线玩家数据
        /// </summary>
        /// <param name="player"></param>
        public void ResetEverdayData(Player player)
        {
            player.AccountData.keepLoginNum += 1;
            player.AccountData.lastLoginDate = DateTime.Now;
            player.lastGetOnlineAwardTime = DateTime.Now;
            //player.Data.singInNum = GetSingInNum(player.AccountData.keepLoginNum);
            player.Data.onlineAwardId = 0;          
            player.Data.onlineAwardTime = 0;
      

            //在线的跨天签到
            SignInDaily(player);

            //在线的跨天更新任务列表
           // player.CheckTaskNewbie(true);
        }
        
        /// <summary>
        /// 获取每日签到抽奖次数
        /// </summary>
        /// <param name="keepLoginDay">连接登录天数</param>
        //public int GetSingInNum(int keepLoginDay)
        //{
        //    if (keepLoginDay > 6)
        //        return 7;
        //    return keepLoginDay + 1;
        //}

        /// <summary>
        /// 在线时间算到上次领取时间内(登录时算进去)
        /// </summary>
        /// <param name="player"></param>
        public void OnlineAwardTimeShift(Player player)
        {
//             if (player.Data.onlineAwardId == Glob.config.dicOnlineAward.Count)
//             {
//                 player.Data.onlineAwardTime = 0;
//             }
//             else if (player.Data.onlineAwardTime > 0)
//             {
//                 player.lastGetOnlineAwardTime = DateTime.Now.AddSeconds(-player.Data.onlineAwardTime);
//                 player.Data.onlineAwardTime = 0;
//             }
//             else
//             {
//                 player.lastGetOnlineAwardTime = DateTime.Now;
//                 player.Data.onlineAwardTime = 0;
//             }
        }
        /// <summary>
        /// 在线时间算记录到玩家数据(下线时算进去)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public void  OnlineAwardTimePut(Player player)
        {
//             if (player.Data.onlineAwardId == Glob.config.dicOnlineAward.Count)
//                 player.Data.onlineAwardTime = 0;
//             else
//                 player.Data.onlineAwardTime = (int)((DateTime.Now - player.lastGetOnlineAwardTime).TotalSeconds);
        }

        /// <summary>
        /// 获取下次领取在线奖励的时间截
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
//         public int GetNextOnlineAwardTime(Player player)
//         {
//             if (player.Data.onlineAwardId == Glob.config.dicOnlineAward.Count)
//                 return 0;
//             OnlineAwardConfig config;
//             if (Glob.config.dicOnlineAward.TryGetValue(player.Data.onlineAwardId + 1, out config))
//             {
//                 DateTime now = DateTime.Now;
//                 int onlineTime = (int)((now - player.lastGetOnlineAwardTime).TotalSeconds);
//                 if (onlineTime > config.waitTime)
//                     return now.ToTimestamp()-1;
//                 return now.AddSeconds(config.waitTime - onlineTime).ToTimestamp();
//             }
//             return 0;
//         }



        /// <summary>
        /// 购买开服基金
        /// </summary>
        /// <param name="player"></param>
        public void BuyOpenFund(Player player)
        {
            if (player.payData == null)
            {
                player.payData = new TPay(player.ID);
                player.payData.openFundBuy = true;
                player.payData.Insert();
            }
            else
            {
                player.payData.openFundBuy = true;
                player.payData.Update();
            }         
        }

        /// <summary>
        /// 是否购买了开服基金
        /// </summary>
        public bool IsBuyOpenFund(Player player)
        {
            return player.payData != null && player.payData.openFundBuy;
        }

        /// <summary>
        /// 升级时 给等级奖励
        /// </summary>
        /// <param name="player"></param>
        public void GetLevelAward(Player player)
        {
         
        }

        /// <summary>
        /// 开启挂机奖励或领取完挂机奖励时重新计算奖励id
        /// </summary>
        /// <param 
        /// ></param>
        public void UpdateHangAwards(Player player, bool isSend = true)
        {
        }
    }
}
