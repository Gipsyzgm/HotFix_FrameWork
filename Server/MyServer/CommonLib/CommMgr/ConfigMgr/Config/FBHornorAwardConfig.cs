using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>副本荣耀奖励</summary>
    public class FBHornorAwardConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 荣耀奖励Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 奖励描述
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 奖励
        /// </summary>
        public List<int[]> award { get; set; }
        /// <summary>
        /// 奖励进度
        /// 最大章节id * 100 + 最大关卡数
        /// </summary>
        public int chapterPro { get; set; }
        /// <summary>
        /// 领取所需荣耀点数
        /// </summary>
        public int num { get; set; }
    }
}
