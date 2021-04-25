using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>天赋设置</summary>
    public class DowerSettingConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => DowerCostFactor;
        /// <summary>
        /// 【天赋】天赋升级消耗系数（金币）实际消耗=当前天赋等级*系数
        /// </summary>
        public int DowerCostFactor { get; set; }
        /// <summary>
        /// 【挂机】挂机加成天赋id
        /// </summary>
        public int HangDowerId { get; set; }
    }
}
