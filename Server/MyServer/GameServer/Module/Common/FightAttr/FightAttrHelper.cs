using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class FightAttrHelper
    {

        private static Dictionary<FightAttrType, float> _fightAttrFC;

        public static Dictionary<FightAttrType, float> FightAttrFC
        {
            get
            {
                if (_fightAttrFC == null)
                {
                    //战斗力 = 攻击力 + 生命 * 0.1 + 防御 * 2 + 攻击力 * 暴击 * 0.3 + 攻击速度 * 3 + 移动速度 * 2 + POWER(技能2等级, 2) * 50 + POWER(技能3等级, 2) * 50
                    _fightAttrFC = new Dictionary<FightAttrType, float>();
                    _fightAttrFC.Add(FightAttrType.HP, 0.1f);
                    _fightAttrFC.Add(FightAttrType.Atk, 1);
                    _fightAttrFC.Add(FightAttrType.Def, 2);
                    _fightAttrFC.Add(FightAttrType.Crit, 0.00003f);
                    _fightAttrFC.Add(FightAttrType.MS, 2);
                    _fightAttrFC.Add(FightAttrType.AS, 3);
                }
                return _fightAttrFC;
            }
        }


        /// <summary>
        /// 跟据属性值,获取对应属 FightAttrItem对象 
        /// </summary>
        public static FightAttrItem GetFightAttrItem(int attr)
        {
            // 20101; 100102

            if (attr == 0)
                return null;
            FightAttrItem item = new FightAttrItem()
            {
                CalculateType = (FightAttrCalcuType)(Math.Abs(attr % 1000 / 100)),  //计算类型 1固定值 2百分比值
                AttrType = (FightAttrType)(Math.Abs(attr % 100)),  //属性类型
                Value = attr / 1000                  //属性值
            };
            return item;
        }
        /// <summary>
        /// 跟据属性值数组,获取对应属 FightAttrItem数组对象 
        /// </summary>
        public static FightAttrItem[] GetFightAttrItems(int[] attrs)
        {
            FightAttrItem[] list = new FightAttrItem[attrs.Length];
            for (int i = 0; i < attrs.Length; i++)
            {
                list[i] = GetFightAttrItem(attrs[i]);
            }
            return list;
        }

        /// <summary>
        /// 获取属性名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttrName(FightAttrType type)
        {
            string rtn = string.Empty;
            switch (type)
            {
                case FightAttrType.Atk: rtn = "攻击"; break;
                case FightAttrType.HP: rtn = "生命"; break;
                case FightAttrType.Def: rtn = "防御"; break;
                case FightAttrType.Crit: rtn = "暴击率"; break;
                case FightAttrType.MS: rtn = "移动速度"; break;
                case FightAttrType.AS: rtn = "攻击速度"; break;
            }
            return rtn;
        }

        /// <summary>
        /// 获取属性名称和属性值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttrNameValue(FightAttrItem item)
        {
            string str = GetAttrName(item.AttrType);
            switch (item.CalculateType)
            {
                case FightAttrCalcuType.Value:
                    str += (item.Value < 0 ? "" : "+") + item.Value.ToString();
                    break;
                case FightAttrCalcuType.PctValue:
                    str += (item.Value < 0 ? "" : "+") + item.Value / 100f + "%";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 获取属性名称和属性值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttrNameValues(FightAttrItem[] items)
        {
            string str = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                str += GetAttrNameValue(items[i]) + "、";
            }
            str = str.TrimEnd('、');
            return str;
        }
    }
}
