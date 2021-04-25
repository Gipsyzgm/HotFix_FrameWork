using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>特权配置</summary>
    public class VIPDetailConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 特权Id
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 特权数值
        /// </summary>
        public double num { get; set; }
        /// <summary>
        /// 任务完成条件说明
        /// 语言表ID
        /// </summary>
        public Lang des { get; set; }
    }
}
