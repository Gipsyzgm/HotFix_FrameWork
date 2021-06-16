using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 公用奖励状态
    /// </summary>
    public enum EAwardState
    {
        /// <summary>未完成</summary>
        Undone = 0,
        /// <summary>条件已达成未领取(可领取)</summary>
        Done = 1,
        /// <summary>已领取(或已结束)</summary>
        HaveGet = 2,
    }
}
