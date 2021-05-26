using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CenterServer.XGame
{

    public class TimerMgr
    {
        private Timer minuteTimer = null;
        private Timer SecondTimer = null;
        public TimerMgr()
        {
            minuteTimer = new Timer(1000);//每1分钟执行一次
            minuteTimer.Elapsed += new ElapsedEventHandler(minuteTimer_Elapsed);
            int delayMS = 60000 - DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            delayStartTimer(minuteTimer, delayMS, () => { minuteTimer_Elapsed(null, null); });
            minuteTimer_Elapsed(null, null);

            SecondTimer = new Timer(1000);//1秒判断一次
            SecondTimer.Elapsed += new ElapsedEventHandler(SecondTimer_Elapsed);
            SecondTimer.Start();
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

            }
            catch (Exception ex) { Logger.LogError(ex); }
        }


        /// <summary>
        /// 每秒钟定时器
        /// </summary>
        int m_LastRunS = -1;
        private void SecondTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                if (m_LastRunS == dtNow.Second) return;
                m_LastRunS = dtNow.Second;             
            }
            catch (Exception ex) { Logger.LogError(ex); };
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

        /// <summary>
        /// 结束定时器 在重启
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <param name="timer"></param>
        /// <param name="action"></param>
        void delayStartTimerEvent(object source, ElapsedEventArgs e, Timer timer, Action action = null)
        {
            ((Timer)source).Stop();
            if (action != null)
                action();
            timer.Start();
        }
    }
}
