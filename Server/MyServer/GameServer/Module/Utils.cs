using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class Utils
    {
        /// <summary>
        /// 清除时间花费点券数
        /// </summary>
        /// <param name="sec">秒</param>
        /// <returns></returns>
        public static int ClearTimeTicket(int sec)
        {
            sec = sec - 2;//2秒容错
            if (sec > 0)
                return (int)Math.Ceiling(sec / 60f) * Glob.config.settingConfig.ClearCDTicket;
            return 0;
        }

        /// <summary>
        /// 清除时间花费钻石数(1钻/分钟)
        /// </summary>
        public static int ClearTimeTicket(DateTime? endTime)
        {
            if (endTime == null)
                return 0;
            int sec = (int)((DateTime)endTime - DateTime.Now).TotalSeconds;
            return ClearTimeTicket(sec);
        }

        /// <summary>
        /// 跟据排名获取星级
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static int GetRankStar(int rank)
        {
            if (rank <= 3)
                return 4 - rank;
            return 0;
        }

        /// <summary>
        /// 判断是否在时间内
        /// </summary>
        /// <param name="now">当前时间 DateTime.Now.Month * 100 + DateTime.Now.Day</param>
        /// <param name="strTime">开始时间1001</param>
        /// <param name="endTime">结束时间1201</param>
        /// <returns></returns>
        public static bool CheckInDate(int now ,int strTime,int endTime)
        {
            if (strTime == 0 || endTime == 0)
                return true;
            if (endTime < strTime) //跨年了
                return !(now > endTime && now < strTime);
            return now >= strTime && now <= endTime;
        }

        /// <summary>
        /// 判断是否超过当前时间
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static bool CheckIsDate(int strTime)
        {
            string str = DateTime.Now.Date.ToString("MMdd");          
            int.TryParse(str, out int date);
            if (date > strTime)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断是否超过当前时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool CheckIsNow(DateTime date)
        {
            return date.ToTimestamp() <= DateTime.Now.ToTimestamp();
        }
    }
}