using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>物品升星升级</summary>
    public class ItemStarLevelConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 物品id
        /// （物品表Id* 10 + 星级）
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 类型
        /// 0 英雄
        /// 1 宠物
        /// 2 外刀
        /// 3 内刀
        /// 4 指环
        /// 5 防具
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 等级上限
        /// </summary>
        public int lvMax { get; set; }
        /// <summary>
        /// 
        /// 攻击属性类型(攻击，攻速，范围，爆击)
        /// 0 外刀(宠物)
        /// 1 内刀
        /// 2 内刀+外刀
        /// </summary>
        public int attackType { get; set; }
        /// <summary>
        /// 升星消耗装备
        /// </summary>
        public int costItem { get; set; }
    }
}
