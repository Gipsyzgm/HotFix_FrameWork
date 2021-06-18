using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{ 
    /// <summary>
    /// 攻击属性作用类型
    /// </summary>
    public enum EItemAttackType
    {
        OutWeapon,
        InnerWeapon,
        OutAndInnerWeapon,
    }
    /// <summary>
    /// 物品战斗属类型
    /// </summary>
    public enum EItemAttribType
    {
        /// <summary>生命</summary>
        HP = 0,
        /// <summary>闪避率%</summary>
        Miss = 1,
        /// <summary>碰撞伤害减少值</summary>
        CollideDamageSub = 2,
        /// <summary>碰撞伤害减少值%</summary>
        CollideDamageSubPct = 3,
        /// <summary>攻击伤害减少值</summary>
        AttackDamageSub = 4,
        /// <summary>攻击伤害减少值%</summary>
        AttackDamageSubPct = 5,
        /// <summary>移动速度</summary>
        MoveSpeed = 6,
        /// <summary>攻击</summary>
        Attack = 7,
        /// <summary>攻击速度</summary>
        AttackSpeed = 8,
        /// <summary>飞刀数量</summary>
        WeaponNum = 9,
        /// <summary>攻击范围</summary>
        AttackRange = 10,
        /// <summary>爆击率%</summary>
        Crit = 11,
        /// <summary>爆击倍率%</summary>
        CritMult = 12,
    }
}
