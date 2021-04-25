using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>副本关卡</summary>
    public class FBLevelConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 关卡Id
        /// 章节Id*100+关卡号+序号
        /// 必须从0开始，否则编辑器无法显示
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 关卡类型
        /// 0 普通战斗关
        /// 1 奖励关
        /// 2 BOSS关
        /// 3 精英关
        /// </summary>
        public int levelType { get; set; }
        /// <summary>
        /// (相同关卡号)
        /// 出现权重
        /// </summary>
        public int levelWeight { get; set; }
        /// <summary>
        /// 关卡战斗经验
        /// </summary>
        public int warExp { get; set; }
        /// <summary>
        /// 关卡玩家经验
        /// </summary>
        public int exp { get; set; }
    }
}
