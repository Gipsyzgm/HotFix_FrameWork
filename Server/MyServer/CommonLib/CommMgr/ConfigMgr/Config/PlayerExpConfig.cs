using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>玩家升级经验</summary>
    public class PlayerExpConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => level;
        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 升到下级所需经验
        /// (最后一级填0)
        /// </summary>
        public int exp { get; set; }
        /// <summary>
        /// 天赋点数
        /// </summary>
        public int DowerPoint { get; set; }
        /// <summary>
        /// 随机天赋数组
        /// 1攻击力加成
        /// 2暴击几率加成
        /// 3暴击效果加成
        /// 4旋转范围
        /// 5旋转速度
        /// 6减伤
        /// 7生命值
        /// 8回血效果
        /// 9挂机奖励
        /// </summary>
        public int[] Index { get; set; }
    }
}
