using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>充值商品配置</summary>
    public class PayGoodsConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 商品唯一Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 商品名称
        /// 
        /// (首充奖励在[系统设置],只可领取一次,以下所有商品都算达成条件)
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 商品类型
        /// 0普通充值
        /// 1月卡
        /// 2礼包
        /// 3基金
        /// 4英勇卡
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 配置Id
        /// </summary>
        public int configId { get; set; }
        /// <summary>
        /// 购买价格(美分)
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// GooglePay商品Id
        /// </summary>
        public string gpId { get; set; }
        /// <summary>
        /// iOS商品Id
        /// </summary>
        public string iosId { get; set; }
    }
}
