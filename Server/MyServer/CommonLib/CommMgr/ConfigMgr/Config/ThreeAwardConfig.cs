using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>三日登录奖励</summary>
    public class ThreeAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 第几天
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 登录奖励
        /// 物品ID1_数量;物品ID2_数量 
        /// 
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
