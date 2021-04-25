/// <summary>
/// 战斗对象配置接口
/// </summary>

using System.Collections.Generic;

namespace CommonLib.Comm
{
    public interface IWarSceneConf
    {
        int id { get; set; }

        /// <summary>
        /// 最大回合数 0无限制
        /// </summary>
        int roundMax { get; set; }

        /// <summary>
        /// 战斗中获得金币基数
        /// </summary>
        int warFood { get; set; }

        /// <summary>
        /// 战斗中获得石头基数
        /// </summary>
        int warStone { get; set; }

        /// <summary>
        /// 战斗Box配置Id WarBox表(id)
        /// </summary>
        int warBoxId { get; set; }

        /// <summary>
        ///战斗几率配置Id WarSymbolProb表(probId)
        /// </summary>
        int warProbId { get; set; }

        /// <summary>
        /// 战斗连线规则Id WarLineRule表(ruleId) 1 25线
        /// </summary>
        int warRuleId { get; set; }

        /// <summary>
        /// 特殊玩法Id WarSpecial表(warSpecialId) 特殊玩法ID
        /// </summary>
        int warSpecialId { get; set; }
    }
}
