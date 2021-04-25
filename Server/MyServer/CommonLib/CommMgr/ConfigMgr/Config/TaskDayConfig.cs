using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>每日任务</summary>
    public class TaskDayConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        ///  任务Id
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 任务类型
        /// 1 击杀小怪计数
        /// 2 击杀boss计数
        /// 3 通关章节计数
        /// 4 观看广告计数
        /// 5 完成关卡计数
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 完成条件参数
        /// </summary>
        public int condition { get; set; }
        /// <summary>
        /// 任务奖励
        /// </summary>
        public List<int[]> award { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public Lang des { get; set; }
    }
}
