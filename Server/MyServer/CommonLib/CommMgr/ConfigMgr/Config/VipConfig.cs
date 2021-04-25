using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>VIP配置</summary>
    public class VipConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => level;
        /// <summary>
        /// VIP等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 升到下级所需总经验
        /// 1点券=1经验
        /// </summary>
        public int exp { get; set; }
        /// <summary>
        /// 跳过比赛动画
        /// (0不过跳过)
        /// (1可以跳过)
        /// 骑师就餐次数
        /// 探索次数
        /// 基础治疗次数
        /// 广告次数
        /// 采矿次数
        /// 度假次数
        /// 俱乐部研究
        /// </summary>
        public int[] right { get; set; }
    }
}
