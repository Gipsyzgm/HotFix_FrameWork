using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 战斗属性类型
    /// </summary>
    public enum FightAttrType
    {
        /// <summary>生命</summary>
        HP = 1,
        /// <summary>攻击</summary>
        Atk = 2,
        /// <summary>防御</summary>
        Def = 3,
        /// <summary>暴击率1=0.01%</summary>
        Crit = 4,
        /// <summary>移动速度</summary>
        MS = 5,
        /// <summary>攻击速度</summary>
        AS = 6,
    }

    /// <summary>
    /// 战斗属性计算类型
    /// </summary>
    public enum FightAttrCalcuType
    {
        /// <summary>值</summary>
        Value = 1,
        /// <summary>百分比值</summary>
        PctValue = 2,
    }

}
