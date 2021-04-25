using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>新手指引步骤</summary>
    public class GuideStepConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 指引Id*100+步骤序号
        /// </summary>
        public int id { get; set; }
    }
}
