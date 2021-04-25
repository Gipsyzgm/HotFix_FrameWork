using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>新手任务宝箱</summary>
    public class NewbieAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 宝箱id
        /// 累计完成
        /// 新手任务总数量
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 累计完成任务进度奖励
        /// 物品ID1_数量;物品ID2_数量 
        /// 1钻石
        /// 2食物
        /// 3矿石
        /// 4人口
        /// 5经验
        /// 6副本体力
        /// 7竞技体力
        /// 8泰坦体力
        /// </summary>
        public List<int[]> items { get; set; }
    }
}
