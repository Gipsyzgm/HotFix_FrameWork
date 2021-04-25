using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>礼包配置</summary>
    public class PayPackConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 礼包ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 礼包名称
        /// </summary>
        public Lang Name { get; set; }
        /// <summary>
        /// 礼包图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// 显示优先级1，2，3，4，5，6，7，8，9，10，11，12，13，14
        /// 礼包显示类型必须为3
        /// 
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// 礼包分类
        /// 设置同一类只显示一个
        /// 
        /// </summary>
        public int PackGroup { get; set; }
        /// <summary>
        /// 是否为统一触发条件
        /// 组合礼包
        /// </summary>
        public int IsGroup { get; set; }
        /// <summary>
        /// 组合礼包IDs
        /// </summary>
        public int[] GroupIds { get; set; }
        /// <summary>
        /// 组合礼包是否可以购买多个
        /// 1只能购买1个，。。0无限制
        /// </summary>
        public int GroupBuy { get; set; }
        /// <summary>
        ///  礼包显示类型
        /// 1:固定礼包 判断时间、条件 一直显示
        /// 2:全服礼包 只判断时间  一直显示
        /// 3:推送礼包 只判断条件 只显示3个
        /// 4:常驻礼包 只判断条件 一直显示  
        /// 
        /// 1 达到条件后，规定时间内一直显示
        /// 2 规定时间内一直显示
        /// 3 达到条件，按优先级最多显示3个
        /// 4 无持续时间限制
        /// </summary>
        public int GiftType { get; set; }
        /// <summary>
        /// 礼包触发类型
        /// EGiftType
        /// </summary>
        public int EGiftType { get; set; }
        /// <summary>
        /// 触发显示条件参数0
        /// 触发显示条件参数1
        /// 触发显示条件参数2
        /// 触发显示条件参数3
        /// </summary>
        public int[] trigger { get; set; }
        /// <summary>
        /// 礼包内容
        /// </summary>
        public List<int[]> Items { get; set; }
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
        /// 持续时间 小时
        /// 
        /// 填0无持续时间限制
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public int Close { get; set; }
        /// <summary>
        /// 限购次数
        /// 0 无限制
        /// </summary>
        public int GetNum { get; set; }
    }
}
