using CenterServer;
using CommonLib;
using PbPlayer;
using System;
using System.Timers;

namespace GameServer
{
    /// <summary>
    /// 服务器定时器管理
    /// </summary>
    public class TimerMgr
    {
        private Timer minuteTimer;

        /// <summary>
        /// 每分钟执行
        /// </summary>
        public TimerMgr()
        {
            minuteTimer = new Timer(1000);//每1分钟执行一次
            minuteTimer.Elapsed += new ElapsedEventHandler(minuteTimer_Elapsed);
            int delayMS = 60000 - DateTime.Now.Second*1000 + DateTime.Now.Millisecond;
            delayStartTimer(minuteTimer, delayMS, () => { minuteTimer_Elapsed(null, null); });
            minuteTimer_Elapsed(null, null);
        
        }

        /// <summary>
        /// 获取服务器时间所在的周天(1-7)
        /// </summary>
        /// <returns></returns>
        public int GetWeek()
        {
            DayOfWeek week = DateTime.Now.DayOfWeek;
            if (week == DayOfWeek.Sunday)
                return 7;
            return (int)week;
        }

        /// <summary>
        /// 判断时间是否跨天了(与每日重置时间比)
        /// </summary>
        public bool CheckAcrossDay(DateTime time)
        {
            DateTime resetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return (resetTime - time).TotalMilliseconds > 0;
        }

        /// <summary>
        /// 判断时间是否跨天了(与每日重置时间比)
        /// 0同一天,1跨天一天,2跨了二天
        /// </summary>
        public int GetAcrossDay(DateTime time)
        {
            DateTime resetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            double days = (resetTime - time).TotalDays;
            if (days < 0)
                return 0;
            return (int)Math.Ceiling(days);
        }
        /// <summary>
        /// 延时开启定时器
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="ms"></param>
        void delayStartTimer(Timer timer, int ms, Action action = null)
        {
            Timer delayTimer = new Timer(ms);
            delayTimer.Elapsed += new ElapsedEventHandler((s, e) => delayStartTimerEvent(s, e, timer, action));
            delayTimer.Start();
        }              
        private static void delayStartTimerEvent(object source, ElapsedEventArgs e, Timer timer,Action action = null)
        {
            ((Timer)source).Stop();
            if (action != null)
                action();
            timer.Start();
           
        }

        
        int m_LastRunMM = -1;
        /// <summary>
        /// 每分钟定时器
        /// </summary>
        void minuteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                
                DateTime dtNow = DateTime.Now;
                if (m_LastRunMM == dtNow.Minute) return;
                m_LastRunMM = dtNow.Minute;

                string HHmm = dtNow.ToString("HHmm");
                string mm = dtNow.ToString("mm");

                try { AdminCmd.Command.LoadFile(); } catch (Exception ex) { Logger.LogError(ex); }

                //每分钟的竞猜检测
                if (HHmm == "0000") //每天00:00重置用户数据
                {
                   
                  
                }
                if(HHmm == "0001")
                {
                    Glob.activityMgr.CheckOpen();  // 每天检测活动是不应该开启和关闭
                    everydayResetData();
                }


                if (mm == "00"|| mm == "10" || mm == "20" || mm == "30" || mm == "40" || mm == "50")
                {
                    //统计12天留存
                    try { Glob.logMgr.UpdateSevenDay(); } catch (Exception ex) { Logger.LogError(ex); }

                    //重新统计服务器数据
                    try { Glob.logMgr.UpdateLogServer(dtNow); } catch (Exception ex) { Logger.LogError(ex); }

                    //检测宝藏开启状态
                    try { Glob.bonusMgr.CheckTreasureState(); } catch (Exception ex) { Logger.LogError(ex); }


                    try { GC.Collect(); } catch (Exception ex) { Logger.LogError(ex); }
                }
        
                try { GC.Collect(); } catch (Exception ex) { Logger.LogError(ex); }
            }
            catch (Exception ex) { Logger.LogError(ex); };
        }


        public void ResetDayCmd()
        {
            everydayResetData();
        }

        /// <summary>
        /// 每日00:00重置数据
        /// </summary>
        void everydayResetData()
        {
            ////重置英勇每日任务
            //try { Glob.heroicMgr.ResetDailyTask(); } catch (Exception ex) { Logger.LogError(ex); }

            //重置福利分享次数
            //try { Glob.bonusMgr.ResertBonusNum(); } catch (Exception ex) { Logger.LogError(ex); }         
            //重置好友赠送领取次数
            //try { Glob.friendMgr.ResertNum(); } catch (Exception ex) { Logger.LogError(ex); }

            ////重置每日活动
            //try { Glob.activityMgr.ResetDailyActiivty(); } catch (Exception ex) { Logger.LogError(ex); }

            //通知在线玩家，数据重置了
            SC_player_resetData data = new SC_player_resetData();         
            foreach (Player player in Glob.playerMgr.onlinePlayerList.Values)
            {
                //月卡天数减少一天,并重置月卡领取状态
                Glob.vipMgr.ResetEverdayData(player);
                //重置签到次数和在线奖励记录
                Glob.bonusMgr.ResetEverdayData(player);

                player.Data.adtimes = 0;
                data.KeepLoginNum = player.AccountData.keepLoginNum;
                data.RegDay = Glob.timerMgr.GetAcrossDay((DateTime)player.AccountData.regDate) + 1;
                data.LeftAdNum = Glob.config.settingConfig.MaxADTimes;
                data.BuyPowerNum = 0;
                if (player.payData != null)
                {
                    data.MCDay = player.payData.MC;
                    data.IsGetMC = player.payData.isGetMC;
                }     
                player.Send(data);           
                //重新发送福利消息
                Glob.bonusMgr.SendBonusInfo(player);
                //重新发送商城广告物品
                Glob.storeMgr.RefreshADitems(player);

                //重置每日任务
                try { Glob.taskMgr.ResetTaskDay(player); } catch (Exception ex) { Logger.LogError(ex); }
            }
        }
        /// <summary>
        /// 每日00:01刷新状态
        /// </summary>
        void RefreshSomeState()
        {
            //重置元素召唤Id
            // Glob.storeMgr.ChangeElementSummonIndex();
        }
    }
}
