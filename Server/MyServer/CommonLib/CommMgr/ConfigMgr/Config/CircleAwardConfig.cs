using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>转盘奖励</summary>
    public class CircleAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 转盘id
        /// （id>1000 为累计宝箱）
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public List<int[]> items { get; set; }
        /// <summary>
        /// 开启所需转盘次数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 转盘权重值
        /// </summary>
        public int weight { get; set; }
    }
}
