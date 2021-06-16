using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class MUtils
    {
        /// <summary>
        /// 清除时间花费钻石数
        /// </summary>
        /// <param name="sec">秒</param>
        /// <returns></returns>
        public static int ClearTimeTicket(int sec)
        {
            sec = sec - 2;//2秒容错
            if (sec>0)
                return (int)Math.Ceiling(sec / 60f) ;
            return 0;
        }
        /// <summary>
        /// 清除市场花费钻石(10分钟1钻)
        /// </summary>
        /// <param name="sec">秒</param>
        /// <returns></returns>
        public static int ClearShopTimeTicket(int sec)
        {
            sec = sec - 2;//2秒容错
            if (sec > 0)
                return (int)Math.Ceiling(sec / 600f) ;
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
    }
}
