using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
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
        /// <summary>武器-外刀</summary>
        OutWeapon = 4,
        /// <summary>武器-内刀</summary>
        InnerWeapon = 5,
        /// <summary>戒指</summary>
        Ring = 6,
    }
    //装备位置索引
    public enum EItemEquipPlace
    {
        /// <summary>无</summary>
        None = 0,
        /// <summary>英雄面具</summary>
        Hero,
        /// <summary>武器-外刀</summary>
        OutWeapon,
        /// <summary>武器-内刀</summary>
        InnerWeapon,
        /// <summary>戒指1</summary>
        Ring1,
        /// <summary>戒指2</summary>
        Ring2,
        /// <summary>宠物1</summary>
        Pet
    }

    /// <summary>
    /// 虚拟物品子类
    /// </summary>
    public enum EItemSubTypeVirtual
    {
        /// <summary>金币</summary>
        Gold = 1,
        /// <summary>点券</summary>
        Ticket = 2,
        /// <summary>体力</summary>
        Power = 3,
        /// <summary>玩家经验</summary>
        Exp = 4,
    }

    /// <summary>
    /// 道具子类
    /// </summary>
    public enum EItemSubTypeProp
    {
        /// <summary>可使用类道具</summary>
        CanUse = 0,
    }
}
