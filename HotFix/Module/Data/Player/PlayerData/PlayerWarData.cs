using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// 主要处理玩家的战斗数据
    /// </summary>
    public class PlayerWarData
    {
        /// <summary>生命</summary>
        public int HP;
        /// <summary>闪避率%</summary>
        public float Miss;
        /// <summary>碰撞伤害减少值</summary>
        public int CollideDamageSub;
        /// <summary>碰撞伤害减少值%</summary>
        public float CollideDamageSubPct;
        /// <summary>攻击伤害减少值</summary>
        public int AttackDamageSub;
        /// <summary>攻击伤害减少值%</summary>
        public float AttackDamageSubPct;
        /// <summary>移动速度</summary>
        public float MoveSpeed;
        /// <summary>外刀攻击属性</summary>
        public AttackAttrib OutAttackAttrib;
        /// <summary>
        /// 内刀攻击属性
        /// </summary>
        public AttackAttrib InnerAttackAttrib;
        //飞刀发射器
        public int[] CutterEmitter = new int[2];
        public string[] CutterModel = new string[2];
        public int HeroTemplId;
        public string HeroModel;
        //重新计算属性
        public void Recount()
        {
            HP = 0;
            Miss = 0;
            CollideDamageSub = 0;
            CollideDamageSubPct = 0;
            AttackDamageSub = 0;
            AttackDamageSubPct = 0;
            MoveSpeed = 0;
            OutAttackAttrib = new AttackAttrib();
            InnerAttackAttrib = new AttackAttrib();
            //加入英雄属性
            var hero = ItemMgr.I.GetUseEquip(EItemEquipPlace.Hero);
            if (hero != null)
            {
                AddEquipAttr(hero);
                HeroModel = hero.Config.model;
                HeroTemplId = hero.Config.id;
            }
            else
                HeroTemplId = 0;

            //加入刀属性
            var outWeapon = ItemMgr.I.GetUseEquip(EItemEquipPlace.OutWeapon);
            var innerWeapon = ItemMgr.I.GetUseEquip(EItemEquipPlace.InnerWeapon);
            if (outWeapon != null)
            {
                CutterEmitter[0] = outWeapon.Config.arg1;
                CutterModel[0] = outWeapon.Config.model;
                AddEquipAttr(outWeapon);
            }
            if (innerWeapon != null)
            {
                CutterEmitter[1] = innerWeapon.Config.arg1;
                CutterModel[1] = innerWeapon.Config.model;
                AddEquipAttr(innerWeapon);
            }

            //加入装备属性
            AddEquipAttr(ItemMgr.I.GetUseEquip(EItemEquipPlace.Ring1));
            AddEquipAttr(ItemMgr.I.GetUseEquip(EItemEquipPlace.Ring2));
        }

        //装备，英雄，飞刀属性加统计到总属性，暂时只加配表基本数据
        private void AddEquipAttr(ItemEquip equip)
        {
            if (equip != null)
            {
                HP += (int)equip.AttribsValue[(int)EItemAttribType.HP];
                Miss += equip.AttribsValue[(int)EItemAttribType.Miss];
                CollideDamageSub += (int)equip.AttribsValue[(int)EItemAttribType.CollideDamageSub];
                CollideDamageSubPct += (int)equip.AttribsValue[(int)EItemAttribType.CollideDamageSubPct];
                AttackDamageSub += (int)equip.AttribsValue[(int)EItemAttribType.AttackDamageSub];
                AttackDamageSubPct += (int)equip.AttribsValue[(int)EItemAttribType.AttackDamageSubPct];
                MoveSpeed += equip.AttribsValue[(int)EItemAttribType.MoveSpeed];

                if (equip.AttackType == EItemAttackType.OutWeapon || equip.AttackType == EItemAttackType.OutAndInnerWeapon)
                    OutAttackAttrib.Add(equip.AttribsValue);

                if (equip.AttackType == EItemAttackType.InnerWeapon || equip.AttackType == EItemAttackType.OutAndInnerWeapon)
                    InnerAttackAttrib.Add(equip.AttribsValue);
            }
        }

    }

    public struct AttackAttrib
    {
        /// <summary>攻击</summary>
        public int Attack;
        /// <summary>攻击速度</summary>
        public float AttackSpeed;
        /// <summary>攻击范围</summary>
        public float AttackRange;
        /// <summary>飞刀数量</summary>
        public int WeaponNum;
        /// <summary>爆击率%</summary>
        public float Crit;
        /// <summary>爆击倍率%</summary>
        public float CritMult;

        public void Reset()
        {
            Attack = 0;
            AttackSpeed = 0;
            AttackRange = 0;
            WeaponNum = 0;
            Crit = 0;
            CritMult = 0;
        }

        public void Add(float[] attackAttrs)
        {
            Attack += (int)attackAttrs[(int)EItemAttribType.Attack];
            AttackSpeed += attackAttrs[(int)EItemAttribType.AttackSpeed];
            AttackRange += attackAttrs[(int)EItemAttribType.AttackRange];
            WeaponNum += (int)attackAttrs[(int)EItemAttribType.WeaponNum];
            Crit += attackAttrs[(int)EItemAttribType.Crit];
            CritMult += attackAttrs[(int)EItemAttribType.CritMult];
        }
    }
}
