using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 建筑产出类型
    /// </summary>
    public enum EBuildOutputType
    {
        /// <summary>食物</summary>
        Food = 0,
        /// <summary>石头</summary>
        Stone,
        /// <summary>人口</summary>
        People,
        /// <summary>食物+石头</summary>
        FoodStone,
    }
}
