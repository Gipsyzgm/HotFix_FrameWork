
using PbSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace HotFix
{
    public class TimeMgr
    {
        /// <summary>
        /// 服务器时间戳
        /// </summary>
        private double _serverTimestamp = 0;
        /// <summary>
        /// 服务器时间戳
        /// </summary>
        public int ServerTimestamp => (int)_serverTimestamp;

        /// <summary>
        /// 服务器时间
        /// </summary>
        public DateTime ServerTime => ((int)_serverTimestamp).ToDateTime();

        protected int timerRunId = 0;

        //最后一次更新服务时间的时间戳(用本地时间,切出去服务器时间可能没有走)
        public int LastUpdateServerTimestamp = 0;

        public int ServerStartTime = 0;
        public TimeMgr()
        {
            _serverTimestamp = DateTime.Now.ToTimestamp();
        }

        private float heartbeatCount = 0;

        CS_sys_heartbeat msg = new CS_sys_heartbeat();
        private void serverTimer_Elapsed()
        {    
            _serverTimestamp += 0.2f;         
            heartbeatCount += 0.2f;
            if (heartbeatCount > 30)//30秒发送一次心跳包
            {
                heartbeatCount = 0;
                HotMgr.Net.Send(msg);
            }           
        }

        /// <summary>
        /// 获取服务器时间所在的周天(1-7)
        /// </summary>
        /// <returns></returns>
        public int GetWeek()
        {
            DayOfWeek week = ServerTime.DayOfWeek;
            if (week == DayOfWeek.Sunday)
                return 7;
            return (int)week;
        }

        /// <summary>
        /// 设置服务器时间
        /// </summary>
        /// <param name="timestamp"></param>
        public void UpdateServerTimestamp(int timestamp)
        {
            _serverTimestamp = timestamp;
            if (timerRunId == 0)
            {
                timerRunId = HotMgr.Timer.Loop(0.2f, serverTimer_Elapsed);
            }
        }

        /// <summary>
        /// 获取当天剩余秒数
        /// </summary>
        /// <returns></returns>
        public int GetDayTimeLeftSec()
        {
            int s = 60 - ServerTime.Second;
            s += (59 - ServerTime.Minute) * 60;
            s += (23 - ServerTime.Hour) * 3600;
            return s;
        }

        public void Dispose()
        {
            heartbeatCount = 0;
            timerRunId = 0;
        }
    }
}