using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    public class ItemSetting
    {
        //物品战斗属类型长度 EItemAttribType 
        public const int AttribTypeCount = 13;

        //百分比类型 EItemAttribType 
        public static HashSet<EItemAttribType> AttribPctType = new HashSet<EItemAttribType>()
        {
            EItemAttribType.Miss,
            EItemAttribType.CollideDamageSubPct,
            EItemAttribType.AttackDamageSubPct,
            EItemAttribType.Crit,
            EItemAttribType.CritMult,
        };

    }
}
