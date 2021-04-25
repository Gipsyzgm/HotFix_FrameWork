using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>困难副本等级技能</summary>
    public class KNFBLvSkillConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// id
        /// 章节*等级
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 章节id
        /// </summary>
        public int ChapterId { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 是否固定
        /// 0是 1否
        /// </summary>
        public bool IsStatic { get; set; }
    }
}
