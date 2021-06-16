using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 物品大类
    /// </summary>
    public enum EItemType
    {
        /// <summary>虚拟物品</summary>
        Virtual = 0,
        /// <summary>道具</summary>
        Prop = 1,
        /// <summary>英雄</summary>
        Hero = 2,
        /// <summary>宠物</summary>
        Pet = 3,
        /// <summary>外刀</summary>
        OutCut = 4,
        /// <summary>内刀</summary>
        InCut = 5,
        /// <summary>戒指</summary>
        Ring = 6,
        /// <summary>防具</summary>
        Defence = 7
    }

    /// <summary>
    /// 虚拟物品子类
    /// </summary>
    public enum EItemSubTypeVirtual
    {
        /// <summary>金币</summary>
        Gold = 1,
        /// <summary>钻石</summary>
        Ticket = 2,
        ///// <summary>体力</summary>
        Power = 3,
        /// <summary>玩家经验</summary>
        Exp = 4,
        /// <summary>赛季令牌经验</summary>
        SeasonExp = 5,
        /// <summary>荣誉点</summary>
        Hornor = 6,
        /// <summary>Vip经验</summary>
        VipExp = 7,
    }

    /// <summary>
    /// 道具子类
    /// </summary>
    public enum EItemSubTypeProp
    {
        /// <summary>可使用类道具</summary>
        CanUse = 4,
        ///// <summary>消耗类</summary>
        //Consume = 1,
        ///// <summary>战斗道具</summary>
        //Battle = 2,
        ///// <summary>功能道具</summary>
        //Function = 3,
        /// <summary>道具礼包</summary>
        ItemPack = 6,
    }

    /// <summary>
    /// 装备位置
    /// </summary>
    public enum EItemSubTypeEquipIndex
    {
        /// <summary>未穿戴</summary>
        None = 0,
        /// <summary>英雄</summary>
        Hero = 1,
        /// <summary>外刀</summary>
        OutCut = 2,
        /// <summary>内刀</summary>
        InCut = 3,
        /// <summary>戒指1</summary>
        Ring1 = 4,
        /// <summary>戒指2</summary>
        Ring2 = 5,
        /// <summary>宠物</summary>
        Pet = 6,
        /// <summary>防具</summary>
        Armor = 7
    }
}
