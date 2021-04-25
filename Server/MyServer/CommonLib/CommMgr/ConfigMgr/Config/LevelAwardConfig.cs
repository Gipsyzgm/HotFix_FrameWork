using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>等级奖励</summary>
    public class LevelAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 达到等级
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 物品ID1_数量;物品ID2_数量 
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
