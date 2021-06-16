using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class BonusSetting
    {
        /// <summary>
        /// 邀请间隔时间(秒) 默认300 5分钟
        /// </summary>
        public const int InviteInterval = 300;
				
        /// <summary>
        ///  每天邀请次数
        /// </summary>
		public const int InviteDayNum = 3;

        /// <summary>
        /// 邀请奖励钻石
        /// </summary>
        public const int InviteAwardTicket = 20;        
    }
}
