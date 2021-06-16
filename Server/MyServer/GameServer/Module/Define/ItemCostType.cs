using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 道具消耗统计类型
    /// </summary>
    public enum ItemCostType
    {
        Normal = 0,
        
        /// <summary>工坊制造道具</summary>
        ForgeMake = 1,
        /// <summary>训练营训练英雄</summary>
        TrainingMake,
        /// <summary>兵营升级部队</summary>
        SmithyLevelUpEquip,
        /// <summary>召唤使用令牌</summary>
        Summon,
        /// <summary>战斗中使用道具</summary>
        WarUse,
        /// <summary>副本扫荡</summary>
        WarQuick,
        /// <summary>装备升级</summary>
        EquipLevelUp,
        /// <summary>天赋重置</summary>
        DowerReset,
        /// <summary>天赋升级</summary>
        DowerLevelUp,
        /// <summary>英雄突破（升阶）</summary>
        HeroBreak,
        /// <summary>英雄升级</summary>
        HeroLevelUp,
    }
}
