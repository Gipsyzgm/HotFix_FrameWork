/// <summary>
/// 建筑产出配置接口
/// </summary>

using System.Collections.Generic;

namespace CommonLib.Comm
{
    public interface IBuildOutput
    {
        /// <summary>
        /// 生产项Id
        /// </summary>
        int id { get; set; }
        /// <summary>
        /// 生产项说明
        /// </summary>
        Lang name { get; set; }
        /// <summary>
        /// 需要
        /// 训练场等级
        /// </summary>
        int level { get; set; }
        /// <summary>
        /// 生产物
        /// 对应英雄Ids
        /// 权重获得(必得一种)
        /// 英雄Id_权重值
        /// </summary>
        List<int[]> heroIds { get; set; }
        /// <summary>
        /// 单个生产时间
        /// </summary>
        int time { get; set; }
        /// <summary>
        /// 生产单个英雄
        /// 消耗资源
        /// </summary>
        List<int[]> cost { get; set; }
        /// <summary>
        /// 生产英雄
        /// 消耗道具
        /// </summary>
        List<int[]> costItems { get; set; }
        /// <summary>
        /// 研究获得
        /// 生产物的数量
        /// </summary>
        int num { get; set; }
        /// <summary>
        /// 开启
        /// 所需时间
        /// </summary>
        int openTime { get; set; }
        /// <summary>
        /// 开启时需要
        /// 消耗材料物资
        /// 多个;分隔
        /// </summary>
        List<int[]> openCostItems { get; set; }
    }
}
