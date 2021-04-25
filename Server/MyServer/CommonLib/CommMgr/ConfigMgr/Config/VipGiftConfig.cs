using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>VIP礼包</summary>
    public class VipGiftConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => level;
        /// <summary>
        /// 奖励Id
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 购买价格(钻石)
        /// </summary>
        public int ticket { get; set; }
        /// <summary>
        /// 物品ID1_数量;物品ID2_数量 
        /// 1 金币
        /// 2 点券
        /// 3 奖牌
        /// 4 彩券
        /// 5 经验
        /// 6 幸运硬币
        /// 7 珍珠
        /// 8 钻石
        /// 9 魔力
        /// 10天赋水晶
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
