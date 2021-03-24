
using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix
{
    /// <summary>活动</summary>
    public class ActivityConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        ///  活动Id
        /// </summary>
        public int id{ get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int orderid{ get; set; }
        /// <summary>
        /// 活动名称
        /// 语言表ID
        /// </summary>
        public string name{ get; set; }
        /// <summary>
        /// 活动图标
        /// </summary>
        public string icon{ get; set; }
        /// <summary>
        /// 任务完成类型
        /// ETaskType
        /// </summary>
        public int type{ get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public string des{ get; set; }
        /// <summary>
        /// 完成活动描述
        /// </summary>
        public string taskDes{ get; set; }
       
    }        
}
