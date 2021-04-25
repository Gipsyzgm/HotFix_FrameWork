using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>天赋配置</summary>
    public class DowerConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 
        /// 天赋ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 天赋名称
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 天赋描述
        /// </summary>
        public Lang Des { get; set; }
        /// <summary>
        /// 天赋类型
        /// 1 攻击天赋
        /// 2 防御天赋
        /// 3 神圣天赋
        /// 
        /// </summary>
        public int type { get; set; }
    }
}
