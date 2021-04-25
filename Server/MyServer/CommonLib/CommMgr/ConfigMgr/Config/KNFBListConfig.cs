using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>副本列表</summary>
    public class KNFBListConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 章节Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 血量系数
        /// </summary>
        public float hpmodulus { get; set; }
        /// <summary>
        /// 关卡列表
        /// </summary>
        public List<int[]> list { get; set; }
    }
}
