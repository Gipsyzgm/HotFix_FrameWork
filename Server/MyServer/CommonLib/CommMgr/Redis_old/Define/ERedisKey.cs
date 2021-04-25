using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Redis
{
    /// <summary>
    /// Redis键名枚举
    /// </summary>
    public class ERedisKey
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public static string PlayerInfo = "PlayerInfo";
        /// <summary>
        /// 竞技排行
        /// </summary>
        public static string ArenaRank = "ArenaRank";
        /// <summary>
        /// 联赛排行
        /// </summary>
        public static string LeagueRank = "LeagueRank";
        /// <summary>
        /// 联盟排行
        /// </summary>
        public static string ClubRank = "ClubRank";
        /// <summary>
        /// 活动排行
        /// </summary>
        public static string EventFBRank = "EventFBRank";
    }
}
