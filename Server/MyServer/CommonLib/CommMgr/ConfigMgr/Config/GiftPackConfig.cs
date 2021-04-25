using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>金券购买礼包</summary>
    public class GiftPackConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 商品Id
        /// PayGood Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 物品ID1_数量;物品ID2_数量 
        /// 1 金币
        /// 2 金券(钻石)
        /// 3 奖牌
        /// 4 彩券
        /// 5 经验
        /// 6 幸运硬币
        /// 7 珍珠
        /// 8 钻石
        /// 9 魔力
        /// 10天赋水晶
        /// 11赛马积分
        /// 12联赛点数
        /// 13VIP经验
        /// 
        /// </summary>
        public List<int[]> items { get; set; }
        /// <summary>
        /// 随机装备
        /// (id+num+权重)
        /// </summary>
        public List<int[]> equips { get; set; }
    }
}
