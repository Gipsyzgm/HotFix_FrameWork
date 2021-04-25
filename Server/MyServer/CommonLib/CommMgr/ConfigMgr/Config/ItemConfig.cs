using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>物品配置</summary>
    public class ItemConfig : BaseConfig
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
        /// 物品名称
        /// 语言ID
        /// </summary>
        public Lang name { get; set; }
        /// <summary>
        /// 物品图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 物品大类
        /// 0虚拟物品
        /// 1道具
        /// 2 英雄
        /// 3 宠物
        /// 4 武器-外刀
        /// 5 武器-内刀
        /// 6 戒指
        /// 7 防具
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 物品大类
        /// 语言ID
        /// </summary>
        public Lang typeDes { get; set; }
        /// <summary>
        /// 物品子类
        /// 101英雄碎片
        /// 102宠物碎片
        /// 103装备碎片
        /// </summary>
        public int subType { get; set; }
        /// <summary>
        /// 参数1
        /// 使用参照物品类型说明
        /// </summary>
        public int arg1 { get; set; }
        /// <summary>
        /// 参数2
        /// 使用参照物品类型说明
        /// </summary>
        public int arg2 { get; set; }
        /// <summary>
        /// 卖出价格
        /// （金币）
        /// 0 不可出售
        /// </summary>
        public int sell { get; set; }
        /// <summary>
        /// 模型
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 物品描述
        /// 语言ID
        /// </summary>
        public Lang des { get; set; }
        /// <summary>
        /// 对应碎片描述
        /// 语言ID
        /// </summary>
        public Lang des2 { get; set; }
        /// <summary>
        /// 升品所需经验值
        /// （仅限装备）
        /// </summary>
        public int needExp { get; set; }
        /// <summary>
        /// 升品时提供经验值
        /// （仅限装备）
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// 英雄面具是否获得
        /// </summary>
        public bool IsGet { get; set; }
        /// <summary>
        /// 英雄解锁钻石消耗
        /// </summary>
        public int UnlockGemCount { get; set; }
        /// <summary>
        /// 英雄解锁视频次数
        /// </summary>
        public int UnlockVideoCount { get; set; }
        /// <summary>
        /// 装备对应英雄类型不填为通用
        /// </summary>
        public int HeroType { get; set; }
        /// <summary>
        /// 英雄默认内刀ID
        /// </summary>
        public int DefHeroInside { get; set; }
        /// <summary>
        /// 英雄天赋技能id
        /// 技能表id
        /// </summary>
        public int HeroDefSkill { get; set; }
        /// <summary>
        /// 英雄天赋技能Icon
        /// ArtRes\UIAtlas\SkillIcon
        /// </summary>
        public string DefSkillIcon { get; set; }
        /// <summary>
        /// 英雄天赋技能描述
        /// </summary>
        public Lang DefSkillDes { get; set; }
        /// <summary>
        /// 英雄页面大预览图
        /// ArtRes\UIAtlas\Player
        /// </summary>
        public string HeroPreview { get; set; }
        /// <summary>
        /// 英雄类型icon
        /// ArtRes\UIAtlas\Player
        /// </summary>
        public string HeroTypeIcon { get; set; }
        /// <summary>
        /// 英雄类型icon
        /// ArtRes\UIAtlas\Player
        /// </summary>
        public string HeroWeapon { get; set; }
        /// <summary>
        /// 英雄是否已开放
        /// </summary>
        public bool IsActive { get; set; }
    }
}
