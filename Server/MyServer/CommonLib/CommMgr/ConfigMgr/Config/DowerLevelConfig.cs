using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>天赋等级</summary>
    public class DowerLevelConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 
        /// 天赋等级ID
        /// 天赋Id*1000+等级
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 天赋ID
        /// 
        /// </summary>
        public double value { get; set; }
    }
}
