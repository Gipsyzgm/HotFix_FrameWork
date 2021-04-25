using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>每日签到</summary>
    public class SignInConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 签到Id
        /// 奖励组*100+第几天
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 奖励组
        /// </summary>
        public int group { get; set; }
        /// <summary>
        /// 每日签到奖励
        /// 物品ID1_数量;物品ID2_数量 
        /// </summary>
        public List<int[]> award { get; set; }
    }
}
