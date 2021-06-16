using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 活动类型
    /// </summary>
    public enum ActivityType
    {
        /// <summary>活动7天奖励</summary>
        SevenAward = 1,
        /// <summary>累计充值</summary>
        Recharge = 2,
        /// <summary>累计消费</summary>
        Consume = 3
    }

}
