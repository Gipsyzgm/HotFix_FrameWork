using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>赛季宝箱</summary>
    public class SeasonAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 宝箱id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 普通奖励
        /// </summary>
        public List<int[]> NorItems { get; set; }
        /// <summary>
        /// 开启令牌奖励
        /// </summary>
        public List<int[]> items { get; set; }
        /// <summary>
        /// 所需令牌点数
        /// </summary>
        public int Point { get; set; }
    }
}
