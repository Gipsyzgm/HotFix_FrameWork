using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>商城配置</summary>
    public class StoreConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        ///  商品Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 商品栏位
        /// 0道具
        /// 1礼包
        /// </summary>
        public int place { get; set; }
        /// <summary>
        /// 显示排序
        /// 数值越小越靠前
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 物品Id
        /// </summary>
        public int[] itemId { get; set; }
        /// <summary>
        /// 单次购买数量
        /// </summary>
        public int[] itemNum { get; set; }
        /// <summary>
        /// 价格类型
        /// 0广告
        /// 1美元
        /// 2金券
        /// 
        /// 
        /// </summary>
        public int priceType { get; set; }
        /// <summary>
        /// 购买次数重置类型
        /// 0活动开始重置(没活动不重置)
        /// 1 每日重置
        /// </summary>
        public int restType { get; set; }
        /// <summary>
        /// 单次购买价格
        /// （广告类型为广告次数）
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 每日次数上限
        /// （-1为没有次数限制）
        /// </summary>
        public int max { get; set; }
        /// <summary>
        /// 原价
        /// 仅用于显示
        /// </summary>
        public int originalPrice { get; set; }
        /// <summary>
        /// 折扣
        /// 仅用于显示
        /// </summary>
        public int discount { get; set; }
    }
}
