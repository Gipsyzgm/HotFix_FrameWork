using CommonLib.Comm.DBMgr;
using GameServer.XGame.Module;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.XGame.Module
{
    public class BaseFightEntity<T> : BaseEntity<T> where T : ITable
    {
        ///// <summary>
        ///// 所有属性类型,属性值
        ///// </summary>
        //public Dictionary<FightAttrType, int> FightAttrList = new Dictionary<FightAttrType, int>();

        ///// <summary>
        ///// 所有属性类型,百分比属性值
        ///// </summary>
        //public Dictionary<FightAttrType, int> FightAttrPctList = new Dictionary<FightAttrType, int>();

        ///// <summary>
        ///// 每点属性对应的战力
        ///// </summary>
        //protected Dictionary<FightAttrType, float> FightAttrFC => FightAttrHelper.FightAttrFC;

        /// <summary>
        /// 战力
        /// </summary>
        public int FC { get; protected set; }

        public BaseFightEntity()
        {
          /*  //战斗力 = 攻击力 + 生命 * 0.1 + 防御 * 2 + 攻击力 * 暴击 * 0.3 + 攻击速度 * 3 + 移动速度 * 2 + POWER(技能2等级, 2) * 50 + POWER(技能3等级, 2) * 50
            FightAttrFC.Add(FightAttrType.HP, 0.1f);
            FightAttrFC.Add(FightAttrType.Atk, 1);
            FightAttrFC.Add(FightAttrType.Def, 2);
            FightAttrFC.Add(FightAttrType.Crit, 0.00003f);
            FightAttrFC.Add(FightAttrType.MS, 2);
            FightAttrFC.Add(FightAttrType.AS, 3);*/
            //设置初始属性值
            //FightAttrReset();
        }



        ///// <summary>
        ///// 获取总属性值 固定值 和百分比值
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public int GetFightAttrTotal(FightAttrType type)
        //{
        //    return (int)Math.Ceiling(FightAttrList[type] * (10000 + FightAttrPctList[type]) / 10000f);
        //}

        /// <summary>
        /// 计算属性值
        /// </summary>
        public virtual void CalculateAttr()
        {
        }
        ///// <summary>
        ///// 计算战力
        ///// </summary>
        //protected virtual void CalculateFC()
        //{
        //    //战斗力 = 攻击力 + 生命 * 0.1 + 防御 * 2 + 攻击力 * 暴击 * 0.3 + 攻击速度 * 3 + 移动速度 * 2 + POWER(技能2等级, 2) * 50 + POWER(技能3等级, 2) * 50
        //    float countFC = 0;
        //    foreach (FightAttrType type in FightAttrFC.Keys)
        //    {
        //        if(type == FightAttrType.Crit)
        //            countFC += (FightAttrList[FightAttrType.Atk] * (10000 + FightAttrPctList[FightAttrType.Atk]) / 10000f) *  (FightAttrList[type] * (10000 + FightAttrPctList[type]) / 10000f) * FightAttrFC[type];
        //        else
        //            countFC += FightAttrList[type] * (10000 + FightAttrPctList[type]) / 10000f * FightAttrFC[type];
        //    }
        //    FC = (int)Math.Ceiling(countFC);
        //}

        ///// <summary>
        ///// 把属性值加到对应属性中
        ///// </summary>
        //protected void FightAttrAddValue(FightAttrItem item)
        //{
        //    if (item != null)
        //    {
        //        switch (item.CalculateType)
        //        {
        //            case FightAttrCalcuType.Value://1值
        //                FightAttrList[item.AttrType] += item.Value;
        //                break;
        //            case FightAttrCalcuType.PctValue://2百分比值
        //                FightAttrPctList[item.AttrType] += item.Value;
        //                break;
        //        }
        //    }
        //}
        //protected void FightAttrAddValues(FightAttrItem[] items)
        //{
        //    if (items != null)
        //    {
        //        for (int i = 0; i < items.Length; i++)
        //        {
        //            FightAttrAddValue(items[i]);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 重置属性值
        ///// </summary>
        //protected void FightAttrReset()
        //{
        //    FightAttrList.Clear();
        //    FightAttrList.Add(FightAttrType.HP, 0);
        //    FightAttrList.Add(FightAttrType.Atk, 0);
        //    FightAttrList.Add(FightAttrType.Def, 0);
        //    FightAttrList.Add(FightAttrType.Crit, 0);
        //    FightAttrList.Add(FightAttrType.MS, 0);
        //    FightAttrList.Add(FightAttrType.AS, 0);

        //    FightAttrPctList.Clear();
        //    FightAttrPctList.Add(FightAttrType.HP, 0);
        //    FightAttrPctList.Add(FightAttrType.Atk, 0);
        //    FightAttrPctList.Add(FightAttrType.Def, 0);
        //    FightAttrPctList.Add(FightAttrType.Crit, 0);
        //    FightAttrPctList.Add(FightAttrType.MS, 0);
        //    FightAttrPctList.Add(FightAttrType.AS, 0);
        //}
    }
}
