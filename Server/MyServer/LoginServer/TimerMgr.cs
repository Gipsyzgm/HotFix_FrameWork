using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using CommonLib;

namespace LoginServer
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
            minuteTimer = new Timer(1000);//每1000毫秒分钟执行一次
            minuteTimer.Elapsed += new ElapsedEventHandler(minuteTimer_Elapsed);
            int delayMS = 60000 - DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            delayStartTimer(minuteTimer, delayMS, () => { minuteTimer_Elapsed(null, null); });
            minuteTimer_Elapsed(null, null);
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
        private static void delayStartTimerEvent(object source, ElapsedEventArgs e, Timer timer, Action action = null)
        {
            ((Timer)source).Stop();
            if (action != null)
                action();
            timer.Start();

        }

        /// <summary>
        /// 标记前一分钟
        /// </summary>
        int LastRunMM = -1;
        /// <summary>
        /// 每分钟定时器
        /// </summary>
        void minuteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                if (LastRunMM == dtNow.Minute) return;
                LastRunMM = dtNow.Minute;
                try
                {
                    //定时检查是否有配置的指令
                    AdminCmd.Command.LoadFile();

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
            catch (Exception ex) { Logger.LogError(ex); };
        }

    }
}
