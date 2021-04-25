using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>物品背景配置</summary>
    public class ItemBgConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 物品id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string EquipBg { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string BagItemBg { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string HeroItemBg { get; set; }
    }
}
