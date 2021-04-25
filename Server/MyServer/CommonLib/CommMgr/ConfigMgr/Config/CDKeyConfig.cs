using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>CDKey配置</summary>
    public class CDKeyConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// CDKey编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 玩家可使用同编号CDKey数量
        /// 
        /// </summary>
        public int useCount { get; set; }
        /// <summary>
        /// CDKey名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 平台ID
        /// (0无平台限制)
        /// </summary>
        public int pfId { get; set; }
        /// <summary>
        /// CDKey数量
        /// Max:999999
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 过期时间
        /// 不填永久有效
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 获得物品
        /// 物品ID1_数量;物品ID2_数量 
        /// 1 金币
        /// 2 钻石(点券)
        /// 3 经验
        /// 4 英雄经验(功勋)
        /// 5 行动力
        /// 6 水元素
        /// 7 火元素
        /// 8 自然元素
        /// 9 光明元素
        /// 10 黑暗元素
        /// 11 角斗场积分
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
