using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>新手指引</summary>
    public class GuideConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 指引Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 开启条件,需要玩家等级
        /// </summary>
        public int needLv { get; set; }
        /// <summary>
        /// 开启条件，需要完成任务Id
        /// </summary>
        public int needTask { get; set; }
        /// <summary>
        /// 开启条件,需要完成指定指引
        /// </summary>
        public int[] needIds { get; set; }
    }
}
