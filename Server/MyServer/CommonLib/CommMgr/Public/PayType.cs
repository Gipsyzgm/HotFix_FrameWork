using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Comm
{
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum PayType
    {
        /// <summary>普通充值</summary>
        Normal = 0,
        /// <summary>月卡充值</summary>
        MonthCard = 1,
        /// <summary>礼包</summary>
        Gift = 2,
        /// <summary>基金</summary>
        Fund = 3,
    }

}
