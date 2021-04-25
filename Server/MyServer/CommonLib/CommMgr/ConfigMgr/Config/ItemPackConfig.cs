using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>道具礼包配置</summary>
    public class ItemPackConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 物品ID
        /// 与Item表可使用道具对应
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 礼包名
        /// 策划用，不导出
        /// </summary>
        public string n_name { get; set; }
        /// <summary>
        /// 固定获得物品(没有不填)
        /// 物品ID1_数量;物品ID2_数量 
        /// 1 金币
        /// </summary>
        public List<int[]> items { get; set; }
        /// <summary>
        /// 权重获得物品执行次数范围
        /// </summary>
        public int[] weightNum { get; set; }
        /// <summary>
        /// 权重获得物品(必得一种)
        /// 物品Id_数量_权重值
        /// (没有不填)
        /// </summary>
        public List<int[]> weightItems { get; set; }
        /// <summary>
        /// 随机获得物品
        /// 物品Id_数量_百分比几率值(1-100)
        /// (没有不填)
        /// </summary>
        public List<int[]> randomItems { get; set; }
    }
}
