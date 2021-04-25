using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>特权奖励配置</summary>
    public class VIPAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => level;
        /// <summary>
        /// vip等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 达成累计广告次数
        /// 
        /// </summary>
        public int ADNum { get; set; }
        /// <summary>
        /// 奖励内容
        /// 物品id_数量
        /// </summary>
        public List<int[]> items { get; set; }
        /// <summary>
        /// 特权功能
        /// (id 0-12)
        /// </summary>
        public int[] id { get; set; }
    }
}
