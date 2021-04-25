using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>开服基金</summary>
    public class OpenFundConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => level;
        /// <summary>
        /// 领取需要
        /// 玩家等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 返利钻石
        /// </summary>
        public int ticket { get; set; }
    }
}
