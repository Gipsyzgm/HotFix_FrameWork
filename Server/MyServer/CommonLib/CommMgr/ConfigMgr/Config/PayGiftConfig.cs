using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>充值购买礼包</summary>
    public class PayGiftConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 档次ID
        /// (按注册日期天数)
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 商品Id
        /// PayGood Id
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 购买次数重置类型
        /// 0活动开始重置(没活动不重置)
        /// 1 每日重置
        /// </summary>
        public int restType { get; set; }
        /// <summary>
        /// 购买次数限制
        /// 0 无限制
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// (开始结束时间都填写才生效)
        /// 开始时间
        /// 月日
        /// 1001表示10月1号
        /// 00:00:00
        /// 
        /// 活动时间内的购买次数每次开启会清掉
        /// </summary>
        public int starTime { get; set; }
        /// <summary>
        /// (开始结束时间都填写才生效)
        /// 结束时间
        /// 月日
        /// 1002表示10月2号
        /// 23:59:59
        /// </summary>
        public int endTime { get; set; }
        /// <summary>
        /// 物品ID1_数量;物品ID2_数量 
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
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
