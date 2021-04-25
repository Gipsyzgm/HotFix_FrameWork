using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>基金</summary>
    public class FundConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 充值档位ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 商品Id
        /// PayGood Id
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 购买时赠送物品
        /// 1 钻石
        /// 2 食物
        /// 3 石头
        /// 4 人口
        /// 5 经验
        /// 6 副本行动点
        /// 7 竞技行动点
        /// 8 公会行动点
        /// 9 VIP经验
        /// 
        /// (奖励都按Id1 发)
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
