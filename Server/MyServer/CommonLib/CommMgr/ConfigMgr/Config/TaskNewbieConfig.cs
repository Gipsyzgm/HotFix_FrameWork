using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>新手任务</summary>
    public class TaskNewbieConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 新手任务Id
        /// 第几天*100+档次id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 第几天
        /// </summary>
        public int day { get; set; }
        /// <summary>
        /// 档次id
        /// </summary>
        public int levelId { get; set; }
        /// <summary>
        /// 任务名
        /// 语言表ID
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 任务完成条件说明
        /// 语言表ID
        /// </summary>
        public Lang des { get; set; }
        /// <summary>
        /// 任务完成类型
        /// ETaskType
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 完成条件参数1
        /// 完成条件参数2
        /// 完成条件参数3
        /// </summary>
        public int[] condition { get; set; }
        /// <summary>
        /// 任务奖励
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
        public List<int[]> award { get; set; }
    }
}
